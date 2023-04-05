using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DesktopCook
{
    public partial class UpdateModerator : Window
    {
        int id;
        private CookingBookEntities _db = new CookingBookEntities();
        public UpdateModerator()
        {
            InitializeComponent();
            Post.IsEnabled = false;
            Pass.IsEnabled = false;
            Nik.IsEnabled = false;
            Date.IsEnabled = false;
            Categ.IsEnabled = false;
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
        /// Сохрание изменений о работнике
        /// </summary>
        private void UpdateWorker_Click(object sender, RoutedEventArgs e)
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
                    db.SaveChanges();

                }
                MessageBox.Show("Запись добавлена");
            }
            else
            {
                MessageBox.Show("Заполните поля Почты и Пароля");
            }
        }
        /// <summary>
        /// Получение данных о работнике с определенным Id
        /// </summary>
        private void GetCategory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(textboxId.Text);
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    Moderator moderator = db.Moderator.FirstOrDefault(x => x.IdModerator == id);
                    if (moderator == null)
                    {
                        throw new Exception();
                    }
                    Post.Text = moderator.Mail;
                    Pass.Text = moderator.Password;
                    Nik.Text = moderator.NikName;
                    Date.Text = Convert.ToString(moderator.DateOfBirth);
                    Categ.SelectedIndex = (int)moderator.IdCategory;
                    textboxId.IsEnabled = false;
                    GetInformation.IsEnabled = false;
                    Post.IsEnabled = true;
                    Pass.IsEnabled = true;
                    Nik.IsEnabled = true;
                    Date.IsEnabled = true;
                    Categ.IsEnabled = true;

                }
            }
            catch
            {
                MessageBox.Show("Введите действительный Id");
            }
        }
    }
}
