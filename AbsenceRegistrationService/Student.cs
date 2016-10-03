using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbsenceRegistrationService
{
    public class Student : User
    {
        public Student(string email, string name, string surname, string encryptedPassword, string type) : base(email, name, surname, encryptedPassword,"student")
        {
        }
    }
}