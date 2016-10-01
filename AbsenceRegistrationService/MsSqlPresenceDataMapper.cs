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
            cmd = new SqlCommand(sqlCommandString, base.dbconn);
            cmd.ExecuteNonQuery();

            sqlCommandString = "INSERT INTO  Project2GroupGAutenticationUserTable (timeindex,email) VALUES('" + (lastindex + 1) + "','" + obj.GetEmail() + "');";
            cmd = new SqlCommand(sqlCommandString, base.dbconn);
            cmd.ExecuteNonQuery();

            base.Disconnect();
        }

        public UserPresence Read(string key)
        {
            int index=MsSqlOperations.GetTimeIndexFromEmail(key);
            base.Connect();
            sqlCommandString = "select dateaut,timeaut,mac,ip from Project2GroupGAutenticationTable where timeindex='" + index + "'";
            cmd = new SqlCommand(sqlCommandString, mc.GetSqlConnection());
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

        public void Update(UserPresence obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(string key)
        {
            throw new NotImplementedException();
        }
    }
}