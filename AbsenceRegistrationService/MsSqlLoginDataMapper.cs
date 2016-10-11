using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Login_Component;

namespace AbsenceRegistrationService
{
    public class MsSqlLoginDataMapper : MsSqlOperations,/*ILoginDataMapper*/Login_Component.ILoginDataMapper
    {
        private string type;
        private string encryptedPassword;
        private string email;
        private string name;
        private string surname;
        private static Object thisLock = new Object();
        public void Create(Login_Component.User obj)
        {
            lock (MsSqlLoginDataMapper.thisLock)
            {
                if(obj.GetEmail()!=null)base.rightInput(obj.GetEmail());
                if (obj.GetEncryptedPassword() != null) base.rightInput(obj.GetEncryptedPassword());
                if (obj.GetName() != null) base.rightInput(obj.GetName());
                if (obj.GetSurname() != null) base.rightInput(obj.GetSurname());
                if (obj.GetUserType() != null) base.rightInput(obj.GetUserType());
                this.setParameters(obj);
                string sqlCommandString = "INSERT INTO  Project2GroupGUserTable VALUES(" + this.email + "," + this.encryptedPassword + ", " + this.name + "," + this.surname + ", " + this.type + ");";
                base.DoVoidCommand(sqlCommandString);
            }
        }
        public void Delete(string key)
        {
            lock (MsSqlLoginDataMapper.thisLock)
            {
                if(key!=null)base.rightInput(key);
                this.setParameters(new Login_Component.User(key, "", null, null, null));
                string sqlCommandString = "DELETE FROM  Project2GroupGUserTable WHERE email=" + this.email + ";";
                base.DoVoidCommand(sqlCommandString);
            }
        }
        public Login_Component.User Read(string key)
        {
            if(key!=null)base.rightInput(key);
            this.setParameters(new Login_Component.User(key, "", null, null, null));
            string sqlCommandString = "select email,encryptedpassword,name,surname,type from Project2GroupGUserTable where email=" + this.email + "";
            return base.ReadUser(sqlCommandString);
        }
        public void Update(Login_Component.User obj)//the email has to be the same
        {
            lock (MsSqlLoginDataMapper.thisLock)
            {
                if (obj.GetEmail() != null) base.rightInput(obj.GetEmail());
                if (obj.GetEncryptedPassword() != null) base.rightInput(obj.GetEncryptedPassword());
                if (obj.GetName() != null) base.rightInput(obj.GetName());
                if (obj.GetSurname() != null) base.rightInput(obj.GetSurname());
                if (obj.GetUserType() != null) base.rightInput(obj.GetUserType());
                this.setParameters(obj);
                string sqlCommandString = "UPDATE Project2GroupGUserTable SET email=" + this.email + ",encryptedpassword=" + this.encryptedPassword + ",name=" + this.name + ",surname=" + this.surname + ",type=" + this.type + " WHERE email=" + this.email + ";";
                base.DoVoidCommand(sqlCommandString);
            }
        }

        private void setParameters(Login_Component.User obj)
        {
            this.type = (obj.GetUserType() == null) ? "NULL" : ("'" + obj.GetUserType() + "'");
            this.email = (obj.GetEmail() == null) ? "NULL" : ("'" + obj.GetEmail() + "'");
            this.encryptedPassword = (obj.GetEncryptedPassword() == null) ? "NULL" : ("'" + obj.GetEncryptedPassword() + "'");
            this.name = (obj.GetName() == null) ? "NULL" : ("'" + obj.GetName() + "'");
            this.surname = (obj.GetSurname() == null) ? "NULL" : ("'" + obj.GetSurname() + "'");
        }
    }
}