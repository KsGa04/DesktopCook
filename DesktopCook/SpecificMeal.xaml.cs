﻿using System;
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
    /// Логика взаимодействия для SpecificMeal.xaml
    /// </summary>
    public partial class SpecificMeal : Window
    {
        private CookingBookEntities _context = new CookingBookEntities();
        private List<Recipe> _recipe = new List<Recipe>();
        private byte[] _image;
        private int _meal;
        private Users _user;
        public SpecificMeal(int meal, Users user)
        {
            InitializeComponent();
            _user = user;
            _meal = meal;
            ListRecipe.Items.Clear();
            _recipe = _context.Recipe.ToList();
            _recipe = _recipe.Where(x => x.IdMeal == _meal).ToList();
            ListRecipe.ItemsSource = _recipe;
            FillImageBox();
        }
        private void FillImageBox()
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Meal meal = db.Meal.FirstOrDefault(x => x.IdMeal == _meal);
                _image = meal.ImageMeal;
                MemoryStream ms = new MemoryStream(_image);
                ImageMeals.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                Name.Text = meal.NameMeal;
                Desc.Text = meal.DescriptionMeal;
            }
        }
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MyRecipes myRecipes = new MyRecipes(_user);
            myRecipes.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Catalog catalogue = new Catalog(_user);
            catalogue.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            PrivateAccount privateAccount = new PrivateAccount(_user);
            privateAccount.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            Glavnay glavnay = new Glavnay(_user);
            glavnay.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddRecipe addRecipe = new AddRecipe(_user);
            addRecipe.Show();
            this.Hide();
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item)
            {
                var product = item.Content as Recipe;
                int id = product.IdRecipe;
                NewRecipe newRecipe = new NewRecipe(id, _user);
                newRecipe.Show();
                this.Hide();
            }
        }
    }
}