using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DesktopCook;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddTesting()
        {
            byte[] image;
            string path = "d:\\cook\\salat.jpg";
            image = System.IO.File.ReadAllBytes(path);
            string name = "Салат";

            AddCategory.AddCategories(name, image);
            DesktopCook.Category actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                actual = db.Category.OrderByDescending(x => x.IdCategory).First();
                Assert.AreEqual(name, actual.NameCategory);
                CollectionAssert.AreEqual(image, actual.ImageCategory);
            }

        }
        [TestMethod]
        public void RemoveTesting()
        {
            int id;
            DesktopCook.Category actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Category select x.IdCategory).ToList().Last();
                actual = db.Category.Where(x => x.IdCategory == id).FirstOrDefault();
            }
            AddCategory.RemoveCategories(id);
            using (CookingBookEntities db = new CookingBookEntities())
            {
                actual = db.Category.Where(x => x.IdCategory == id).FirstOrDefault();
            }
            Assert.IsNull(actual);
        }
        [TestMethod]
        public void UpdateTesTisting()
        {
            int id;
            string namebefore;
            string nameafter = "Мясо";
            byte[] imageBefore;

            string pathAfter = "d:\\cook\\meat.jpg";
            byte[] imageAfter = System.IO.File.ReadAllBytes(pathAfter);
            DesktopCook.Category actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Category select x.IdCategory).ToList().Last();
                actual = db.Category.Where(x => x.IdCategory == id).FirstOrDefault();
                namebefore = actual.NameCategory;
                imageBefore = actual.ImageCategory;

                UpdateCategory.UpdateCategories(id, nameafter, imageAfter);
            }
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Category select x.IdCategory).ToList().Last();
                actual = db.Category.Where(x => x.IdCategory == id).FirstOrDefault();
                Assert.AreNotEqual(namebefore, actual.NameCategory);
                CollectionAssert.AreNotEqual(imageBefore, actual.ImageCategory);
            }
        }
        [TestMethod]
        public void ViewCatalog()
        {
            AddCategory addCategory = new AddCategory();
            int actual = addCategory.categ.Items.Count;
            int expected = 5;
            Assert.AreEqual(expected, actual);
        }
    }
}
