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
                            Users user = new Users(textboxLog.Text, textboxPass.Password);
                            db.Users.Add(user);
                            db.SaveChanges();
                            MessageBox.Show("Аккаунт " + textboxLog.Text + " зарегистрирован");
                            textboxLog.Clear();
                            textboxPass.Clear();
                            Glavnay glavnay = new Glavnay(user);
                            glavnay.Show();
                            this.Hide();
                }
            }
        }
    }
}
