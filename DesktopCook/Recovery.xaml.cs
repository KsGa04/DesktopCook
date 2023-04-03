using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Логика взаимодействия для Recovery.xaml
    /// </summary>
    public partial class Recovery : Window
    {
        public Recovery()
        {
            InitializeComponent();
        }
        private void Recovery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MailAddress from = new MailAddress("kseniagaranceva@gmail.com", "Ksenia");
                MailAddress to = new MailAddress(textboxMail.Text);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Тест";
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    foreach (Users user in db.Users)
                    {
                        if (textboxMail.Text == user.Mail)
                        {
                            m.Body = "<h1>Пароль: " + user.Password + "</h1>";
                        }
                    }
                }
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                smtp.Credentials = new NetworkCredential("kseniagaranceva@gmail.com", "123");
                smtp.EnableSsl = true;
                smtp.Send(m);
            }

            catch { MessageBox.Show("Ошибка отправки письма"); }
        }
    }
}
