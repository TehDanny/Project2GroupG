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
            throw new NotImplementedException();
        }
        public string GetEncryptedPassword()
        {
            throw new NotImplementedException();
        }
        public string GetName()
        {
            throw new NotImplementedException();
        }
        public string GetSurname()
        {
            throw new NotImplementedException();
        }
        public string GetUserType()//called in this way because GetType already exists
        {
            throw new NotImplementedException();
        }
    }
}