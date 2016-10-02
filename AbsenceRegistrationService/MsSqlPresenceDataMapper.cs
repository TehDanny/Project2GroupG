using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AbsenceRegistrationService
{
    public class MsSqlPresenceDataMapper :MsSqlConnect,IPresenceDataMapper
    {
        private string sqlCommandString = "";
        private SqlCommand cmd;
        private SqlDataReader reader;

        public LinkedList<UserPresence> ReadUserHistory(string key)
        {
            throw new NotImplementedException();
        }

        public LinkedList<LinkedList<UserPresence>> ReadAllUsersHistory()
        {
            throw new NotImplementedException();
        }

        public void Create(UserPresence obj)
        {
            base.Connect();

            int lastindex=MsSqlOperations.LastIndexAutenticationTable();

            sqlCommandString = "INSERT INTO  Project2GroupGAutenticationTable (dateaut,timeaut, mac,ip) VALUES('" + obj.GetDate().ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + obj.GetDate().TimeOfDay + "', " + "'" + obj.GetMac() + "','"+obj.GetIp()+"');";
            cmd = new SqlCommand(sqlCommandString, base.GetSqlConnection());
            cmd.ExecuteNonQuery();

            sqlCommandString = "INSERT INTO  Project2GroupGAutenticationUserTable (timeindex,email) VALUES('" + (lastindex + 1) + "','" + obj.GetEmail() + "');";
            cmd = new SqlCommand(sqlCommandString, base.GetSqlConnection());
            cmd.ExecuteNonQuery();

            base.Disconnect();
        }

        public UserPresence Read(string key)//last autentication
        {
            int index=MsSqlOperations.GetLastTimeIndexFromEmail(key);
            base.Connect();
            sqlCommandString = "select dateaut,timeaut,mac,ip from Project2GroupGAutenticationTable where timeindex='" + index + "'";
            cmd = new SqlCommand(sqlCommandString, base.GetSqlConnection());
            reader = cmd.ExecuteReader();
            DateTime dt = new DateTime();
            TimeSpan ts = new TimeSpan();
            string mac = "";
            string ip = "";
            if (reader.Read())
            {
                dt = (DateTime)reader["dateaut"];
                ts = (TimeSpan)reader["timeaut"];
                mac = (string)reader["mac"];
                ip = (string)reader["ip"];
            }
            base.Disconnect();
            dt.Add(ts);
            UserPresence up = new UserPresence(dt, key, mac, ip);
            return up;
        }

        public void Update(UserPresence obj)//update the autentication time for that user
        {
            int index = MsSqlOperations.GetLastTimeIndexFromEmail(obj.GetEmail());
            base.Connect();
            this.sqlCommandString = "UPDATE  Project2GroupGAutenticationTable SET dateaut='" + obj.GetDate().ToString("yyyy-MM-dd HH:mm:ss.fff") + "',timeaut='" + obj.GetDate().TimeOfDay + "', mac='" + obj.GetMac() + "',ip='" + obj.GetIp() + "' WHERE timeindex='"+index+"';";
            cmd = new SqlCommand(sqlCommandString, base.GetSqlConnection());
            cmd.ExecuteNonQuery();
            base.Disconnect();
        }

        public void Delete(string key)
        {
            int index = MsSqlOperations.GetLastTimeIndexFromEmail(key);
            base.Connect();
            this.sqlCommandString = "DELETE FROM  Project2GroupGAutenticationTable WHERE timeindex='" + index + "';";
            cmd = new SqlCommand(sqlCommandString, base.GetSqlConnection());
            cmd.ExecuteNonQuery();
            base.Disconnect();
        }
    }
}