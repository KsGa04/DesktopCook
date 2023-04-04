using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopCook
{
    public partial class Catalog : Window
    {
        private CookingBookEntities _context = new CookingBookEntities();
        private List<Category> _category = new List<Category>();
        private Users _users;
        public Catalog(Users user)
        {
            _users = user;
            InitializeComponent();
            ListCategory.Items.Clear();
            _category = _context.Category.ToList();
            ListCategory.ItemsSource = _category;
        }
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

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item)
            {
                var product = item.Content as Category;
                int id = product.IdCategory;
                AllMeals allMeals = new AllMeals(id, _users);
                allMeals.Show();
                this.Hide();

            }
        }

    }
}
