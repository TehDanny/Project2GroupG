using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Login_Component;

namespace AbsenceRegistrationService
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AbsenceRegistration" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AbsenceRegistration.svc or AbsenceRegistration.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class AbsenceRegistration : IAbsenceRegistration
    {
        //We might want to use a singleton for the Login and MsSqlLoginDataMapper instead of creating those in every method
        public void CreateUser(string email, string fisrtname, string surname, string password, string confirmPassword)
        {
            MsSqlLoginDataMapper ldm = new MsSqlLoginDataMapper();
            Login l = new Login(ldm);
            l.CreateUser(email, fisrtname, surname, password, confirmPassword);
        }

    }
}
