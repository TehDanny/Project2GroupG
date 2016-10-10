using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbsenceRegistrationService;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;

namespace TestDb
{
    [TestClass]
    public class CheckSqlInjection:MsSqlOperations
    {
        private MsSqlLoginDataMapper ldm = new MsSqlLoginDataMapper();
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
        public void TrySql()
        {
            string email = "ferocemarcello@gmail.com" + (this.lastEmailNumber() + 1);
            Login_Component.User u = new Login_Component.User(email, "marcello", "feroce", "123456", "student");
            try
            {
                ldm.Create(u);
                email=email+"'OR'1'='1";
                string sqlCommandString = "select email from Project2GroupGUserTable where email='" + email + "'";
                base.Connect();
                cmd = new SqlCommand(sqlCommandString, base.GetSqlConnection());
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    File.AppendAllText(@"C:\users\feroc\MyTest.txt", (string)reader["email"]+Environment.NewLine);
                }
                base.Disconnect();
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }
    }
}
