using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopCook;
using System.Linq;
using System;

namespace UnitTestProject1
{
    /// <summary>
    /// Сводное описание для TestRecipe
    /// </summary>
    [TestClass]
    public class TestRecipe
    {
        [TestMethod]
        public void AddTesting()
        {
            byte[] image;
            string path = "d:\\cook\\borsh.jpg";
            image = System.IO.File.ReadAllBytes(path);
            string name = "Борщ (тест)";
            string ingr = "тестовый рецепт";
            string desc = "тестовый рецепт";
            int idCateg = 1;
            int idMeal = 1;
            int idUser = 1;


            AddRecipe.AddRecipes(name, ingr, desc, image, idCateg, idMeal, idUser, false);
            DesktopCook.Recipe actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                actual = db.Recipe.OrderByDescending(x => x.IdRecipe).First();
                Assert.AreEqual(name, actual.NameRecipe);
                Assert.AreEqual(ingr, actual.Ingredient);
                Assert.AreEqual(desc, actual.Description);
                Assert.AreEqual(idCateg, actual.IdCategory);
                Assert.AreEqual(idMeal, actual.IdMeal);
                Assert.AreEqual(idUser, actual.IdUser);
                CollectionAssert.AreEqual(image, actual.ImageRecipe);
            }

        }
        [TestMethod]
        public void RemoveTesting()
        {
            int id;
            DesktopCook.Recipe actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Recipe select x.IdRecipe).ToList().Last();
                actual = db.Recipe.Where(x => x.IdRecipe == id).FirstOrDefault();
            }
            MyRecipes.RemoveRecipes(id);
            using (CookingBookEntities db = new CookingBookEntities())
            {
                actual = db.Recipe.Where(x => x.IdRecipe == id).FirstOrDefault();
            }
            Assert.IsNull(actual);
        }
        [TestMethod]
        public void UpdateTesting()
        {
            int id;
            byte[] image;
            string path = "d:\\cook\\moxito.jpg";
            image = System.IO.File.ReadAllBytes(path);
            string name = "Мохито (тест)";
            string ingr = "тестовый рецепт";
            string desc = "тестовый рецепт";
            int idCateg = 2;
            int idMeal = 1;
            int idUser = 1;

            string nameBefore;
            string inrgBefore;
            string descBefore;
            byte[] imageBefore;
            DesktopCook.Recipe actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Recipe select x.IdRecipe).ToList().Last();
                actual = db.Recipe.Where(x => x.IdRecipe == id).FirstOrDefault();
                nameBefore = actual.NameRecipe;
                inrgBefore = actual.Ingredient;
                descBefore = actual.Description;
                imageBefore = actual.ImageRecipe;

                UpdateMyRecipe.UpdateRecipe(id, name, ingr, desc, image, idCateg, idMeal, idUser, false);
            }
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Recipe select x.IdRecipe).ToList().Last();
                actual = db.Recipe.Where(x => x.IdCategory == id).FirstOrDefault();
                Assert.AreNotEqual(nameBefore, actual.NameRecipe);
                Assert.AreNotEqual(inrgBefore, actual.Ingredient);
                Assert.AreNotEqual(descBefore, actual.Description);
                CollectionAssert.AreNotEqual(imageBefore, actual.ImageRecipe);
            }
        }
        [TestMethod]
        public void ApproveDisRecipe()
        {
            int id;
            byte[] image;
            string path = "d:\\cook\\moxito.jpg";
            image = System.IO.File.ReadAllBytes(path);
            string name = "Мохито (тест)";
            string ingr = "тестовый рецепт";
            string desc = "тестовый рецепт";
            bool moder = true;

            string nameBefore;
            string inrgBefore;
            string descBefore;
            byte[] imageBefore;
            bool moderBefore;
            DesktopCook.Recipe actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Recipe select x.IdRecipe).ToList().Last();
                actual = db.Recipe.Where(x => x.IdRecipe == id).FirstOrDefault();
                nameBefore = actual.NameRecipe;
                inrgBefore = actual.Ingredient;
                descBefore = actual.Description;
                imageBefore = actual.ImageRecipe;
                moderBefore = actual.Moder;

                ModirateRecipe.ApproveDisRecipe(id, name, ingr, desc, moder);
            }
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Recipe select x.IdRecipe).ToList().Last();
                actual = db.Recipe.Where(x => x.IdCategory == id).FirstOrDefault();
                Assert.AreNotEqual(moderBefore, actual.Moder);
            }
        }
        [TestMethod]
        public void ViewRecipe()
        {
            DesktopCook.Users users;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                users = db.Users.Where(x => x.IdUser == 1).FirstOrDefault();
            }
            MyRecipes myRecipes = new MyRecipes(users);
            int actual = myRecipes.listRecipe.Items.Count;
            int expected = 3;
            Assert.AreEqual(expected, actual);
        }
    }
}
