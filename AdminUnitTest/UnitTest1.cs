using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DesktopCook;
using System.Linq;

namespace AdminUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddTestMethod()
        {
            byte[] image;
            string path = "D:\\cook\\salat.jpg";
            image = System.IO.File.ReadAllBytes(path);
            string name = "Салаты";
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
        public void RemoveCategoryTesting()
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
        public void ViewCatalog()
        {
            AddCategory addCategory = new AddCategory();
            int actual = addCategory.categ.Items.Count;
            int expected = 10;
            Assert.AreEqual(expected, actual);
        }
        //[TestMethod]
        //public void ChangeCategoryTesting()
        //{
        //    string sourceName;
        //    byte[] sourceImage;
        //    using (CookingBookEntities db = new CookingBookEntities())
        //    {
        //        DesktopCook.Category category;
        //        int id;
        //        id = (from x in db.Category select x.IdCategory).ToList().Last();
        //        category = db.Category.Where(x => x.IdCategory == id).First();
        //        sourceName = category.NameCategory;
        //        sourceImage = category.ImageCategory;

        //        byte[] image;
        //        string path = "d:\\1.png";
        //        image = System.IO.File.ReadAllBytes(path);
        //        UpdateCategory.UpdateCategories("test", image);
        //    }

        //    using (CookingBookEntities db = new CookingBookEntities())
        //    {
        //        DesktopCook.Category actualPosition;
        //        int id;
        //        id = (from x in db.Category select x.IdCategory).ToList().Last();
        //        actualPosition = db.Category.Where(x => x.IdCategory == id).First();

        //        string actualName = actualPosition.NameCategory;
        //        byte[] actualImage = actualPosition.ImageCategory;

        //        Assert.AreNotEqual(sourceName, actualName);
        //        Assert.AreNotEqual(sourceImage, actualImage);
        //    }


        //}
    }
}
