using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageDb
{
    class ManageDb
    {
        private static SqlConnection dbconn= new SqlConnection("Data Source = ealdb1.eal.local;Initial Catalog=EAL5_DB;Persist Security Info=true;User ID=EAL5_USR;Password=Huff05e05");
        private static string sqlCommandString="";
        private static SqlCommand cmd;
        private static SqlDataReader reader;
        private static SqlParameter parameter;
        private static void ConnectDB()
        {
            
            try
            {
                dbconn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static void CloseDB()
        {
            dbconn.Close();
        }
        private static void CreateTableUser(string table_name)
        {
            ConnectDB();
            sqlCommandString = "CREATE TABLE " + table_name + "(email varchar(100) NOT NULL,encryptedpassword varchar(100) NOT NULL, name varchar(100) ,surname varchar(100),type varchar(7),PRIMARY KEY (email));";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            cmd.ExecuteNonQuery();
            CloseDB();
        }
        /*private static void CreateTableDayTime(string table_name)
        {
            ConnectDB();
            sqlCommandString = "CREATE TABLE " + table_name + "(email varchar(100) NOT NULL,encryptedpassword varchar(100) NOT NULL, name varchar(100) ,surname varchar(100),type varchar(7),PRIMARY KEY (email));";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            cmd.ExecuteNonQuery();
            CloseDB();
        }*/
        public static void Main(string[] args)
        {
            
            try
            {
                CreateTableUser("Project2GroupGUserTable");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                CreateTableDayTime("Project2GroupGUserTable");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }

        
    }
}
