using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbsenceRegistrationService;
using ManageDb;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;

namespace TestDb
{
    [TestClass]
    public class TestDbClass
    {
        private MsSqlLoginDataMapper ldm = new MsSqlLoginDataMapper();
        private MsSqlPresenceDataMapper pdm = new MsSqlPresenceDataMapper();
        private string email;
        private int lastEmailNumber()
        {
            string sqlCommandString = "select email FROM Project2GroupGUserTable where email LIKE " + "'ferocemarcello@gmail.com%' ORDER BY email desc;" + "";
            SqlDataReader reader;
            SqlConnection dbconn = new SqlConnection("Data Source = 10.140.12.14"/*ealdb1.eal.local*/+ ";Initial Catalog=EAL5_DB;Persist Security Info=true;User ID=EAL5_USR;Password=Huff05e05");
            dbconn.Open();
            SqlCommand cmd = new SqlCommand(sqlCommandString, dbconn);
            reader = cmd.ExecuteReader();

            int lastNumber = 0;
            string email = "";
            List<int> l = new List<int>();
            while (reader.Read())
            {
                email = (string)reader["email"];
                int index = "ferocemarcello@gmail.com".Length;
                string n = email.Substring(index);
                l.Add(int.Parse(n));
            }
            l.Sort();
            l.Reverse();
            lastNumber = l.IndexOf(0);
            dbconn.Close();
            return lastNumber;
        }
        [TestMethod]
        public void CreateUserOK()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456","student");
            try
            {
                ldm.Create(u);
                Assert.IsTrue(true);
            }
            catch(Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void CreateUserPresenceOK()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber()+1);
            UserPresence up = new UserPresence(DateTime.Now, email, "12345678901234567","123456789012345");
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce","123456","student");
            try
            {
                ldm.Create(u);
                pdm.Create(up);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void ReadAllUsersHistoryOK()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456", "student");
            ldm.Create(u);
            LinkedList<UserPresence> upl = new LinkedList<UserPresence>();
            DateTime dt = DateTime.Now;
            UserPresence up = new UserPresence(dt, u.GetEmail(), "lnn", "jln");
            pdm.Create(up);
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            u = new Login_Component.User(email, "marcello", "feroce", "123456", "student");
            ldm.Create(u);
            UserPresence up2 = new UserPresence(dt, u.GetEmail(), "lnn", "jln");
            pdm.Create(up2);
            try
            {
                upl=pdm.ReadAllUsersHistory();
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void ReadOneUserHistoryOK()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456", "student");
            ldm.Create(u);
            LinkedList<UserPresence> upl = new LinkedList<UserPresence>();
            for(int i = 0; i < 12; i++)
            {
                pdm.Create(new UserPresence(DateTime.Now, email, "34543345", "34543"));
            }
            try
            {
                upl = pdm.ReadUserHistory(email);
                bool correct = true;
                foreach(UserPresence up in upl)
                {
                    if (!(up.GetEmail().Equals(email)))
                    {
                        correct = false;
                    }
                }
                Assert.IsTrue(correct);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }
        
        [TestMethod]
        public void CreateUserPresenceTooLongMac()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = Path.Combine(pathUser, "Downloads");
            UserPresence up = new UserPresence(DateTime.Now, email, "123456789012345678", "123456789012345");
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456","student");
            try
            {
                ldm.Create(u);
                pdm.Create(up);
                Assert.IsTrue(false);
            }
            catch (SqlException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void CreateUserPresenceNullNotNullableValuesOk()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u = new Login_Component.User(null,null, null,null, null);
            try
            {
                ldm.Create(u);
                Assert.IsTrue(false);
            }
            catch (SqlException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void CreateUserPresenceNullValuesOk()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u = new Login_Component.User(email, null, null, "123456", null);
            try
            {
                ldm.Create(u);
                Assert.IsTrue(true);
            }
            catch (SqlException)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void CreateUserNullNotNullableValues()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = Path.Combine(pathUser, "Downloads");
            UserPresence up = new UserPresence(DateTime.Now, email, "123456789012345678", "123456789012345");
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456", "student");
            try
            {
                ldm.Create(u);
                pdm.Create(up);
                Assert.IsTrue(false);
            }
            catch (SqlException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void CreateUserPresenceNullNotNullableValues()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = Path.Combine(pathUser, "Downloads");
            UserPresence up = new UserPresence(default(DateTime), null,null, null);
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456", "student");
            try
            {
                ldm.Create(u);
                pdm.Create(up);
                Assert.IsTrue(false);
            }
            catch (SqlException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void CreateUserPresenceTooLongIp()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            UserPresence up = new UserPresence(DateTime.Now, email, "12345678901234567", "1234567890123456");
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456","student");
            try
            {
                ldm.Create(u);
                pdm.Create(up);
                Assert.IsTrue(false);
            }
            catch (SqlException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void ReadUserOK()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456","teacher");
            Login_Component.User secondU;
            try
            {
                ldm.Create(u);
                secondU = ldm.Read(email);
                Assert.IsTrue(secondU.UserEquals(u));
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void ReadNotExistingUser()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u;
            try
            {
                u=ldm.Read(email);
                Assert.IsTrue(u==null);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void ReadNullEmailUser()
        {
            Login_Component.User u;
            try
            {
                u = ldm.Read(null);
                Assert.IsTrue(u == null);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void ReadNullEmailUserPresence()
        {
            try
            {
                Assert.IsTrue(pdm.Read(null) == null);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void ReadNotExistingUserPresence()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u= new Login_Component.User(email, "marcello", "feroce", "123456", "student");
            try
            {
                ldm.Create(u);
                UserPresence p=pdm.Read(email);
                Assert.IsTrue(p == null);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void ReadPresenceOK()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456", "teacher");
            UserPresence up = new UserPresence(DateTime.Now,email,"12345678901234567","123456789012345");
            UserPresence up2;
            try
            {
                ldm.Create(u);
                pdm.Create(up);
                up2 = pdm.Read(email);
                Assert.IsTrue(up2.EqualsPresence(up));
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void DeleteUserOK()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456", "student");
            try
            {
                ldm.Create(u);
                ldm.Delete(u.GetEmail());
                Assert.IsTrue(ldm.Read(u.GetEmail())==null);
                
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void DeleteNotExistingUser()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            try
            {
                ldm.Delete(email);
                Assert.IsTrue(true);

            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void DeleteUserNullEmail()
        {
            try
            {
                ldm.Delete(null);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void DeleteUserPresenceNullEmail()
        {
            try
            {
                pdm.Delete(null);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void DeleteNotExistingUserPresence()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            try
            {
                pdm.Delete(email);
                Assert.IsTrue(true);

            }
            catch (SqlException)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void DeleteUserPresenceOK()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            UserPresence up = new UserPresence(DateTime.Now, email, "12345678901234567", "123456789012345");
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456", "student");
            try
            {
                ldm.Create(u);
                pdm.Create(up);
                pdm.Delete(u.GetEmail());
                up = pdm.Read(u.GetEmail());
                Assert.IsTrue(up == null);

            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void UpdateUserOK()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456", "student");
            Login_Component.User u2 = new Login_Component.User(email, "marcell", "feroc", "1234567", null);
            try
            {
                ldm.Create(u);
                ldm.Update(u2);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void UpdateNullUser()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456", "student");
            Login_Component.User u2 = new Login_Component.User(null, "marcell", "feroc", "1234567", null);
            try
            {
                ldm.Create(u);
                ldm.Update(u2);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void UpdateNotExistingUser()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456", "student");
            try
            {
                ldm.Create(u);
                email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
                Login_Component.User u2 = new Login_Component.User(email, "marcell", "feroc", "1234567", "lqeff!");
                ldm.Update(u2);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void UpdateNotExistingUserPresence()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            UserPresence up = new UserPresence(default(DateTime), email, "", "");
            try
            {
                pdm.Update(up);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void UpdateUserPresenceOK()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456", "student");
            UserPresence up = new UserPresence(DateTime.Now, email, "123456789", "12345456");
            
            try
            {
                ldm.Create(u);
                pdm.Create(up);
                UserPresence up2 = new UserPresence(DateTime.Now, email, "12345678932", "12341235456");
                pdm.Update(up2);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void UpdateNullUserPresence()
        {
            email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456", "student");
            UserPresence up = new UserPresence(DateTime.Now, email, "123456789", "12345456");

            try
            {
                ldm.Create(u);
                pdm.Create(up);
                UserPresence up2 = new UserPresence(DateTime.Now, null, "12345678932", "12341235456");
                pdm.Update(up2);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
    }
}
