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
    /// Логика взаимодействия для ModirateComment.xaml
    /// </summary>
    public partial class ModirateComment : Window
    {
        public Moderator _moderator;
        public int _id;
        public ModirateComment(Moderator moderator)
        {
            InitializeComponent();
            _moderator = moderator;
            ListViewLoad();
            TextComment.IsEnabled = false;
            idcom.IsEnabled = false;
        }
        public void ListViewLoad()
        {
            using (CookingBookEntities db = new CookingBookEntities())
            {
                var comments = db.Comment.ToList();

                Moder.ItemsSource = comments;
            }
        }
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
        private void RemoveRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (Moder.SelectedIndex >= 0)
            {
                var result = MessageBox.Show("Вы точно хотите удалить этот рецепт?", "Удалить", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    var item = Moder.SelectedItem as Comment;
                    int id = item.IdComment;
                    using (CookingBookEntities db = new CookingBookEntities())
                    {
                        Comment comment = db.Comment.Where(x => x.IdComment == id).FirstOrDefault();

                        db.Comment.Remove(comment);
                        db.SaveChanges();
                        ListViewLoad();
                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали ни один элемент");
                }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            TextComment.IsEnabled = true;
            var item = Moder.SelectedItem as Comment;
            int id = item.IdComment;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                Comment comment = db.Comment.FirstOrDefault(x => x.IdComment == id);
                TextComment.Text = comment.NameComment;
                idcom.Text =Convert.ToString(comment.IdComment);
                
            }
        }

        private void Saves_Click(object sender, RoutedEventArgs e)
        {
            _id = Convert.ToInt32(idcom.Text);
            if (TextComment.Text != "")
            {
                using (CookingBookEntities db = new CookingBookEntities())
                {
                    Comment comment = db.Comment.FirstOrDefault(x => x.IdComment == _id);
                    comment.NameComment = TextComment.Text;
                    
                    db.SaveChanges();
                }
                MessageBox.Show("Запись обновлена");
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
            ListViewLoad();
        }
    }
}
