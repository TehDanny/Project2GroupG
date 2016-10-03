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

        protected T ReadOneRowTypeFromCommandStringOneField<T>(string commandString, string fieldName)
        {

            T result;
            result=default(T);
            result = (T)Activator.CreateInstance(typeof(T), new object[] { result });
            base.Connect();
            cmd = new SqlCommand(commandString, base.GetSqlConnection());
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                object o;
                o= reader[fieldName];
                result.GetType();
                
                result =(T)reader[fieldName];
            }
            base.Disconnect();
            return result;
        }
        protected LinkedList<T> ReadSomeRowesTypeFromCommandStringeOneField<T>(string commandString, string fieldName)
        {
            LinkedList<T> results = new LinkedList<T>();
            base.Connect();
            cmd = new SqlCommand(commandString, base.GetSqlConnection());
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                results.AddLast((T)reader[fieldName]);
            }
            base.Disconnect();
            return results;
        }
        protected LinkedList<int> GetTimeIndexesFromEmail(string key)
        {
            sqlCommandString = "select timeindex from Project2GroupGAutenticationUserTable where email='" + key + "'";
            LinkedList<int> indexes = new LinkedList<int>();
            base.Connect();
            cmd = new SqlCommand(sqlCommandString, base.GetSqlConnection());
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                indexes.AddLast((int)reader["timeindex"]);
            }
            base.Disconnect();
            return indexes;
        }

        
        protected int LastIndexAutenticationTable()
        {
            sqlCommandString = "select TOP 1 timeindex from Project2GroupGAutenticationTable ORDER BY timeindex desc";
            return this.ReadOneRowTypeFromCommandStringOneField<int>(sqlCommandString, "timeindex");
        }
        protected int GetLastTimeIndexFromEmail(string email)
        {
            sqlCommandString = "select TOP 1 timeindex from Project2GroupGAutenticationUserTable where email='"+email+"' ORDER BY timeindex desc";
            return this.ReadOneRowTypeFromCommandStringOneField<int>(sqlCommandString, "timeindex");
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