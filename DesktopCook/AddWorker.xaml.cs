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
    /// Логика взаимодействия для AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Window
    {
        private CookingBookEntities _db = new CookingBookEntities();
        public AddWorker()
        {
            InitializeComponent();
            foreach (var d in _db.Category)
            {
                Categ.Items.Add(d.NameCategory);
            }
        }

        private void AddCategory_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddCategory addCategory = new AddCategory();
            addCategory.Show();
            this.Hide();
        }

        private void AddMeals_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddMeals addMeals = new AddMeals();
            addMeals.Show();
            this.Hide();
        }

        private void AddWorker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddWorker addWorker = new AddWorker();
            addWorker.Show();
            this.Hide();
        }

        private void AllModerators_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AllModerator allModerator = new AllModerator();
            allModerator.Show();
            this.Hide();
        }

        private void Authorization_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            if ((Post.Text != null) && (Pass.Text != null))
            {
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    Moderator moderator = new Moderator();
                    moderator.Mail = Post.Text;
                    moderator.Password = Pass.Text;
                    moderator.NikName = Nik.Text;
                    moderator.DateOfBirth = Convert.ToDateTime(Date.Text);
                    moderator.IdCategory = Convert.ToInt32(Categ.SelectedIndex + 1);
                    db.Moderator.Add(moderator);
                    db.SaveChanges();

                }
                MessageBox.Show("Запись добавлена");
            }
            else
            {
                MessageBox.Show("Заполните поля Почты и Пароля");
            }
        }
    }
}
