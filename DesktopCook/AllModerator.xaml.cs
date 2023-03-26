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
    /// Логика взаимодействия для AllModerator.xaml
    /// </summary>
    public partial class AllModerator : Window
    {
        private CookingBookEntities _context = new CookingBookEntities();
        private List<Moderator> _moderator = new List<Moderator>();
        public AllModerator()
        {
            InitializeComponent();
            ListViewLoad();
        }
        public void ListViewLoad()
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                var moderators = db.Moderator.ToList();

                Moder.ItemsSource = moderators;
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddCategory addCategory = new AddCategory();
            addCategory.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            AddMeals addMeals = new AddMeals();
            addMeals.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            AddWorker addWorker = new AddWorker();
            addWorker.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            AllModerator allModerator = new AllModerator();
            allModerator.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
                        Moderator moderator = db.Moderator.Where(x => x.IdModerator == id).FirstOrDefault();

                        db.Moderator.Remove(moderator);
                        db.SaveChanges();
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
