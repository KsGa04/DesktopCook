using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopCook;
using System.Linq;

namespace UnitTestProject1
{
    /// <summary>
    /// Сводное описание для TestComment
    /// </summary>
    [TestClass]
    public class TestComment
    {
        [TestMethod]
        public void AddTesting()
        {
            string name = "Так себе рецепт";
            int IdUser = 1;
            int IdRecipe = 1;

            NewRecipe.AddComment(name, IdUser, IdRecipe);
            DesktopCook.Comment actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                actual = db.Comment.OrderByDescending(x => x.IdComment).First();
                Assert.AreEqual(name, actual.NameComment);
                Assert.AreEqual(IdUser, actual.IdUser);
                Assert.AreEqual(IdRecipe, actual.IdRecipe);
            }

        }
        [TestMethod]
        public void RemoveTesting()
        {
            int id;
            DesktopCook.Comment actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Comment select x.IdComment).ToList().Last();
                actual = db.Comment.Where(x => x.IdComment == id).FirstOrDefault();
            }
            ModirateComment.RemoveComment(id);
            using (CookingBookEntities db = new CookingBookEntities())
            {
                actual = db.Comment.Where(x => x.IdComment == id).FirstOrDefault();
            }
            Assert.IsNull(actual);
        }
        [TestMethod]
        public void UpdateTesTisting()
        {
            int id;
            string name = "Интересный рецепт";
            string nameBefore;
            DesktopCook.Comment actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Comment select x.IdComment).ToList().Last();
                actual = db.Comment.Where(x => x.IdComment == id).FirstOrDefault();
                nameBefore = actual.NameComment;

                ModirateComment.UpdateComment(id, name);
            }
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Comment select x.IdComment).ToList().Last();
                actual = db.Comment.Where(x => x.IdComment == id).FirstOrDefault();
                Assert.AreNotEqual(nameBefore, actual.NameComment);
            }
        }
        //[TestMethod]
        //public void ViewComment()
        //{
        //    int recipe = 1;
        //    DesktopCook.Users users;
        //    using (CookingBookEntities db = new CookingBookEntities())
        //    {
        //        users = db.Users.OrderByDescending(x => x.IdUser).First();
        //    }
        //    NewRecipe newRecipe = new NewRecipe(recipe, users);
        //    int actual = newRecipe.listCom.Items.Count;
        //    int expected = 3;
        //    Assert.AreEqual(expected, actual);
        //}
    }
}
