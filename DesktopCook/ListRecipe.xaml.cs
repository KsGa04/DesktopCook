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
    /// Логика взаимодействия для ListRecipe.xaml
    /// </summary>
    public partial class ListRecipe : Window
    {
        private CookingBookEntities _context = new CookingBookEntities();
        private List<Recipe> _recipe = new List<Recipe>();
        private Moderator _moderator;
        private int? id;
        public ListRecipe(Moderator moderator)
        {
            _moderator = moderator;
            id = moderator.IdCategory;
            InitializeComponent();
            LstRecipe.Items.Clear();
            _recipe = _context.Recipe.ToList();
            _recipe = _recipe.Where(x => x.IdCategory == id && x.Moder == false).ToList();
            LstRecipe.ItemsSource = _recipe;
        }
        private void PrivateAccountModerator_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PrivateAccountModerator accountModerator = new PrivateAccountModerator(_moderator);
            accountModerator.Show();
            this.Hide();
        }

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item)
            {
                var product = item.Content as Recipe;
                int id = product.IdRecipe;
                ModirateRecipe allMeals = new ModirateRecipe(id, _moderator);
                allMeals.Show();
                this.Hide();
            }
        }

        private void ListRecipe_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListRecipe recipe = new ListRecipe(_moderator);
            recipe.Show();
            this.Hide();
        }
    }
}
