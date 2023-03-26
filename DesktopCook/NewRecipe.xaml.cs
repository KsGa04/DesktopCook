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
    /// Логика взаимодействия для NewRecipe.xaml
    /// </summary>
    public partial class NewRecipe : Window
    {
        private CookingBookEntities _context = new CookingBookEntities();
        private List<Comment> _comment = new List<Comment>();
        private int _recipe;
        private byte[] _image;
        private Users _users;
        public NewRecipe(int recipe, Users users)
        {
            InitializeComponent();
            _recipe = recipe;
            _users = users;
            UpdateComment();
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
        private void UpdateComment()
        {
            _comment = _context.Comment.ToList();
            _comment = _comment.Where(x => x.IdRecipe == _recipe).ToList();
            ListComment.ItemsSource = _comment;
        }
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Glavnay glavnay = new Glavnay(_users);
            glavnay.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            PrivateAccount privateAccount = new PrivateAccount(_users);
            privateAccount.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            Catalog catalogue = new Catalog(_users);
            catalogue.Show();
            this.Hide();
        }

        private void TextBlock_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            MyRecipes myRecipes = new MyRecipes(_users);
            myRecipes.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            int id = _users.IdUser;
            if (e.Key == Key.Enter)
            {
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    Comment comment = new Comment();
                    comment.NameComment = NameCom.Text;
                    comment.DateComement = DateTime.Now;
                    comment.IdUser = id;
                    comment.IdRecipe = _recipe;
                    db.Comment.Add(comment);
                    db.SaveChanges();
                }
            }
            UpdateComment();
        }
    }
}
