﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AbsenceRegistrationService
{
    public class MsSqlPresenceDataMapper :MsSqlOperations,IPresenceDataMapper
    {
        private string date;
        private string time;
        private string email;
        private string ip;
        private string mac;
        private static Object thisLock = new Object();

        public LinkedList<UserPresence> ReadUserHistory(string key)
        {
            if (key != null) base.rightInput(key);
            LinkedList<UserPresence> usp = new LinkedList<UserPresence>();
            foreach(UserPresence up in this.ReadAllUsersHistory())
            {
                if (up.GetEmail().Equals(key))
                {
                    usp.AddLast(up);
                }
            }
            return usp;
        }

        public LinkedList<UserPresence> ReadAllUsersHistory()
        {
            LinkedList<UserPresence> userPresences = new LinkedList<UserPresence>();
            string sqlCommandString = "select dateaut,timeaut,mac,ip,email from Project2GroupGAutenticationTable,Project2GroupGAutenticationUserTable where Project2GroupGAutenticationTable.timeindex=Project2GroupGAutenticationUserTable.timeindex";
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

            if (obj.GetEmail() != null)  base.rightInput(obj.GetEmail());
            if (obj.GetDate() != null) base.rightInput(obj.GetDate().ToString("yyyy-MM-dd HH:mm:ss.fff"));
            if (obj.GetIp() != null) base.rightInput(obj.GetIp());
            if (obj.GetMac() != null) base.rightInput(obj.GetMac());
            lock (thisLock)
            {
                this.SetParameters(obj);
                string sqlCommandString = "INSERT INTO  Project2GroupGAutenticationTable (dateaut,timeaut, mac,ip) VALUES(" + this.date + "," + this.time + ", " + "" + this.mac + "," + this.ip + ");";
                this.DoVoidCommand(sqlCommandString);
                int lastindex = base.LastIndexAutenticationTable();
                sqlCommandString = "INSERT INTO  Project2GroupGAutenticationUserTable (timeindex,email) VALUES('" + lastindex + "'," + this.email + ");";
                this.DoVoidCommand(sqlCommandString);
            }
            
            
        }

        private void SetParameters(UserPresence obj)
        {
            this.date = (obj.GetDate() == null) ? "NULL" : ("'" + obj.GetDate().ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
            this.time = (obj.GetDate().TimeOfDay == null) ? "NULL" : ("'" + obj.GetDate().TimeOfDay + "'");
            this.email = (obj.GetEmail() == null) ? "NULL" : ("'" + obj.GetEmail() + "'");
            this.ip = (obj.GetIp() == null) ? "NULL" : ("'" + obj.GetIp() + "'");
            this.mac = (obj.GetMac() == null) ? "NULL" : ("'" + obj.GetMac() + "'");
        }

        public UserPresence Read(string key)//last autentication
        {
            if (key != null) base.rightInput(key);
            string sqlCommandString;
            this.SetParameters(new UserPresence(default(DateTime),key,null,null));
            int index = base.GetLastTimeIndexFromEmail(key);
            if (index == 0) return null;
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
            dt=dt.Add(ts);
            UserPresence up = new UserPresence(dt, key, mac, ip);
            return up;
        }
        public void Update(UserPresence obj)//update last autentication time for that user
        {
            if (obj.GetEmail() != null) base.rightInput(obj.GetEmail());
            if (obj.GetDate() != null) base.rightInput(obj.GetDate().ToString("yyyy-MM-dd HH:mm:ss.fff"));
            if (obj.GetIp() != null) base.rightInput(obj.GetIp());
            if (obj.GetMac() != null) base.rightInput(obj.GetMac());
            lock (thisLock)
            {
                this.SetParameters(obj);
                int index = base.GetLastTimeIndexFromEmail(obj.GetEmail());
                string sqlCommandString = "UPDATE  Project2GroupGAutenticationTable SET dateaut=" + this.date + ",timeaut=" + this.time + ", mac=" + this.mac + ",ip=" + this.ip + " WHERE timeindex='" + index + "';";
                this.DoVoidCommand(sqlCommandString);
            }
            
        }

        public void Delete(string key)
        {
            if (key != null) base.rightInput(key);
            lock (thisLock)
            {
                int index = base.GetLastTimeIndexFromEmail(key);
                string sqlCommandString = "DELETE FROM  Project2GroupGAutenticationTable WHERE timeindex='" + index + "';";
                this.DoVoidCommand(sqlCommandString);
            }
        }
    }
}