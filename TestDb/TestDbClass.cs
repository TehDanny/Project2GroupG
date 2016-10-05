using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbsenceRegistrationService;
using ManageDb;
using System.Data.SqlClient;
using System.IO;

namespace TestDb
{
    [TestClass]
    public class TestDbClass
    {
        private MsSqlLoginDataMapper ldm = new MsSqlLoginDataMapper();
        private MsSqlPresenceDataMapper pdm = new MsSqlPresenceDataMapper();
        [TestMethod]
        public void CreateUserOK()
        {
            Login_Component.User u = new Login_Component.User("ferocemarcello@gmail.com", "marcello", "feroce", "123456","student");
            try
            {
                ldm.Create(u);
                Assert.IsTrue(true);
            }
            catch(Exception e)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void CreateUserPresenceOK()
        {
            UserPresence up = new UserPresence(DateTime.Now, "ferocemarcello2@gmail.com", "12345678901234567","123456789012345");
            Login_Component.User u = new Login_Component.User("ferocemarcello2@gmail.com", "marcello", "feroce","123456","student");
            try
            {
                ldm.Create(u);
                pdm.Create(up);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void CreateUserPresenceTooLongMac()
        {
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = Path.Combine(pathUser, "Downloads");
            UserPresence up = new UserPresence(DateTime.Now, "ferocemarcello3@gmail.com", "123456789012345678", "123456789012345");
            Login_Component.User u = new Login_Component.User("ferocemarcello3@gmail.com", "marcello", "feroce", "123456","student");
            try
            {
                ldm.Create(u);
                pdm.Create(up);
                Assert.IsTrue(false);
            }
            catch (SqlException e)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void CreateUserPresenceTooLongIp()
        {
            UserPresence up = new UserPresence(DateTime.Now, "ferocemarcello4@gmail.com", "12345678901234567", "1234567890123456");
            Login_Component.User u = new Login_Component.User("ferocemarcello4@gmail.com", "marcello", "feroce", "123456","student");
            try
            {
                ldm.Create(u);
                pdm.Create(up);
                Assert.IsTrue(false);
            }
            catch (SqlException e)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void ReadUserOK()
        {
            Login_Component.User u = new Login_Component.User("ferocemarcello5@gmail.com", "marcello", "feroce", "123456","teacher");
            Login_Component.User secondU;
            try
            {
                ldm.Create(u);
                secondU = ldm.Read("ferocemarcello5@gmail.com");
                Assert.IsTrue(secondU.UserEquals(u));
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void ReadPresenceOK()
        {
            Login_Component.User u = new Login_Component.User("ferocemarcello6@gmail.com", "marcello", "feroce", "123456", "teacher");
            UserPresence up = new UserPresence(DateTime.Now,"ferocemarcello6@gmail.com","12345678901234567","123456789012345");
            UserPresence up2;
            try
            {
                ldm.Create(u);
                pdm.Create(up);
                up2 = pdm.Read("ferocemarcello6@gmail.com");
                Assert.IsTrue(up2.EqualsPresence(up));
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void DeleteUserOK()
        {
            Login_Component.User u = new Login_Component.User("ferocemarcello7@gmail.com", "marcello", "feroce", "123456", "student");
            try
            {
                ldm.Create(u);
                ldm.Delete(u.GetEmail());
                Assert.IsTrue(ldm.Read(u.GetEmail())==null);
                
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void DeleteUserPresenceOK()
        {
            UserPresence up = new UserPresence(DateTime.Now, "ferocemarcello8@gmail.com", "12345678901234567", "123456789012345");
            Login_Component.User u = new Login_Component.User("ferocemarcello8@gmail.com", "marcello", "feroce", "123456", "student");
            try
            {
                ldm.Create(u);
                pdm.Create(up);
                pdm.Delete(u.GetEmail());
                up = pdm.Read(u.GetEmail());
                Assert.IsTrue(up == null);

            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }
    }
}
