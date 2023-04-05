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

    public partial class AddMeals : Window
    {
        private CookingBookEntities _db = new CookingBookEntities();
        private byte[] _image = null;

        private List<Meal> _meal = new List<Meal>();
        public AddMeals()
        {
            InitializeComponent();
            ListViewLoad();
            foreach (var d in _db.Category)
            {
                Categ.Items.Add(d.NameCategory);
            }
        }
        /// <summary>
        /// Заполнение ListView данными из таблицы Meal
        /// </summary>
        public void ListViewLoad()
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                var meals = db.Meal.ToList();

                Meals.ItemsSource = meals;
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
            ImageAccount.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
        }
        /// <summary>
        /// Добавление Блюда в таблицу
        /// </summary>
        private void AddMeals_Click(object sender, RoutedEventArgs e)
        {
            if ((Name.Text != "") && (Desc.Text != "") && (Categ.SelectedItem != null))
            {
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    Meal meal = new Meal(Name.Text, Desc.Text, _image, Convert.ToInt32(Categ.SelectedIndex + 1));
                    db.Meal.Add(meal);
                    db.SaveChanges();
                }
                MessageBox.Show("Запись добавлена");
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        /// <summary>
        /// Удаление блюда из таблицы
        /// </summary>
        private void RemoveMeal_Click(object sender, RoutedEventArgs e)
        {
            if (Meals.SelectedIndex >= 0)
            {
                var result = MessageBox.Show("Вы точно хотите удалить это блюдо?", "Удалить", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    var item = Meals.SelectedItem as Meal;
                    int id = item.IdMeal;
                    using (CookingBookEntities db = new CookingBookEntities())
                    {
                        Meal meal = db.Meal.Where(x => x.IdMeal == id).FirstOrDefault();

                        db.Meal.Remove(meal);
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
