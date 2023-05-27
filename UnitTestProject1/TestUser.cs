using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopCook;
using System.Linq;
using System;

namespace UnitTestProject1
{
    /// <summary>
    /// Сводное описание для TestUser
    /// </summary>
    [TestClass]
    public class TestUser
    {
        [TestMethod]
        public void PrivateAccountUser()
        {
            int id;
            string mail = "ks@mail.ru";
            string pass = "71717171";
            string nik = "nikname";
            DateTime dateTime = new DateTime(2003, 12, 05);

            string mailBefore;
            string passBefore;
            string nikBefore;
            DateTime dateTimeBefore;
            DesktopCook.Users actual;
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Users select x.IdUser).ToList().Last();
                actual = db.Users.Where(x => x.IdUser == id).FirstOrDefault();
                mailBefore = actual.Mail;
                passBefore = actual.Password;
                nikBefore = actual.NikName;
                dateTimeBefore = Convert.ToDateTime(actual.DateOfBirth);

                PrivateAccount.SaveUser(id, mail, pass, nik, dateTime);
            }
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Users select x.IdUser).ToList().Last();
                actual = db.Users.Where(x => x.IdUser == id).FirstOrDefault();
                Assert.AreNotEqual(mailBefore, actual.Mail);
                Assert.AreNotEqual(passBefore, actual.Password);
                Assert.AreNotEqual(nikBefore, actual.NikName);
                Assert.AreNotEqual(dateTimeBefore, actual.DateOfBirth);
            }
        }
        [TestMethod]
        public void RegistrationUser()
        {
            int id;
            string mail = "testMail";
            string pass = "passMail";
            DesktopCook.Users actual;
            Registration.Reg(mail, pass);
            using (CookingBookEntities db = new CookingBookEntities())
            {
                id = (from x in db.Users select x.IdUser).ToList().Last();
                actual = db.Users.Where(x => x.IdUser == id).FirstOrDefault();
                Assert.AreEqual(mail, actual.Mail);
                Assert.AreEqual(pass, actual.Password);
            }
        }
    }
}
