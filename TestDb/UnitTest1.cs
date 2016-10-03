using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbsenceRegistrationService;
using ManageDb;

namespace TestDb
{
    [TestClass]
    public class UnitTest1
    {
        MsSqlPresenceDataMapper ldm = new MsSqlPresenceDataMapper();
        [TestMethod]
        public void TestMethod1()
        {
            UserPresence up = new UserPresence(DateTime.Now, "ferocemarcello@gmail.com", "12345678901234567", "123456789012345");
            try
            {
                ManageDbProgram.Clear();
                ldm.Create(up);
                Assert.IsTrue(true);
                ldm.Delete(up.GetEmail());
            }
            catch(Exception e)
            {
                System.IO.File.WriteAllText(@"C:\Users\feroc\Downloads\Message.txt", e.Message);
                Assert.IsTrue(false);
            }
        }
    }
}
