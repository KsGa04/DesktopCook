using System.Net;
using System.Net.Mail;
using System.Windows;

namespace DesktopCook
{
    public partial class Recovery : Window
    {
        public Recovery()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Отправка пароль на почту пользователя 
        /// </summary>
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
