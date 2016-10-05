using System;

namespace AbsenceRegistrationService
{
    public class User
    {

        private string email;
        private string name, surname;
        private string encryptedPassword;
        private string type;

        public User(string email, string name, string surname, string encryptedPassword, string type)
        {
            this.email = email;
            this.name = name;
            this.surname = surname;
            this.encryptedPassword = encryptedPassword;
            this.type = type;
        }
        public bool UserEquals(User u)
        {
            if(this.email.Equals(u.GetEmail())&& this.name.Equals(u.GetName())&& this.surname.Equals(u.GetSurname())&& this.encryptedPassword.Equals(u.GetEncryptedPassword())&& this.type.Equals(u.GetUserType()))
            {
                return true;
            }
            return false;
        }
        public string GetEmail()
        {
            return this.email;
        }
        public string GetEncryptedPassword()
        {
            return this.encryptedPassword;
        }
        public string GetName()
        {
            return this.name;
        }
        public string GetSurname()
        {
            return this.surname;
        }
        public string GetUserType()//called in this way because GetType already exists
        {
            return this.type;
        }
    }
}