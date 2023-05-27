using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DesktopCook
{
    public partial class NewRecipe : Window
    {
        private CookingBookEntities _context = new CookingBookEntities();
        private List<Comment> _comment = new List<Comment>();
        private int _recipe;
        private byte[] _image;
        private Users _users;
        public ListView listCom;
        public NewRecipe(int recipe, Users users)
        {
            listCom = ListComment;
            InitializeComponent();
            _recipe = recipe;
            _users = users;
            UpdateComment();
            FillImageBox();
        }
        /// <summary>
        /// Заполнение окна данными рецепта
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
        /// Обновление списка комментариев
        /// </summary>
        private void UpdateComment()
        {
            _comment = _context.Comment.ToList();
            _comment = _comment.Where(x => x.IdRecipe == _recipe).ToList();
            ListComment.ItemsSource = _comment;
        }
        /// <summary>
        /// Переходы между окнами 
        /// </summary>
        private void Main_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Glavnay glavnay = new Glavnay(_users);
            glavnay.Show();
            this.Hide();
        }

        private void PrivateAccount_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PrivateAccount privateAccount = new PrivateAccount(_users);
            privateAccount.Show();
            this.Hide();
        }

        private void Catalogue_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Catalog catalogue = new Catalog(_users);
            catalogue.Show();
            this.Hide();
        }

        private void MyRecipes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MyRecipes myRecipes = new MyRecipes(_users);
            myRecipes.Show();
            this.Hide();
        }

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }
        /// <summary>
        /// Добавление комментария при нажатие клавиши enter
        /// </summary>
        public static void AddComment(string name, int IdUser, int IdRecipe)
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Comment comment = new Comment();
                comment.NameComment = name;
                comment.DateComement = DateTime.Now;
                comment.IdUser = IdUser;
                comment.IdRecipe = IdRecipe;
                db.Comment.Add(comment);
                db.SaveChanges();
            }
        }
        private void Comment_KeyDown(object sender, KeyEventArgs e)
        {
            int id = _users.IdUser;
            if (e.Key == Key.Enter)
            {
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    AddComment(NameCom.Text, id, _recipe);
                }
            }
            UpdateComment();
        }
    }
}
