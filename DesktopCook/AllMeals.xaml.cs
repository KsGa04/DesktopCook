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
    /// Логика взаимодействия для AllMeals.xaml
    /// </summary>
    public partial class AllMeals : Window
    {
        int id;
        private CookingBookEntities _context = new CookingBookEntities();
        private List<Meal> _meals = new List<Meal>();
        private string _FindedName = "";
        private int _category;
        private Users _user;
        public AllMeals(int category, Users user)
        {
            InitializeComponent();
            _user = user;
            _category = category;
            _meals = _context.Meal.ToList();
            _meals = _meals.Where(x => x.IdCategory == _category).ToList();
            ListMeals.ItemsSource = _meals;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Glavnay glavnay = new Glavnay(_user);
            glavnay.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            PrivateAccount privateAccount = new PrivateAccount(_user);
            privateAccount.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            Catalog catalogue = new Catalog(_user);
            catalogue.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            MyRecipes myRecipes = new MyRecipes(_user);
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
                var product = item.Content as Meal;
                int id = product.IdMeal;
                SpecificMeal specificMeal = new SpecificMeal(id, _user);
                specificMeal.Show();
                this.Hide();
            }
        }
    }
}
