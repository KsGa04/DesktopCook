using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopCook;
using System.Linq;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class TestMeal
    {
        [TestMethod]
        public void AddTesting()
        {
            byte[] image;
            string path = "d:\\cook\\meat.jpg";
            image = System.IO.File.ReadAllBytes(path);
            string name = "Мясной рулет";
            string description = "Варёный, варёно-копчёный или сырокопчёный мясной продукт промышленного и домашнего производства свёрнутой трубкообразной формы из посоленных тазобедренных и плечелопаточных частей свиной, говяжьей или бараньей туши";
            int id = 3;
            AddMeals.AddMeal(name, description, image, id);
            DesktopCook.Meal actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                actual = db.Meal.OrderByDescending(x => x.IdMeal).First();
                Assert.AreEqual(name, actual.NameMeal);
                Assert.AreEqual(description, actual.DescriptionMeal);
                Assert.AreEqual(id, actual.IdCategory);
                CollectionAssert.AreEqual(image, actual.ImageMeal);
            }

        }
        [TestMethod]
        public void RemoveTesting()
        {
            int id;
            DesktopCook.Meal actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Meal select x.IdMeal).ToList().Last();
                actual = db.Meal.Where(x => x.IdMeal == id).FirstOrDefault();
            }
            AddMeals.RemoveMeals(id);
            using (CookingBookEntities db = new CookingBookEntities())
            {
                actual = db.Meal.Where(x => x.IdMeal == id).FirstOrDefault();
            }
            Assert.IsNull(actual);
        }
        [TestMethod]
        public void UpdateTesting()
        {
            int id;
            string namebefore;
            byte[] imageBefore;
            string descriptionBefore;
            int idCategBefore;

            string nameafter = "Шашлык";
            string descriptionAfter = "Варёный, варёно-копчёный или сырокопчёный мясной продукт промышленного и домашнего производства свёрнутой трубкообразной формы из посоленных тазобедренных и плечелопаточных частей свиной, говяжьей или бараньей туши";
            int idCategAfter = 3;
            string pathAfter = "d:\\cook\\meat.jpg";
            byte[] imageAfter = System.IO.File.ReadAllBytes(pathAfter);
            DesktopCook.Meal actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Meal select x.IdMeal).ToList().Last();
                actual = db.Meal.Where(x => x.IdMeal == id).FirstOrDefault();
                namebefore = actual.NameMeal;
                imageBefore = actual.ImageMeal;
                descriptionBefore = actual.DescriptionMeal;
                idCategBefore = Convert.ToInt32(actual.IdCategory);

                UpdateMeals.UpdateMeal(id, nameafter, descriptionAfter, imageAfter, idCategAfter);
            }
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Meal select x.IdMeal).ToList().Last();
                actual = db.Meal.Where(x => x.IdMeal == id).FirstOrDefault();
                Assert.AreNotEqual(namebefore, actual.NameMeal);
                Assert.AreNotEqual(descriptionBefore, actual.DescriptionMeal);
                Assert.AreNotEqual(idCategBefore, actual.IdCategory);
                CollectionAssert.AreNotEqual(imageBefore, actual.ImageMeal);
            }
        }
        [TestMethod]
        public void ViewAddCatalog()
        {
            AddMeals addMeals = new AddMeals();
            int actual = addMeals.meal.Items.Count;
            int expected = 3;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ViewAllMeals()
        {
            DesktopCook.Users users;
            int id = 1;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                users = db.Users.OrderByDescending(x => x.IdUser).First();
            }
            AllMeals catalog = new AllMeals(1,users);
            int actual = catalog.ListMeal.Items.Count;
            int expected = 1;
            Assert.AreEqual(expected, actual);
        }
    }
}