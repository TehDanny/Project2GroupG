using System;
using System.Data.SqlClient;

namespace AbsenceRegistrationService
{
    public class MsSqlConnect
    {
        protected SqlConnection dbconn = new SqlConnection("Data Source = ealdb1.eal.local;Initial Catalog=EAL5_DB;Persist Security Info=true;User ID=EAL5_USR;Password=Huff05e05");
        public void Connect()
        {
            dbconn.Open();
        }
        public void Disconnect()
        {
            dbconn.Close();
        }
        public SqlConnection GetSqlConnection()
        {
            return this.dbconn;
        }
    }
}