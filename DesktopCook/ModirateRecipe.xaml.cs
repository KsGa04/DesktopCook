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
        private void ModirateComment_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ModirateComment modirate = new ModirateComment(_moderator);
            modirate.Show();
            this.Hide();
        }
        /// <summary>
        /// Одобрение рецепта модератором 
        /// </summary>
        public static void ApproveDisRecipe(int id, string name, string ingr, string description, bool moder)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Recipe recipe = db.Recipe.FirstOrDefault(x => x.IdRecipe == id);
                recipe.NameRecipe = name;
                recipe.Ingredient = ingr;
                recipe.Description = description;
                recipe.Moder = moder;
                db.SaveChanges();
            }
        }
        private void Approve_Click(object sender, RoutedEventArgs e)
        {
                ApproveDisRecipe(_recipe, Name.Text, Ingr.Text, Desc.Text, true);
                MessageBox.Show("Запись обновлена");
        }
        /// <summary>
        /// Не одобрение рецепта модератором
        /// </summary>
        private void Disapprove_Click(object sender, RoutedEventArgs e)
        {
            ApproveDisRecipe(_recipe, Name.Text, Ingr.Text, Desc.Text, false);
            MessageBox.Show("Запись обновлена");
        }
    }
}
