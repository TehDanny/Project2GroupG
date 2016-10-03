using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbsenceRegistrationService
{
    public class Teacher : User
    {
        public Teacher(string email, string name, string surname, string encryptedPassword) : base(email, name, surname, encryptedPassword, "teacher")
        {
        }
    }
}