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
            sqlCommandString = "INSERT INTO  Project2GroupGAutenticationTable (dateaut,timeaut, mac,ip) VALUES('" + obj.GetDate().ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + obj.GetDate().TimeOfDay + "', " + "'" + obj.GetMac() + "','"+obj.GetIp()+"');";
            this.DoVoidCommand(this.sqlCommandString);
            int lastindex = MsSqlOperations.LastIndexAutenticationTable();
            sqlCommandString = "INSERT INTO  Project2GroupGAutenticationUserTable (timeindex,email) VALUES('" + lastindex + "','" + obj.GetEmail() + "');";
            this.DoVoidCommand(this.sqlCommandString);
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
        public void Update(UserPresence obj)//update last autentication time for that user
        {
            int index = MsSqlOperations.GetLastTimeIndexFromEmail(obj.GetEmail());
            this.sqlCommandString = "UPDATE  Project2GroupGAutenticationTable SET dateaut='" + obj.GetDate().ToString("yyyy-MM-dd HH:mm:ss.fff") + "',timeaut='" + obj.GetDate().TimeOfDay + "', mac='" + obj.GetMac() + "',ip='" + obj.GetIp() + "' WHERE timeindex='"+index+"';";
            this.DoVoidCommand(this.sqlCommandString);
        }

        public void Delete(string key)
        {
            int index = MsSqlOperations.GetLastTimeIndexFromEmail(key);
            this.sqlCommandString = "DELETE FROM  Project2GroupGAutenticationTable WHERE timeindex='" + index + "';";
            this.DoVoidCommand(this.sqlCommandString);
        }
        private void DoVoidCommand(string commandString)
        {
            base.Connect();
            cmd = new SqlCommand(commandString, base.GetSqlConnection());
            cmd.ExecuteNonQuery();
            base.Disconnect();
        }
    }
}