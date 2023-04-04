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
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
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
