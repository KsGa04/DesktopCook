using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopCook
{
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
        /// <summary>
        /// Переходы между окнами
        /// </summary>
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
        /// <summary>
        /// При нажатии на блок ListView происходит переход на другое окно для проверки рецепта (модерация)
        /// </summary>
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
