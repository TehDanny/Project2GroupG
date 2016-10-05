using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageDb
{
    public class ManageDbProgram
    {
        private static SqlConnection dbconn= new SqlConnection("Data Source = 10.140.12.14"/*ealdb1.eal.local*/+";Initial Catalog=EAL5_DB;Persist Security Info=true;User ID=EAL5_USR;Password=Huff05e05");
        private static string sqlCommandString="";
        private static SqlCommand cmd;
        private static SqlDataReader reader;
        public static void Clear()
        {
            ConnectDB();
            sqlCommandString = "DELETE FROM Project2GroupGUserTable;";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            cmd.ExecuteNonQuery();
            sqlCommandString = "DELETE FROM Project2GroupGAutenticationTable;";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            cmd.ExecuteNonQuery();
            CloseDB();
        }
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
        private static void DeleteTable(string table_name)
        {
            ConnectDB();
            sqlCommandString = "DROP TABLE "+table_name+";";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            cmd.ExecuteNonQuery();
            CloseDB();
        }
        private static void CreateTableUser(string table_name)
        {
            ConnectDB();
            sqlCommandString = "CREATE TABLE " + table_name + "(email varchar(100) NOT NULL,encryptedpassword varchar(100) NOT NULL, name varchar(100) ,surname varchar(100),type varchar(7),PRIMARY KEY (email));";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            cmd.ExecuteNonQuery();
            CloseDB();
        }
        private static void CreateTableAutentication(string table_name)
        {
            ConnectDB();
            sqlCommandString = "CREATE TABLE " + table_name + "(timeindex int IDENTITY(1,1)" +/*IDENTITY(0,1) means auto incremental value, starting by 0, incrementing by 1 each time*/" NOT NULL,dateaut date NOT NULL, timeaut time NOT NULL, mac varchar(17),ip varchar(15)PRIMARY KEY (timeindex));";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            cmd.ExecuteNonQuery();
            CloseDB();
        }
        private static void CreateTableAutenticationUser(string table_name)
        {
            ConnectDB();
            sqlCommandString = "CREATE TABLE " + table_name + "(timeindex int NOT NULL,email varchar(100) NOT NULL,PRIMARY KEY (timeindex,email),FOREIGN KEY (timeindex) REFERENCES Project2GroupGAutenticationTable(timeindex) ON DELETE CASCADE ON UPDATE CASCADE,FOREIGN KEY (email) REFERENCES Project2GroupGUserTable(email) ON DELETE CASCADE ON UPDATE CASCADE);";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            cmd.ExecuteNonQuery();
            CloseDB();
        }
        public static void Main(string[] args)
        {
            
            DeleteTable("Project2GroupGAutenticationUserTable");
            DeleteTable("Project2GroupGUserTable");
            DeleteTable("Project2GroupGAutenticationTable");
            CreateTableAutentication("Project2GroupGAutenticationTable");
            CreateTableUser("Project2GroupGUserTable");
            CreateTableAutenticationUser("Project2GroupGAutenticationUserTable");
            Clear();
            
            try
            {
                ConnectDB();
                sqlCommandString = "INSERT INTO  Project2GroupGUserTable VALUES('marc643f@edu.eal.dk','"+"password".GetHashCode().ToString()+"', 'marcello', 'feroce', 'student');";
                cmd = new SqlCommand(sqlCommandString, dbconn);
                cmd.ExecuteNonQuery();

                sqlCommandString = "INSERT INTO  Project2GroupGUserTable VALUES('marc535f@edu.eal.dk','" + "fakepassword".GetHashCode().ToString() + "', 'mars', 'attack', 'student');";
                cmd = new SqlCommand(sqlCommandString, dbconn);
                cmd.ExecuteNonQuery();

                DateTime myDateTime = DateTime.Now;
                string sqlFormattedTime = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                TimeSpan t = myDateTime.TimeOfDay;
                sqlCommandString = "INSERT INTO  Project2GroupGAutenticationTable (dateaut,timeaut, mac,ip) VALUES('"+ sqlFormattedTime+"','"+t+"','12345678901234567','192.168.0.1');";
                cmd = new SqlCommand(sqlCommandString, dbconn);
                cmd.ExecuteNonQuery();

                DateTime myDateTime2 = DateTime.Now;
                string sqlFormattedTime2 = myDateTime2.ToString("yyyy-MM-dd HH:mm:ss.fff");
                t = myDateTime2.TimeOfDay;
                sqlCommandString = "INSERT INTO  Project2GroupGAutenticationTable (dateaut,timeaut, mac,ip) VALUES('" + sqlFormattedTime2 + "','" +t+"', "+"'"+ null +"','192.168.0.2');";
                cmd = new SqlCommand(sqlCommandString, dbconn);
                cmd.ExecuteNonQuery();

                sqlCommandString = "INSERT INTO  Project2GroupGAutenticationUserTable (timeindex,email) VALUES('" +1 + "','marc535f@edu.eal.dk');";
                cmd = new SqlCommand(sqlCommandString, dbconn);
                cmd.ExecuteNonQuery();

                sqlCommandString = "INSERT INTO  Project2GroupGAutenticationUserTable (timeindex,email) VALUES('"+0+"','marc643f@edu.eal.dk');";
                cmd = new SqlCommand(sqlCommandString, dbconn);
                cmd.ExecuteNonQuery();

                sqlCommandString = "DELETE FROM  Project2GroupGAutenticationTable where timeindex='"+0+"';";
                cmd = new SqlCommand(sqlCommandString, dbconn);
                cmd.ExecuteNonQuery();

                CloseDB();

                ConnectDB();
                sqlCommandString = "select timeindex,dateaut, timeaut from Project2GroupGAutenticationTable";
                cmd = new SqlCommand(sqlCommandString, dbconn);
                reader = cmd.ExecuteReader();
                DateTime newD;
                TimeSpan newT;
                int ordinal = 0;
                int index = 0;
                while (reader.Read())
                {
                    ordinal= reader.GetOrdinal("dateaut");
                    newD = reader.GetDateTime(ordinal);
                    ordinal = reader.GetOrdinal("timeaut");
                    newT = reader.GetTimeSpan(ordinal);
                    index = (int)reader["timeindex"];
                    Console.WriteLine(newD.ToShortDateString());
                    Console.WriteLine(newT);
                    Console.WriteLine(index);
                }
                CloseDB();

                ConnectDB();
                sqlCommandString = "select timeindex from Project2GroupGAutenticationUserTable";
                cmd = new SqlCommand(sqlCommandString, dbconn);
                reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    index = (int)reader["timeindex"];
                    Console.WriteLine(index);
                }
                CloseDB();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            Console.ReadLine();
        }

        
    }
}
