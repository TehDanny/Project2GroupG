﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AbsenceRegistrationService
{
    public class MsSqlOperations:MsSqlConnect
    {
        protected  string sqlCommandString = "";
        protected SqlCommand cmd;
        protected SqlDataReader reader;

        protected T ReadTypeFromCommandStringOneField<T>(string commandString, string fieldName)
        {
            T result = (T)(new object());
            base.Connect();
            cmd = new SqlCommand(commandString, base.GetSqlConnection());
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result = (T)reader[fieldName];
            }
            base.Disconnect();
            return default(T);
        }
        protected int LastIndexAutenticationTable()
        {
            sqlCommandString = "select TOP 1 timeindex from Project2GroupGAutenticationTable ORDER BY timeindex desc";
            return this.ReadTypeFromCommandStringOneField<int>(sqlCommandString, "timeindex");
        }
        protected int GetLastTimeIndexFromEmail(string email)
        {
            sqlCommandString = "select TOP 1 timeindex from Project2GroupGAutenticationUserTable where email='"+email+"' ORDER BY timeindex desc";
            return this.ReadTypeFromCommandStringOneField<int>(sqlCommandString, "timeindex");
        }
        protected void DoVoidCommand(string commandString)
        {
            base.Connect();
            cmd = new SqlCommand(commandString, base.GetSqlConnection());
            cmd.ExecuteNonQuery();
            base.Disconnect();
        }
    }
}