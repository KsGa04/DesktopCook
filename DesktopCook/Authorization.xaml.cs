using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DesktopCook
{
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Переходы между окнами
        /// </summary>
        private void Registration_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Hide();
        }

        /// <summary>
        /// Авторизация и проверка данных из бд
        /// </summary>
        public static bool AuthoUser(string mail, string pass)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                foreach (Users user in db.Users)
                {
                    if ((mail == user.Mail) && (pass == user.Password))
                    {
                        MessageBox.Show("Вы вошли под учетной записью " + user.Mail);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль указан неверно!");
                        return false;
                    }
                }
                return true;
            }
        }
        public static bool AuthoModer(string mail, string pass)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                foreach (Users user in db.Users)
                {
                    if ((mail == user.Mail) && (pass == user.Password))
                    {
                        MessageBox.Show("Вы вошли под учетной записью " + user.Mail);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль указан неверно!");
                        return false;
                    }
                }
                return true;
            }
        }
        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                if (db.Users.Where(x => x.Mail == textboxLog.Text && x.Password == textboxPass.Password).Any())
                {
                    Users user = db.Users.Where(x => x.Mail == textboxLog.Text && x.Password == textboxPass.Password).First();
                    Glavnay glavnay = new Glavnay(user);
                    this.Hide();
                    MessageBox.Show("Вы вошли под учетной записью " + user.Mail);
                    glavnay.Show();
                    return;
                }
                else if (db.Moderator.Where(x => x.Mail == textboxLog.Text && x.Password == textboxPass.Password).Any())
                {
                    Moderator moderator = db.Moderator.Where(x => x.Mail == textboxLog.Text && x.Password == textboxPass.Password).First();
                    PrivateAccountModerator glavnay = new PrivateAccountModerator(moderator);
                    this.Hide();
                    MessageBox.Show("Вы вошли под учетной записью " + moderator.Mail);
                    glavnay.Show();
                    return;
                }
                else if (db.Administrator.Where(x => x.Mail == textboxLog.Text && x.Password == textboxPass.Password).Any())
                {
                    Administrator administrator = db.Administrator.Where(x => x.Mail == textboxLog.Text && x.Password == textboxPass.Password).First();
                    AddCategory glavnay = new AddCategory();
                    this.Hide();
                    MessageBox.Show("Вы вошли под учетной записью " + administrator.Mail);
                    glavnay.Show();
                    return;
                }
                else
                {
                    MessageBox.Show("Пользователь либо не зарегистрован либо почта/пароль указаны неверно");
                }

            }

        }
    }
}
