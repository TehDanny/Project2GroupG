using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AbsenceRegistrationService
{
    public class MsSqlLoginDataMapper : MsSqlConnect,ILoginDataMapper
    {
        private string sqlCommandString = "";
        private SqlCommand cmd;
        private SqlDataReader reader;
        public void Create(User obj)
        {
            base.Connect();
            sqlCommandString="INSERT INTO  Project2GroupGUserTable VALUES('"+obj.GetEmail()+"','" + obj.GetEncryptedPassword()+ "', '"+obj.GetName()+"','"+obj.GetSurname()+"', '"+obj.GetUserType()+"');";
            cmd = new SqlCommand(sqlCommandString, base.GetSqlConnection());
            cmd.ExecuteNonQuery();

            int lastindex = MsSqlOperations.LastIndexAutenticationTable();

            sqlCommandString = "INSERT INTO  Project2GroupGAutenticationUserTable (timeindex,email) VALUES('" + (lastindex+1) + "','"+obj.GetEmail()+"');";
            cmd = new SqlCommand(sqlCommandString, base.GetSqlConnection());
            cmd.ExecuteNonQuery();

            base.Disconnect();
        }

        public void Delete(string key)
        {
            base.Connect();
            sqlCommandString = "DELETE FROM  Project2GroupGUserTable WHERE email='"+key+"';";
            cmd = new SqlCommand(sqlCommandString, base.GetSqlConnection());
            cmd.ExecuteNonQuery();
            base.Disconnect();
        }
        public User Read(string key)
        {
            base.Connect();
            sqlCommandString = "select email,encryptedpassword,name,surname,type from Project2GroupGUserTable where email='" + key + "'";
            cmd = new SqlCommand(sqlCommandString, base.GetSqlConnection());
            reader = cmd.ExecuteReader();
            User u = null;
            if (reader.Read())
            {
                u = (User)/*here we should return an object of a class that inherits User*/reader["email"];
                base.Disconnect();
                return u;
            }
            base.Disconnect();
            return null;
        }
        public void Update(User obj)
        {
            string email = obj.GetEmail();
            string encryptedPassword = obj.GetEncryptedPassword();
            string name = obj.GetName();
            string surname = obj.GetSurname();
            string type = obj.GetUserType();
            base.Connect();
            sqlCommandString = "UPDATE Project2GroupGUserTable SET email='" + email + "',encryptedpassword='" + encryptedPassword + "',name='" + name + "',surname='" + surname + "',type='" + type + "' WHERE email='" + email + "';";
            cmd = new SqlCommand(sqlCommandString, base.GetSqlConnection());
            cmd.ExecuteNonQuery();
            base.Disconnect();
        }
    }
}