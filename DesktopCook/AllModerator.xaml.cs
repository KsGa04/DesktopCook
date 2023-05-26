using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopCook
{
    public partial class AllModerator : Window
    {
        public ListView moder;
        public AllModerator()
        {
            InitializeComponent();
            ListViewLoad();
            moder = Moder;
        }
        public void ListViewLoad()
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                var moderators = db.Moderator.ToList();

                Moder.ItemsSource = moderators;
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
        /// Удаление работника из бд 
        /// </summary>
        public static void RemoveModer(int id)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Moderator moderator = db.Moderator.Where(x => x.IdModerator == id).FirstOrDefault();
                db.Moderator.Remove(moderator);
                db.SaveChanges();
            }
        }
        private void RemoveModerator_Click(object sender, RoutedEventArgs e)
        {
            if (Moder.SelectedIndex >= 0)
            {
                var result = MessageBox.Show("Вы точно хотите удалить этого сотрудника?", "Удалить", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    var item = Moder.SelectedItem as Moderator;
                    int id = item.IdModerator;
                    using (CookingBookEntities db = new CookingBookEntities())
                    {
                        RemoveModer(id);
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали ни один элемент");
            }
            ListViewLoad();
        }
    }
}
