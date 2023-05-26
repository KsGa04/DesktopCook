using System;
using System.Windows;
using System.Windows.Input;

namespace DesktopCook
{
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
        /// <summary>
        /// Переходы между окнами 
        /// </summary>
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
        private void UpdateCategory_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpdateCategory updateCategory = new UpdateCategory();
            updateCategory.Show();
            this.Hide();
        }
        private void UpdateMeals_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpdateMeals updateMeals = new UpdateMeals();
            updateMeals.Show();
            this.Hide();
        }
        private void UpdateModerator_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpdateModerator updateModerator = new UpdateModerator();
            updateModerator.Show();
            this.Hide();
        }
        /// <summary>
        /// Добавление работника в бд
        /// </summary>
        public static void AddModer(string mail, string password, string nik,DateTime dateOfBirth, int idCateg)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Moderator moderator = new Moderator();
                moderator.Mail =mail;
                moderator.Password = password;
                moderator.NikName = nik;
                moderator.DateOfBirth = dateOfBirth;
                moderator.IdCategory = idCateg;
                db.Moderator.Add(moderator);
                db.SaveChanges();
            }
        }
        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            if ((Post.Text != null) && (Pass.Text != null))
            {
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    AddModer(Post.Text, Pass.Text, Nik.Text, Convert.ToDateTime(Date.Text), Convert.ToInt32(Categ.SelectedIndex + 1));


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
