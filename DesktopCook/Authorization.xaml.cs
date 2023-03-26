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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Recovery recovery = new Recovery();
            recovery.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                if (role.SelectedIndex == 0)
                {
                    foreach (Users user in db.Users)
                    {
                        if ((textboxLog.Text == user.Mail) && (textboxPass.Password == user.Password))
                        {
                            //MessageBox.Show("Вход успешен!");
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
                            //MessageBox.Show("Вход успешен!");
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
                            //MessageBox.Show("Вход успешен!");
                            AddCategory glavnay = new AddCategory();
                            this.Hide();
                            MessageBox.Show("Вы вошли под учетной записью " + administrator.Mail);
                            glavnay.Show();
                            return;
                        }
                    }
                    MessageBox.Show("Логин или пароль указан неверно!");
                }

            }
        }
    }
}
