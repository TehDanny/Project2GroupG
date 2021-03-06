﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AbsenceRegistrationService
{
    public class MsSqlOperations:MsSqlConnect
    {
        protected SqlCommand cmd;
        protected SqlDataReader reader;

        protected T ReadOneRowOneTypeFromCommandStringOneField<T>(string commandString, string fieldName)
        {

            T result;
            result = default(T);
            base.Connect();
            cmd = new SqlCommand(commandString, base.GetSqlConnection());
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result =(T)reader[fieldName];
            }
            base.Disconnect();
            return result;
        }
        protected Login_Component.User ReadUser(string commandString)
        {

            Login_Component.User result = null;
            base.Connect();
            cmd = new SqlCommand(commandString, base.GetSqlConnection());
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string email = (string)reader["email"];
                string name = (string)reader["name"];
                string surname = (string)reader["surname"];
                string encryptedPassword = (string)reader["encryptedPassword"];
                string type = (string)reader["type"];
                result = new Login_Component.User(email, name, surname, encryptedPassword, type);
            }
            base.Disconnect();
            return result;
        }
        /*protected LinkedList<T> ReadSomeRowesTypeFromCommandStringeOneField<T>(string commandString, string fieldName)
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
        }*/
        protected LinkedList<int> GetTimeIndexesFromEmail(string key)
        {
            string sqlCommandString = "select timeindex from Project2GroupGAutenticationUserTable where email='" + key + "'";
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
        protected void rightInput(string s)
        {
            if (s.Contains("\"")||s.Contains("!") || s.Contains("=") || s.Contains("%") || s.Contains("&") || s.Contains("?") || s.Contains(";") || s.Contains(",")|| s.Contains("*") || s.Contains("--"))
            {
                throw new SqlInjectionException();
            }
        }
        
        protected int LastIndexAutenticationTable()
        {
            string sqlCommandString = "select TOP 1 timeindex from Project2GroupGAutenticationTable ORDER BY timeindex desc";
            return this.ReadOneRowOneTypeFromCommandStringOneField<int>(sqlCommandString, "timeindex");
        }
        protected int GetLastTimeIndexFromEmail(string email)
        {
            string sqlCommandString = "select TOP 1 timeindex from Project2GroupGAutenticationUserTable WHERE email='"+email+"' ORDER BY timeindex desc";
            return this.ReadOneRowOneTypeFromCommandStringOneField<int>(sqlCommandString, "timeindex");
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