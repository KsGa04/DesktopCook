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
    /// Логика взаимодействия для AddRecipe.xaml
    /// </summary>
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

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Glavnay glavnay = new Glavnay(_users);
            glavnay.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            PrivateAccount privateAccount = new PrivateAccount(_users);
            privateAccount.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            Catalog catalogue = new Catalog(_users);
            catalogue.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            MyRecipes myRecipes = new MyRecipes(_users);
            myRecipes.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string path;
            if ((bool)openFileDialog.ShowDialog())
            {
                path = openFileDialog.FileName;
                _image = System.IO.File.ReadAllBytes(path);

                //_db.Users.Add(new Users() { DateOfBirth = Convert.ToDateTime("12.04.1997"), ImageUser = image, Mail = "test", NikName = "test", Password = "test" });
                //_db.SaveChanges();
            }
            MemoryStream ms = new MemoryStream(_image);
            ImageAccount.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            id = _users.IdUser;
            if ((Name.Text != "") && (Desc.Text != "") && (Categ.SelectedItem != null) && (Dish.SelectedItem != null)) ;
            {
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    Recipe recipe = new Recipe(Name.Text, Ingr.Text, Desc.Text, _image, Convert.ToInt32(Categ.SelectedIndex + 1), Convert.ToInt32(Dish.SelectedIndex + 1), id);
                    db.Recipe.Add(recipe);
                    db.SaveChanges();
                }
                MessageBox.Show("Запись добавлена");
            }
        }
    }
}
