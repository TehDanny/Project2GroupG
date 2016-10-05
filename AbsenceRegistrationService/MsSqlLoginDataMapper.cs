using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AbsenceRegistrationService
{
    public class MsSqlLoginDataMapper : MsSqlOperations,/*ILoginDataMapper*/Login_Component.ILoginDataMapper
    {
        public void Create(Login_Component.User obj)
        {
            sqlCommandString="INSERT INTO  Project2GroupGUserTable VALUES('"+obj.GetEmail()+"','" + obj.GetEncryptedPassword()+ "', '"+obj.GetName()+"','"+obj.GetSurname()+"', '"+obj.GetUserType()+"');";
            base.DoVoidCommand(sqlCommandString);
        }
        public void Delete(string key)
        {
            sqlCommandString = "DELETE FROM  Project2GroupGUserTable WHERE email='"+key+"';";
            base.DoVoidCommand(sqlCommandString);
        }
        public Login_Component.User Read(string key)
        {
            sqlCommandString = "select email,encryptedpassword,name,surname,type from Project2GroupGUserTable where email='" + key + "'";
            return base.ReadOneRowTypeFromCommandStringOneField<Login_Component.User>(sqlCommandString, "email");
        }
        public void Update(Login_Component.User obj)
        {
            sqlCommandString = "UPDATE Project2GroupGUserTable SET email='" + obj.GetEmail() + "',encryptedpassword='" + obj.GetEncryptedPassword() + "',name='" + obj.GetName() + "',surname='" + obj.GetSurname() + "',type='" + obj.GetUserType() + "' WHERE email='" + obj.GetEmail() + "';";
            base.DoVoidCommand(sqlCommandString);
        }
    }
}