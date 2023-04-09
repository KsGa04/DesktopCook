using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DesktopCook
{
    public partial class UpdateCategory : Window
    {
        int id;
        private byte[] _image;
        public UpdateCategory()
        {
            InitializeComponent();
            ListViewLoad();
            Choose_A_Photo.IsEnabled = false;
            SaveChanges.IsEnabled = false;
            Name.IsEnabled = false;

        }
        /// <summary>
        /// Заполнение ListView категориями
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
        /// Возможность выбрать фото
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
            ImageCategory.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
        }
        /// <summary>
        /// Сохрание изменений о категории
        /// </summary>

        private void UpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                if (Name.Text != "")
                {
                    id = Convert.ToInt32(textboxId.Text);
                    Category category = db.Category.FirstOrDefault(x => x.IdCategory == id);
                    category.ImageCategory = _image;
                    category.NameCategory = Name.Text;
                    db.SaveChanges();
                    MessageBox.Show("Запись обновлена");
                }
                else
                {
                    MessageBox.Show("Заполните все окна");
                }
            }
            ListViewLoad();
        }
        /// <summary>
        /// Получение данных о категории с определенным Id
        /// </summary>
        private void GetCategory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(textboxId.Text);
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    Category category = db.Category.FirstOrDefault(x => x.IdCategory == id);
                    if (category == null)
                    {
                        throw new Exception();
                    }
                    _image = category.ImageCategory;
                    MemoryStream ms = new MemoryStream(_image);
                    ImageCategory.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    Name.Text = category.NameCategory;
                    Name.IsEnabled = true;
                    SaveChanges.IsEnabled = true;
                    Choose_A_Photo.IsEnabled = true;
                    textboxId.IsEnabled = false;
                    GetInformation.IsEnabled = false;
                }
            }
            catch
            {
                MessageBox.Show("Введите действительный Id");
            }
        }
    }
}
