using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopCook
{
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
        /// Удаление рецепта 
        /// </summary>
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
        /// <summary>
        /// Заполнение ListView рецептами определенного пользователя
        /// </summary>
        public void FillListRecipe()
        {
            id = _user.IdUser;
            _recipe = _context.Recipe.ToList();
            _recipe = _recipe.Where(x => x.IdUser == id).ToList();
            ListRecipe.ItemsSource = _recipe;
        }
        /// <summary>
        /// При нажатии на блок ListView происходит переход на другое окно с возможностью редактирования рецепта
        /// </summary>

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var item = ListRecipe.SelectedItem as Recipe;
            int id = item.IdRecipe;
            UpdateMyRecipe allMeals = new UpdateMyRecipe(_user, id);
            allMeals.Show();
            this.Hide();
        }
    }
}
