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

        private void Recovery_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Recovery recovery = new Recovery();
            recovery.Show();
            this.Hide();
        }
        /// <summary>
        /// Авторизация и проверка данных из бд
        /// </summary>
        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                if (role.SelectedIndex == 0)
                {
                    foreach (Users user in db.Users)
                    {
                        if ((textboxLog.Text == user.Mail) && (textboxPass.Password == user.Password))
                        {
                            Glavnay glavnay = new Glavnay(user);
                            this.Hide();
                            MessageBox.Show("Вы вошли под учетной записью " + user.Mail);
                            glavnay.Show();
                            return;
                        }
                    }
                    MessageBox.Show("Логин или пароль указан неверно!");
                }
                else if (role.SelectedIndex == 1)
                {
                    foreach (Moderator moderator in db.Moderator)
                    {
                        if ((textboxLog.Text == moderator.Mail) && (textboxPass.Password == moderator.Password))
                        {
                            PrivateAccountModerator glavnay = new PrivateAccountModerator(moderator);
                            this.Hide();
                            MessageBox.Show("Вы вошли под учетной записью " + moderator.Mail);
                            glavnay.Show();
                            return;
                        }
                    }
                    MessageBox.Show("Логин или пароль указан неверно!");
                }
                else if (role.SelectedIndex == 2)
                {
                    foreach (Administrator administrator in db.Administrator)
                    {
                        if ((textboxLog.Text == administrator.Mail) && (textboxPass.Password == administrator.Password))
                        {
                            AddCategory glavnay = new AddCategory();
                            this.Hide();
                            MessageBox.Show("Вы вошли под учетной записью " + administrator.Mail);
                            glavnay.Show();
                            return;
                        }
                    }
                    MessageBox.Show("Логин или пароль указан неверно!");
                }
                else
                {
                    MessageBox.Show("Выберите роль!");
                }

            }
        }

        private void Pass_Checked(object sender, RoutedEventArgs e)
        {
            if (Pass.IsChecked == true)
            {
            }
        }
    }
}
