using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DesktopCook
{
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
        /// <summary>
        /// Заполнение окна данными о блюде
        /// </summary>
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
        /// <summary>
        /// Переходы между окнами 
        /// </summary>
        private void MyRecipes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MyRecipes myRecipes = new MyRecipes(_user);
            myRecipes.Show();
            this.Hide();
        }

        private void Catalogue_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Catalog catalogue = new Catalog(_user);
            catalogue.Show();
            this.Hide();
        }

        private void PrivateAccount_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PrivateAccount privateAccount = new PrivateAccount(_user);
            privateAccount.Show();
            this.Hide();
        }

        private void Main_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Glavnay glavnay = new Glavnay(_user);
            glavnay.Show();
            this.Hide();
        }

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipe addRecipe = new AddRecipe(_user);
            addRecipe.Show();
            this.Hide();
        }
        /// <summary>
        /// При нажатии на блок ListView происходит переход на другое окно с общим описанием рецепта
        /// </summary>
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
