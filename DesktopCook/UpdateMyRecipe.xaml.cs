using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DesktopCook
{
    public partial class UpdateMyRecipe : Window
    {
        private CookingBookEntities _db = new CookingBookEntities();
        private byte[] _image;
        private Users _user;
        private int _id;
        private int _userId;
        public UpdateMyRecipe(Users users, int id)
        {
            InitializeComponent();
            _user = users;
            _id = id;
            FillImageBox();
            foreach (var d in _db.Category)
            {
                Categ.Items.Add(d.NameCategory);
            }
            foreach (var i in _db.Meal)
            {
                Dish.Items.Add(i.NameMeal);
            }
        }
        /// <summary>
        /// Переходы между окнами 
        /// </summary>
        private void Main_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Glavnay glavnay = new Glavnay(_user);
            glavnay.Show();
            this.Hide();
        }

        private void PrivateAccount_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PrivateAccount privateAccount = new PrivateAccount(_user);
            privateAccount.Show();
            this.Hide();
        }

        private void Catalogue_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Catalog catalogue = new Catalog(_user);
            catalogue.Show();
            this.Hide();
        }

        private void MyRecipes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MyRecipes myRecipes = new MyRecipes(_user);
            myRecipes.Show();
            this.Hide();
        }

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }
        /// <summary>
        /// Заполнение окна данными о рецепте
        /// </summary>
        private void FillImageBox()
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Recipe recipe = db.Recipe.FirstOrDefault(x => x.IdRecipe == _id);
                _image = recipe.ImageRecipe;
                MemoryStream ms = new MemoryStream(_image);
                ImageAccount.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                Name.Text = recipe.NameRecipe;
                Desc.Text = recipe.Description;
                Ingr.Text = recipe.Ingredient;
            }
        }
        /// <summary>
        /// Возможность выбрать фото
        /// </summary>
        private void Choose_A_Photo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string path;
            if ((bool)openFileDialog.ShowDialog())
            {
                path = openFileDialog.FileName;
                _image = System.IO.File.ReadAllBytes(path);
            }
            MemoryStream ms = new MemoryStream(_image);
            ImageAccount.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
        }
        /// <summary>
        /// Сохрание изменений о рецепте
        /// </summary>
        public static void UpdateRecipe(int id, string name, string ingr, string description, byte[] image, int idCateg, int idMeal, int idUser, bool moder)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Recipe recipe = db.Recipe.FirstOrDefault(x => x.IdRecipe == id);
                recipe.ImageRecipe = image;
                recipe.NameRecipe = name;
                recipe.Ingredient = ingr;
                recipe.Description = description;
                recipe.IdCategory = idCateg;
                recipe.IdMeal = idMeal;
                recipe.IdUser = idUser;
                recipe.Moder = moder;
                db.SaveChanges();
            }
        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            _userId = _user.IdUser;
            if ((Name.Text != "") && (Desc.Text != "") && (Categ.SelectedItem != null) && (Dish.SelectedItem != null))
            {
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    UpdateRecipe(_id, Name.Text, Ingr.Text, Desc.Text, _image, Convert.ToInt32(Categ.SelectedIndex + 1), Convert.ToInt32(Dish.SelectedIndex + 1), _userId, false);
                }
                MessageBox.Show("Запись обновлена");
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
    }
}
