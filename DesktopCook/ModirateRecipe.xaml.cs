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
    /// Логика взаимодействия для ModirateRecipe.xaml
    /// </summary>
    public partial class ModirateRecipe : Window
    {
        private CookingBookEntities _context = new CookingBookEntities();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PrivateAccountModerator accountModerator = new PrivateAccountModerator(_moderator);
            accountModerator.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ListRecipe listRecipe = new ListRecipe(_moderator);
            listRecipe.Show();
            this.Hide();
        }
    }
}
