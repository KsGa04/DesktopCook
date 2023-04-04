using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для UpdateMeals.xaml
    /// </summary>
    public partial class UpdateMeals : Window
    {
        int id;
        private CookingBookEntities _db = new CookingBookEntities();
        private byte[] _image = null;
        private Meal _meals;
        private string _selectedCountryCode;
        private Category _selectedCountry;
        private List<Meal> _meal = new List<Meal>();
        public UpdateMeals()
        {
            InitializeComponent();
            ListViewLoad();
            foreach (var d in _db.Category)
            {
                Categ.Items.Add(d.NameCategory);
            }
            Name.IsEnabled = false;
            UpdateMeal.IsEnabled = false;
            Choose_A_Photo.IsEnabled = false;
            textboxId.IsEnabled = true;
            GetInformation.IsEnabled = true;
        }
        public void ListViewLoad()
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                var moderators = db.Meal.ToList();

                Meals.ItemsSource = moderators;
            }
        }

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
            ImageMeals.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
        }

        private void UpdateMeals_Click(object sender, RoutedEventArgs e)
        {
            if ((Name.Text != "") && (Desc.Text != "") && (Categ.SelectedItem != null))
            {
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    Meal meal = new Meal(Name.Text, Desc.Text, _image, Convert.ToInt32(Categ.SelectedIndex + 1));
                    db.SaveChanges();
                }
                MessageBox.Show("Запись добавлена");
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        private void GetMeals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(textboxId.Text);
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    Meal meal = db.Meal.FirstOrDefault(x => x.IdMeal == id);
                    if (meal == null)
                    {
                        throw new Exception();
                    }
                    _image = meal.ImageMeal;
                    MemoryStream ms = new MemoryStream(_image);
                    ImageMeals.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    Name.Text = meal.NameMeal;
                    Desc.Text = meal.DescriptionMeal;
                    Categ.SelectedItem = meal.IdCategory;
                    Name.IsEnabled = true;
                    UpdateMeal.IsEnabled = true;
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
