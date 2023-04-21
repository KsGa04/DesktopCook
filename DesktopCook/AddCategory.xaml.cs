using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DesktopCook
{
    public partial class AddCategory : Window
    {
        public ListView categ;
        private byte[] _image;
        public AddCategory()
        {
            InitializeComponent();
            ListViewLoad();
            categ = Categ;
            MessageBox.Show(categ.Items.Count.ToString());
        }
        /// <summary>
        /// Заполнение ListView данными из твблицы Category
        /// </summary>
        public void ListViewLoad()
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                var categories = db.Category.ToList();

                Categ.ItemsSource = categories;
            }
        }
        /// <summary>
        /// Переходы между окнами
        /// </summary>
        private void AddMeals_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddMeals addMeals = new AddMeals();
            addMeals.Show();
            this.Hide();
        }

        private void AddCategory_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddCategory addCategory = new AddCategory();
            addCategory.Show();
            this.Hide();
        }

        private void AddWorker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddWorker addWorker = new AddWorker();
            addWorker.Show();
            this.Hide();
        }

        private void AllModerator_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
        /// Возможность выбрать фотографию
        /// </summary>
        private void ChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string path;
            if ((bool)openFileDialog.ShowDialog())
            {
                path = openFileDialog.FileName;
                _image = System.IO.File.ReadAllBytes(path);
            }
            MemoryStream ms = new MemoryStream(_image);
            ImageAccount.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
        }
        /// <summary>
        /// Добавление новой категории в бд и вывод сообщения об успешном/не успешном добавлении
        /// </summary>
        public static void AddCategories(string name, byte[] image)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Category category = new Category(name, image);
                db.Category.Add(category);
                db.SaveChanges();
            }
        }
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                if (Name.Text != "")
                {
                    AddCategories(Name.Text, _image);
                    MessageBox.Show("Запись добавлена");
                }
                else
                {
                    MessageBox.Show("Заполните все окна");
                }
            }
            ListViewLoad();
        }
        public static void RemoveCategories(int id)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Category category = db.Category.Where(x => x.IdCategory == id).FirstOrDefault();
                db.Category.Remove(category);
                db.SaveChanges();
            }
        }
        private void RemoveCategory_Click(object sender, RoutedEventArgs e)
        {
            if (Categ.SelectedIndex >= 0)
            {
                var result = MessageBox.Show("Вы точно хотите удалить эту категорию?", "Удалить", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    var item = Categ.SelectedItem as Category;
                    int id = item.IdCategory;
                    RemoveCategories(id);
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
