using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AbsenceRegistrationService
{
    public class MsSqlPresenceDataMapper :MsSqlOperations,IPresenceDataMapper
    {
        public LinkedList<UserPresence> ReadUserHistory(string key)
        {
            LinkedList<int> indexes = base.GetTimeIndexesFromEmail(key);
            LinkedList<UserPresence> userPresences = new LinkedList<UserPresence>();
            string indexesString = string.Join(",", indexes.ToArray());
            sqlCommandString = "select dateaut,timeaut,mac,ip from Project2GroupGAutenticationTable where timeindex in+'" + indexesString + "'";
            base.Connect();
            cmd = new SqlCommand(sqlCommandString, base.GetSqlConnection());
            reader = cmd.ExecuteReader();
            DateTime dt = new DateTime();
            TimeSpan ts = new TimeSpan();
            string mac = "";
            string ip = "";
            while (reader.Read())
            {
                dt = (DateTime)reader["dateaut"];
                ts = (TimeSpan)reader["timeaut"];
                mac = (string)reader["mac"];
                ip = (string)reader["ip"];
                dt.Add(ts);
                userPresences.AddLast(new UserPresence(dt, key, mac, ip));
            }
            base.Disconnect();
            return userPresences;
        }

        public LinkedList<UserPresence> ReadAllUsersHistory()
        {
            LinkedList<UserPresence> userPresences = new LinkedList<UserPresence>();
            sqlCommandString = "select email,dateaut,timeaut,mac,ip from Project2GroupGAutenticationTable";
            base.Connect();
            cmd = new SqlCommand(sqlCommandString, base.GetSqlConnection());
            reader = cmd.ExecuteReader();
            DateTime dt = new DateTime();
            TimeSpan ts = new TimeSpan();
            string mac = "";
            string ip = "";
            string email = "";
            while (reader.Read())
            {
                dt = (DateTime)reader["dateaut"];
                ts = (TimeSpan)reader["timeaut"];
                mac = (string)reader["mac"];
                ip = (string)reader["ip"];
                email = (string)reader["email"];
                dt.Add(ts);
                userPresences.AddLast(new UserPresence(dt, email, mac, ip));
            }
            base.Disconnect();
            return userPresences;
        }
        public void Create(UserPresence obj)
        {
            sqlCommandString = "INSERT INTO  Project2GroupGAutenticationTable (dateaut,timeaut, mac,ip) VALUES('" + obj.GetDate().ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + obj.GetDate().TimeOfDay + "', " + "'" + obj.GetMac() + "','"+obj.GetIp()+"');";
            this.DoVoidCommand(this.sqlCommandString);
            int lastindex = base.LastIndexAutenticationTable();
            sqlCommandString = "INSERT INTO  Project2GroupGAutenticationUserTable (timeindex,email) VALUES('" + lastindex + "','" + obj.GetEmail() + "');";
            this.DoVoidCommand(this.sqlCommandString);
        }
        public UserPresence Read(string key)//last autentication
        {
            int index=base.GetLastTimeIndexFromEmail(key);
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
            int index = base.GetLastTimeIndexFromEmail(obj.GetEmail());
            this.sqlCommandString = "UPDATE  Project2GroupGAutenticationTable SET dateaut='" + obj.GetDate().ToString("yyyy-MM-dd HH:mm:ss.fff") + "',timeaut='" + obj.GetDate().TimeOfDay + "', mac='" + obj.GetMac() + "',ip='" + obj.GetIp() + "' WHERE timeindex='"+index+"';";
            this.DoVoidCommand(this.sqlCommandString);
        }

        public void Delete(string key)
        {
            int index = base.GetLastTimeIndexFromEmail(key);
            this.sqlCommandString = "DELETE FROM  Project2GroupGAutenticationTable WHERE timeindex='" + index + "';";
            this.DoVoidCommand(this.sqlCommandString);
        }
    }
}