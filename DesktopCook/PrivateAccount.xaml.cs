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
    /// Логика взаимодействия для PrivateAccount.xaml
    /// </summary>
    public partial class PrivateAccount : Window
    {
        private CookingBookEntities _db = new CookingBookEntities();
        private Users _users;
        int id;
        public PrivateAccount()
        {
            InitializeComponent();
        }
        public PrivateAccount(Users users)
        {
            InitializeComponent();

            _users = users;
            id = users.IdUser;

            //FillImageBox();
            Nikname.Text = _users.NikName;
            DateBirth.Text = _users.DateOfBirth.ToString();
            Post.Text = _users.Mail;
            Pass.Password = _users.Password;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            //    OpenFileDialog openFileDialog = new OpenFileDialog();
            //    string path;
            //    if ((bool)openFileDialog.ShowDialog())
            //    {
            //        path = openFileDialog.FileName;
            //        _image = System.IO.File.ReadAllBytes(path);

            //        //_db.Users.Add(new Users() { DateOfBirth = Convert.ToDateTime("12.04.1997"), ImageUser = image, Mail = "test", NikName = "test", Password = "test" });
            //        //_db.SaveChanges();
            //    }
            ////FillImageBox();

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Users user = db.Users.FirstOrDefault(x => x.IdUser == id);
                user.NikName = Nikname.Text;
                user.DateOfBirth = System.DateTime.Parse(DateBirth.Text);
                user.Mail = Post.Text;
                user.Password = Pass.Password;
                db.SaveChanges();
            }
            MessageBox.Show("Запись обновлена");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddRecipe addRecipe = new AddRecipe(_users);
            addRecipe.Show();
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
    }
}
