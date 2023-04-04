using System.Windows;

namespace DesktopCook
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            if (textboxPass.Password.Length < 8)
            {
                MessageBox.Show("Неправильная длина пароля");
            }
            else
            {
                Users user = new Users(textboxLog.Text, textboxPass.Password);
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                MessageBox.Show("Аккаунт " + textboxLog.Text + " зарегитрирован");
                textboxLog.Clear();
                textboxPass.Clear();
                Glavnay glavnay = new Glavnay(user);
                glavnay.Show();
                this.Hide();
            }
        }
    }
}
