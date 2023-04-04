using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DesktopCook
{
    public partial class PrivateAccountModerator : Window
    {
        private CookingBookEntities _db = new CookingBookEntities();
        private Moderator _moderator;
        int id;
        public PrivateAccountModerator(Moderator moderator)
        {
            InitializeComponent();
            _moderator = moderator;
            id = moderator.IdModerator;
            Nikname.Text = _moderator.NikName;
            DateBirth.Text = _moderator.DateOfBirth.ToString();
            Post.Text = _moderator.Mail;
            Pass.Password = _moderator.Password;
        }

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        private void PrivateAccountModerator_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PrivateAccountModerator accountModerator = new PrivateAccountModerator(_moderator);
            accountModerator.Show();
            this.Hide();
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Moderator user = db.Moderator.FirstOrDefault(x => x.IdModerator == id);
                user.NikName = Nikname.Text;
                user.DateOfBirth = System.DateTime.Parse(DateBirth.Text);
                user.Mail = Post.Text;
                user.Password = Pass.Password;
                db.SaveChanges();
            }
            MessageBox.Show("Запись обновлена");
        }

        private void ListRecipe_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListRecipe listRecipe = new ListRecipe(_moderator);
            listRecipe.Show();
            this.Hide();
        }
    }
}
