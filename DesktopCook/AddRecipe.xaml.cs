using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DesktopCook
{

    public partial class AddRecipe : Window
    {
        private CookingBookEntities _db = new CookingBookEntities();
        private byte[] _image;
        private Users _users;
        private int id;
        public AddRecipe(Users user)
        {
            InitializeComponent();
            _users = user;

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
            Glavnay glavnay = new Glavnay(_users);
            glavnay.Show();
            this.Hide();
        }

        private void PrivateAccount_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PrivateAccount privateAccount = new PrivateAccount(_users);
            privateAccount.Show();
            this.Hide();
        }

        private void Catalog_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Catalog catalogue = new Catalog(_users);
            catalogue.Show();
            this.Hide();
        }

        private void MyRecipes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MyRecipes myRecipes = new MyRecipes(_users);
            myRecipes.Show();
            this.Hide();
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

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }
        /// <summary>
        /// Добавление рецепта в бд
        /// </summary>
        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            id = _users.IdUser;
            if ((Name.Text != "") && (Desc.Text != "") && (Categ.SelectedItem != null) && (Dish.SelectedItem != null))
            {
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    Recipe recipe = new Recipe(Name.Text, Ingr.Text, Desc.Text, _image, Convert.ToInt32(Categ.SelectedIndex + 1), Convert.ToInt32(Dish.SelectedIndex + 1), id, false);
                    db.Recipe.Add(recipe);
                    db.SaveChanges();
                }
                MessageBox.Show("Запись добавлена");
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
    }
}
