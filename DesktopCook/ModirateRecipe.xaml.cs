using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DesktopCook
{
    public partial class ModirateRecipe : Window
    {
        private int _recipe;
        private byte[] _image;
        private Moderator _moderator;
        public ModirateRecipe(int id, Moderator moderator)
        {
            InitializeComponent();
            _recipe = id;
            _moderator = moderator;
            FillImageBox();
        }
        /// <summary>
        /// Заполнение textbox и ImageRecipe данными определенного рецепта
        /// </summary>
        private void FillImageBox()
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Recipe recipe = db.Recipe.FirstOrDefault(x => x.IdRecipe == _recipe);
                _image = recipe.ImageRecipe;
                MemoryStream ms = new MemoryStream(_image);
                ImageRecipe.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                Name.Text = recipe.NameRecipe;
                Desc.Text = recipe.Description;
                Ingr.Text = recipe.Ingredient;
            }
        }
        /// <summary>
        /// Переходы между окнами
        /// </summary>
        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        private void PrivateAccountModerator_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PrivateAccountModerator accountModerator = new PrivateAccountModerator(_moderator);
            accountModerator.Show();
            this.Hide();
        }

        private void ListRecipes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListRecipe listRecipe = new ListRecipe(_moderator);
            listRecipe.Show();
            this.Hide();
        }
        /// <summary>
        /// Одобрение рецепта модератором 
        /// </summary>
        private void Approve_Click(object sender, RoutedEventArgs e)
        {
                using (CookingBookEntities db = new CookingBookEntities())
                {
                Recipe recipe = db.Recipe.FirstOrDefault(x => x.IdRecipe == _recipe);
                recipe.NameRecipe = Name.Text;
                recipe.Description = Desc.Text;
                recipe.Ingredient = Ingr.Text;
                recipe.Moder = true;
                    db.SaveChanges();
                }
                MessageBox.Show("Запись обновлена");
        }
        /// <summary>
        /// Не одобрение рецепта модератором
        /// </summary>
        private void Disapprove_Click(object sender, RoutedEventArgs e)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Recipe recipe = db.Recipe.FirstOrDefault(x => x.IdRecipe == _recipe);
                recipe.NameRecipe = Name.Text;
                recipe.Description = Desc.Text;
                recipe.Ingredient = Ingr.Text;
                recipe.Moder = false;
                db.SaveChanges();
            }
            MessageBox.Show("Запись обновлена");
        }
    }
}
