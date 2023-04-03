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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopCook
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private Users _users;
        public UserControl1()
        {
            InitializeComponent();
            //_users = users;
        }
        private void Glavnay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Glavnay glavnay = new Glavnay(_users);
            glavnay.Show();
        }

        private void PrivateAccount_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PrivateAccount account = new PrivateAccount(_users);
            account.Show();
        }

        private void Catalog_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Catalog catalog = new Catalog(_users);
            catalog.Show();
        }

        private void MyRecipe_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MyRecipes myRecipe = new MyRecipes(_users);
            myRecipe.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();

        }
    }
}
