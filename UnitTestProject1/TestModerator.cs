using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopCook;
using System.Linq;
using System;

namespace UnitTestProject1
{
    /// <summary>
    /// Сводное описание для TestModerator
    /// </summary>
    [TestClass]
    public class TestModerator
    {
        [TestMethod]
        public void AddTesting()
        {
            string mail = "ks@mail.ru";
            string pass = "71717171";
            string nik = "ksga";
            DateTime dateTime = new DateTime(2004, 08, 10);
            int idCateg = 2;
            AddWorker.AddModer(mail, pass, nik, dateTime, idCateg);
            DesktopCook.Moderator actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                actual = db.Moderator.OrderByDescending(x => x.IdModerator).First();
                Assert.AreEqual(mail, actual.Mail);
                Assert.AreEqual(pass, actual.Password);
                Assert.AreEqual(idCateg, actual.IdCategory);
                Assert.AreEqual(nik, actual.NikName);
                Assert.AreEqual(dateTime, actual.DateOfBirth);
            }

        }
        [TestMethod]
        public void RemoveTesting()
        {
            int id;
            DesktopCook.Moderator actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Moderator select x.IdModerator).ToList().Last();
                actual = db.Moderator.Where(x => x.IdModerator == id).FirstOrDefault();
            }
            AllModerator.RemoveModer(id);
            using (CookingBookEntities db = new CookingBookEntities())
            {
                actual = db.Moderator.Where(x => x.IdModerator == id).FirstOrDefault();
            }
            Assert.IsNull(actual);
        }
        [TestMethod]
        public void UpdateTesting()
        {
            int id;
            string mail = "ar@mail.ru";
            string pass = "12345678";
            string nik = "nikname";
            DateTime dateTime = new DateTime(2003, 12, 05);
            int idCateg = 2;

            string mailBefore;
            string passBefore;
            string nikBefore;
            DateTime dateTimeBefore;
            int idCategBefore;
            DesktopCook.Moderator actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Moderator select x.IdModerator).ToList().Last();
                actual = db.Moderator.Where(x => x.IdModerator == id).FirstOrDefault();
                mailBefore = actual.Mail;
                passBefore = actual.Password;
                nikBefore = actual.NikName;
                dateTimeBefore = Convert.ToDateTime(actual.DateOfBirth);
                idCategBefore = Convert.ToInt32(actual.IdCategory);

                UpdateModerator.UpdateModer(id, mail, pass, nik, dateTime, idCateg);
            }
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Moderator select x.IdModerator).ToList().Last();
                actual = db.Moderator.Where(x => x.IdModerator == id).FirstOrDefault();
                Assert.AreNotEqual(mailBefore, actual.Mail);
                Assert.AreNotEqual(passBefore, actual.Password);
                Assert.AreNotEqual(nikBefore, actual.NikName);
                Assert.AreNotEqual(dateTimeBefore, actual.DateOfBirth);
                Assert.AreNotEqual(idCategBefore, actual.IdCategory);
            }
        }
        //[TestMethod]
        //public void ViewAllModer()
        //{
        //    AllModerator moderator = new AllModerator();
        //    int actual = moderator.moder.Items.Count;
        //    int expected = 2;
        //    Assert.AreEqual(expected, actual);
        //}
        [TestMethod]
        public void PrivateAccountModer()
        {
            int id;
            string mail = "ar@mail.ru";
            string pass = "12345678";
            string nik = "nikname";
            DateTime dateTime = new DateTime(2003, 12, 05);

            string mailBefore;
            string passBefore;
            string nikBefore;
            DateTime dateTimeBefore;
            DesktopCook.Moderator actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Moderator select x.IdModerator).ToList().Last();
                actual = db.Moderator.Where(x => x.IdModerator == id).FirstOrDefault();
                mailBefore = actual.Mail;
                passBefore = actual.Password;
                nikBefore = actual.NikName;
                dateTimeBefore = Convert.ToDateTime(actual.DateOfBirth);

                PrivateAccountModerator.SaveModer(id, mail, pass, nik, dateTime);
            }
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Moderator select x.IdModerator).ToList().Last();
                actual = db.Moderator.Where(x => x.IdModerator == id).FirstOrDefault();
                Assert.AreNotEqual(mailBefore, actual.Mail);
                Assert.AreNotEqual(passBefore, actual.Password);
                Assert.AreNotEqual(nikBefore, actual.NikName);
                Assert.AreNotEqual(dateTimeBefore, actual.DateOfBirth);
            }
        }
    }
}
