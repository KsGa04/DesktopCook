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
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        private CookingBookEntities _context = new CookingBookEntities();
        private List<Category> _category = new List<Category>();
        private string _FindedName = "";
        private Users _users;
        public Catalog(Users user)
        {
            _users = user;
            InitializeComponent();
            ListCategory.Items.Clear();
            _category = _context.Category.ToList();
            ListCategory.ItemsSource = _category;
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
