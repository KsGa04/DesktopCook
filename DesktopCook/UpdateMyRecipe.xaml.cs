using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopCook
{
    /// <summary>
    /// Логика взаимодействия для UpdateMyRecipe.xaml
    /// </summary>
    public partial class UpdateMyRecipe : Window
    {
        private CookingBookEntities _context = new CookingBookEntities();
        private List<Recipe> _recipe = new List<Recipe>();
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
        }
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
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            _userId = _user.IdUser;
            if ((Name.Text != "") && (Desc.Text != "") && (Categ.SelectedItem != null) && (Dish.SelectedItem != null))
            {
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    Recipe recipe = new Recipe(Name.Text, Ingr.Text, Desc.Text, _image, Convert.ToInt32(Categ.SelectedIndex + 1), Convert.ToInt32(Dish.SelectedIndex + 1), _userId, false);
                    db.SaveChanges();
                }
                MessageBox.Show("Запись обновлена");
            }
        }
    }
}
