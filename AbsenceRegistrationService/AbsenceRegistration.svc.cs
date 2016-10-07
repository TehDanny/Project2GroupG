﻿using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Login_Component;
using System.Web;
using System.ServiceModel.Channels;
using System.Collections.Generic;

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
            l.CreateUser(email, fisrtname, surname, password, confirmPassword);
            return LoginUser(email, password);
        }

        /// <summary>
        /// Logs the user in
        /// </summary>
        /// <returns>True if teacher, false if student</returns>
        public bool LoginUser(string email, string password)
        {
            l.LoginUser(email, password);
            this.email = email;
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
            //Check if the user has previously logged-in
            CheckLoggedIn();

            //We take the IP address
            string ip = GetClientIP();

            //Check IP            
            CheckIP(ip);
            
            //Check if the last presence matches with that one
            UserPresence tmp = pdm.Read(email);
            if (tmp.GetDate().Hour == DateTime.Now.Hour)
                throw new FaultException("Already checked-in at this hour (" + DateTime.Now.Hour + ")");

            //Save to DB
            InitializePresenceDataMapperFromSession();
            
            string mac = GetClientMac();
            UserPresence up = new UserPresence(DateTime.Now, email, mac, ip);
            
            pdm.Create(up);
            
        }

        public LinkedList<UserPresence> GetAllUsersHistory()
        {
            //Chech if the user has previously logged-in and if he is a teacher
            CheckLoggedIn();
            if (isTeacher == true)
                throw new FaultException("User is not a teacher");

            //Read from DB
            InitializePresenceDataMapperFromSession();
            LinkedList<UserPresence> history = pdm.ReadAllUsersHistory();

            return history;
        }

        public bool GetUserPresent(string email)
        {

            //Chech if the user has previously logged-in and if he is a teacher
            CheckLoggedIn();
            if (isTeacher == true)
                throw new FaultException("User is not a teacher");

            //Read from DB
            InitializePresenceDataMapperFromSession();
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

        //Not the best checkIP ever, gonna improve when we make more research on subnet masks
        private void CheckIP(string ip)
        {
            if (!ip.StartsWith("10"))
                throw new FaultException("IP outside eal");

            //Subnet mask of EAL: 255.255.240.0
        }

        private void CheckLoggedIn()
        {
            if (HttpContext.Current.Session["email"] != null && HttpContext.Current.Session["isTeacher"] != null)
            {
                email = (string)HttpContext.Current.Session["email"];
                isTeacher = (bool)HttpContext.Current.Session["isTeacher"];
            }
            else
                throw new FaultException("User not logged in");
        }
        
        //Must move this into another class, there's the risk of this class getting too huge
        string GetClientIP()
        {
            //THE IP IS NOT CORRECT SO FAR
            //So far copy-pasted code, hopefully we'll make sense of it by looking at the documentation.
            MessageProperties incomingMessageProperties = OperationContext.Current.IncomingMessageProperties;

            RemoteEndpointMessageProperty remoteEndpointMessageProperty = incomingMessageProperties[RemoteEndpointMessageProperty.Name]
                as RemoteEndpointMessageProperty;
            
            string ip = remoteEndpointMessageProperty.Address;

            return ip;
        }

        //TODO: find a better name, eventually
        //Might do: create a more generic method for that that takes in some parameters,
        //so we can only have one of those instead of 2 (or probably even more in the future)
        private void InitializeLoginFromSession()
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

        private void InitializePresenceDataMapperFromSession()
        {
            if (HttpContext.Current.Session["pdm"] == null)
            {
                pdm = new MsSqlPresenceDataMapper();
                HttpContext.Current.Session["pdm"] = pdm;
            }
            else
                pdm = (MsSqlPresenceDataMapper) HttpContext.Current.Session["pdm"];
        }

    }
}
