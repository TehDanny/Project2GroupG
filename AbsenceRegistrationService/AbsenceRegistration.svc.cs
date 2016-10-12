using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Login_Component;
using System.Web;
using System.ServiceModel.Channels;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace AbsenceRegistrationService
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AbsenceRegistration" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AbsenceRegistration.svc or AbsenceRegistration.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class AbsenceRegistration : IAbsenceRegistration
    {

        MsSqlLoginDataMapper ldm
        {
            get
            {
                if (HttpContext.Current.Session["ldm"] == null)
                    HttpContext.Current.Session["ldm"] = new MsSqlLoginDataMapper();
                return (MsSqlLoginDataMapper)HttpContext.Current.Session["ldm"];
            }
            set
            {
                HttpContext.Current.Session["ldm"] = value;
            }
        }

        Login l
        {
            get
            {
                if (HttpContext.Current.Session["l"] == null)
                    HttpContext.Current.Session["l"] = new Login(ldm);
                return (Login)HttpContext.Current.Session["l"];
            }
            set
            {
                HttpContext.Current.Session["l"] = value;
            }
        }


        MsSqlPresenceDataMapper pdm
        { 
            get
            {
                if (HttpContext.Current.Session["pdm"] == null)
                    HttpContext.Current.Session["pdm"] = new MsSqlPresenceDataMapper();
                return (MsSqlPresenceDataMapper)HttpContext.Current.Session["pdm"];
            }
            set
            {
                HttpContext.Current.Session["pdm"] = value;                
            }
        }

        string email
        {
            get
            {
                if (HttpContext.Current.Session["email"] == null)
                    throw new FaultException("User not logged in");
                return (string)HttpContext.Current.Session["email"];
            }
            set
            {
                HttpContext.Current.Session["email"] = value;
            }
        }
        //True if teacher, false if student
        bool isTeacher
        {
            get
            {
                if (HttpContext.Current.Session["isTeacher"] == null)
                    throw new FaultException("User not logged in");
                return (bool)HttpContext.Current.Session["isTeacher"];
            }
            set
            {
                HttpContext.Current.Session["isTeacher"] = value;
            }
        }

        /// <summary>
        /// Creates and logs the user in
        /// </summary>
        /// <returns>True if teacher, false if student</returns>
        public bool CreateUser(string email, string fisrtname, string surname, string password, string confirmPassword)
        {
            try
            {
                l.CreateUser(email, fisrtname, surname, password, confirmPassword);
            }
            catch (Exception e)
            {
                throw new FaultException(e.GetType().ToString());
            }

            return LoginUser(email, password);
        }

        /// <summary>
        /// Logs the user in
        /// </summary>
        /// <returns>True if teacher, false if student</returns>
        public bool LoginUser(string email, string password)
        {

            try
            {
                l.LoginUser(email, password);
            }
            catch (Exception e)
            {
                throw new FaultException(e.GetType().ToString());
            }

            this.email = email;
            //Keeps the session for 120 minutes, so the user doesn't have to log in every time
            HttpContext.Current.Session.Timeout = 120;

            if (this.email.Contains("@eal.dk"))
            {
                isTeacher = true;
                return isTeacher;
            }
            else
            {
                isTeacher = false;
                return isTeacher;
            }
        
        }

        public void CheckIn()
        {
            //We take the IP address
            string ip = GetClientIP();

            //Check IP throws an exception if the IP is not valid           
            CheckIP(ip);
            
            //Check if the last presence matches with that one
            UserPresence tmp = pdm.Read(email);
            if (tmp.GetDate().Hour == DateTime.Now.Hour)
                throw new FaultException("Already checked-in at this hour (" + DateTime.Now.Hour + ")");

            //Save to DB            
            string mac = GetClientMac();
            UserPresence up = new UserPresence(DateTime.Now, email, mac, ip);
            
            pdm.Create(up);
            
        }

        public LinkedList<UserPresence> GetAllUsersHistory()
        {
            //Chech if the user has previously logged-in as a teacher
            if (isTeacher == false)
                throw new FaultException("User is not a teacher");

            //Read from DB
            LinkedList<UserPresence> history = pdm.ReadAllUsersHistory();

            return history;
        }

        public bool GetUserPresent(string email)
        {
            //Chech if the user has previously logged-in as a teacher
            if (isTeacher == false)
                throw new FaultException("User is not a teacher");

            //Read from DB
            UserPresence tmp = pdm.Read(email);

            //Check if the last presence matches with the current time
            if (tmp.GetDate().Hour == DateTime.Now.Hour)
                return true;
            else
                return false;
        }

        private string GetClientMac()
        {
            throw new NotImplementedException();
        }


        private string[] ipList = { "185.19.133.3", "185.19.132.84" };
        //Not the best checkIP ever, gonna improve when we make more research on subnet masks
        private void CheckIP(string ip)
        {
            foreach (string s in ipList)
            {
                if (s.Equals(ip))
                    return;
            }
            throw new FaultException("IP outside eal. IP="+ip);
        }
        
        //Must move this into another class, there's the risk of this class getting too huge
        string GetClientIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }

    }
}
