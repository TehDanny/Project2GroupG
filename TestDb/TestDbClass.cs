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
        [TestMethod]
        public void CreateUserOK()
        {
            User u = new Student();
            try
            {
                ManageDbProgram.Clear();
                ldm.Create(u);
                Assert.IsTrue(true);
                ldm.Delete(u.GetEmail());
            }
            catch(Exception e)
            {
                System.IO.File.WriteAllText(@"C:\Users\feroc\Downloads\Message.txt", e.Message);
                Assert.IsTrue(false);
            }
        }
    }
}
