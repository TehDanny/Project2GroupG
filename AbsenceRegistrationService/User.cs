using System;

namespace AbsenceRegistrationService
{
    public abstract class User
    {

        private string email;
        private string name, surname;
        private string encryptedPassword;

        public string GetEmail()
        {
            return null;
        }
        public string GetEncryptedPassword()
        {
            return null;
        }
        public string GetName()
        {
            return null;
        }
        public string GetSurname()
        {
            return null;
        }
        public abstract string GetUserType();//called in this way because GetType already exists
    }
}