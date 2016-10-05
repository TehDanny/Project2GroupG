using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Login_Component;
using System.Web;
using System.ServiceModel.Activation;

namespace AbsenceRegistrationService
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AbsenceRegistration" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AbsenceRegistration.svc or AbsenceRegistration.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class AbsenceRegistration : IAbsenceRegistration
    {

        MsSqlLoginDataMapper ldm;
        Login l;

        //We might want to use a singleton for the Login and MsSqlLoginDataMapper instead of creating those in every method
        public void CreateUser(string email, string fisrtname, string surname, string password, string confirmPassword)
        {
            InitializeFromSession();

            l.CreateUser(email, fisrtname, surname, password, confirmPassword);
        }

        public void LoginUser(string email, string password)
        {
            InitializeFromSession();
            l.LoginUser(email, password);
        }

        //TODO: find a better name, eventually
        private void InitializeFromSession()
        {
            if (HttpContext.Current.Session["ldm"] == null)
            {
                ldm = new MsSqlLoginDataMapper();
                l = new Login(ldm);
                HttpContext.Current.Session["ldm"] = ldm;
                HttpContext.Current.Session["l"] = l;
            }
            else
            {
                ldm = (MsSqlLoginDataMapper)HttpContext.Current.Session["ldm"];
                l = (Login)HttpContext.Current.Session["l"];
            }
        }
    }
}
