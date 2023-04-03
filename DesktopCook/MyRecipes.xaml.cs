using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MyRecipes.xaml
    /// </summary>
    public partial class MyRecipes : Window
    {
        private CookingBookEntities _context = new CookingBookEntities();
        private List<Recipe> _recipe = new List<Recipe>();
        private Users _user;
        private int id;
        public MyRecipes(Users user)
        {
            _user = user;
            int id = user.IdUser;
            InitializeComponent();
            ListRecipe.Items.Clear();
            _recipe = _context.Recipe.ToList();
            _recipe = _recipe.Where(x => x.IdUser == id).ToList();
            ListRecipe.ItemsSource = _recipe;
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

        private void RemoveRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (ListRecipe.SelectedIndex >= 0)
            {
                var result = MessageBox.Show("Вы точно хотите удалить этот рецепт?", "Удалить", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    var item = ListRecipe.SelectedItem as Recipe;
                    int id = item.IdRecipe;
                    using (CookingBookEntities db = new CookingBookEntities())
                    {
                        Recipe recipe = db.Recipe.Where(x => x.IdRecipe == id).FirstOrDefault();

                        db.Recipe.Remove(recipe);
                        db.SaveChanges();
                        FillListRecipe();
                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали ни один элемент");
                }
            }
        }
        public void FillListRecipe()
        {
            id = _user.IdUser;
            _recipe = _context.Recipe.ToList();
            _recipe = _recipe.Where(x => x.IdUser == id).ToList();
            ListRecipe.ItemsSource = _recipe;
        }
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item)
            {
                var product = item.Content as Recipe;
                int id = product.IdRecipe;
                UpdateMyRecipe allMeals = new UpdateMyRecipe(_user, id);
                allMeals.Show();
                this.Hide();

            }
        }
    }
}
