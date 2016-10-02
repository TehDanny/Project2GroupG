using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AbsenceRegistrationService
{
    public static class MsSqlOperations
    {
        private static MsSqlConnect mc = new MsSqlConnect();
        private static string sqlCommandString = "";
        private static SqlCommand cmd;
        private static SqlDataReader reader;

        private static int ReadFromCommandString(string commandString,string fieldName)
        {
            int result = 0;
            mc.Connect();
            cmd = new SqlCommand(commandString, mc.GetSqlConnection());
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result = (int)reader[fieldName];
            }
            mc.Disconnect();
            return result;
        }
        public static int LastIndexAutenticationTable()
        {
            sqlCommandString = "select TOP 1 timeindex from Project2GroupGAutenticationTable ORDER BY timeindex desc";
            return MsSqlOperations.ReadFromCommandString(sqlCommandString, "timeindex");
        }
        public static int GetLastTimeIndexFromEmail(string email)
        {
            sqlCommandString = "select TOP 1 timeindex from Project2GroupGAutenticationUserTable where email='"+email+"' ORDER BY timeindex desc";
            return MsSqlOperations.ReadFromCommandString(sqlCommandString, "timeindex");
        }
    }
}