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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
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
