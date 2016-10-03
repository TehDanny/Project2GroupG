using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbsenceRegistrationService;
using ManageDb;

namespace TestDb
{
    [TestClass]
    public class TestDbClass
    {
        MsSqlLoginDataMapper ldm = new MsSqlLoginDataMapper();
        MsSqlPresenceDataMapper pdm = new MsSqlPresenceDataMapper();
        [TestMethod]
        public void CreateUserOK()
        {
            User u = new Student("", "", "", "", "");
            try
            {
                ManageDbProgram.Clear();
                ldm.Create(u);
                ldm.Delete(u.GetEmail());
                Assert.IsTrue(true);
            }
            catch(Exception e)
            {
                System.IO.File.WriteAllText(@"C:\Users\feroc\Downloads\Message.txt", e.Message);
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void CreateUserPresenceOK()
        {
            UserPresence up = new UserPresence(DateTime.Now,null,"12345678901234567","123456789012345");
            User u = new Student("","","","","");
            try
            {
                ManageDbProgram.Clear();
                ldm.Create(u);
                pdm.Create(up);
                pdm.Delete(up.GetEmail());
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText(@"C:\Users\feroc\Downloads\Message.txt", e.Message);
                Assert.IsTrue(false);
            }
        }
    }
}
