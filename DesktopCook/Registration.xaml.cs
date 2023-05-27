using System.Windows;

namespace DesktopCook
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Регистрация 
        /// </summary>
        public static void Reg(string mail, string pass)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Users user = new Users(mail, pass);
                db.Users.Add(user);
                db.SaveChanges();
                Glavnay glavnay = new Glavnay(user);
                glavnay.Show();
            }
        }
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            if (textboxPass.Password.Length < 8)
            {
                MessageBox.Show("Неправильная длина пароля");
            }
            else
            {
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    Reg(textboxLog.Text, textboxPass.Password);
                    MessageBox.Show("Аккаунт " + textboxLog.Text + " зарегистрирован");
                    textboxLog.Clear();
                    textboxPass.Clear();
                    this.Hide();

                }
            }
        }
    }
}
