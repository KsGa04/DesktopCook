using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DesktopCook
{
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
            Nikname.Text = users.NikName;
            DateBirth.Text = users.DateOfBirth.ToString();
            Post.Text = users.Mail;
            Pass.Password = users.Password;
        }

        /// <summary>
        /// Переходы между окнами 
        /// </summary>
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

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }
        /// <summary>
        /// Сохранение изменений в личном кабинете 
        /// </summary>
        public static void SaveUser(int id, string mail, string pass, string nik, System.DateTime dateTime)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Users user = db.Users.FirstOrDefault(x => x.IdUser == id);
                user.Mail = mail;
                user.Password = pass;
                user.NikName = nik;
                user.DateOfBirth = dateTime;
                db.SaveChanges();
            }
        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (Post.Text != "" && Pass.Password != "")
            {
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    SaveUser(id, Post.Text, Pass.Password, Nikname.Text, System.DateTime.Parse(DateBirth.Text));
                    db.SaveChanges();
                }
                MessageBox.Show("Запись обновлена");
            }
            else
            {
                MessageBox.Show("Введите логин и пароль!");
            }
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipe addRecipe = new AddRecipe(_users);
            addRecipe.Show();
            this.Hide();
        }

        private void Catalogue_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
    }
}
