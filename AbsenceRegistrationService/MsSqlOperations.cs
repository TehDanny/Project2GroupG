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
        public static int LastIndexAutenticationTable()
        {
            mc.Connect();
            int lastindex = 0;
            sqlCommandString = "select TOP 1 timeindex from Project2GroupGAutenticationTable ORDER BY timeindex desc";
            cmd = new SqlCommand(sqlCommandString, mc.GetSqlConnection());
            reader = cmd.ExecuteReader();
            lastindex = (int)reader["timeindex"];
            mc.Disconnect();
            return lastindex;
        }

        public static int GetLastTimeIndexFromEmail(string email)
        {
            mc.Connect();
            int index = 0;
            sqlCommandString = "select timeindex from Project2GroupGAutenticationUserTable where email='"+email+"' ORDER BY timeindex desc";
            cmd = new SqlCommand(sqlCommandString, mc.GetSqlConnection());
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                index = (int)reader["timeindex"];
            }
            mc.Disconnect();
            return index;
        }
    }
}