using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbsenceRegistrationService
{
    public class Teacher : User
    {
        public override string GetUserType()
        {
            return null;
        }
    }
}