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
        /// <summary>
        /// Переходы между окнами
        /// </summary>
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
        private void ModirateComment_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ModirateComment modirate = new ModirateComment(_moderator);
            modirate.Show();
            this.Hide();
        }
        /// <summary>
        /// Сохранение изменений в личном кабинете 
        /// </summary>
        public static void SaveModer(int id, string mail, string pass, string nik, System.DateTime dateTime)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Moderator moderator = db.Moderator.FirstOrDefault(x => x.IdModerator == id);
                moderator.Mail = mail;
                moderator.Password = pass;
                moderator.NikName = nik;
                moderator.DateOfBirth = dateTime;
                db.SaveChanges();
            }
        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                SaveModer(id, Post.Text, Pass.Password, Nikname.Text, System.DateTime.Parse(DateBirth.Text));
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
