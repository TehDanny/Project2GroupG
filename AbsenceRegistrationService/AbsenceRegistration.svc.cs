using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Login_Component;
using System.Web;
using System.ServiceModel.Channels;

namespace AbsenceRegistrationService
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AbsenceRegistration" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AbsenceRegistration.svc or AbsenceRegistration.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class AbsenceRegistration : IAbsenceRegistration
    {

        MsSqlLoginDataMapper ldm;
        Login l;

        MsSqlPresenceDataMapper pdm;
        string email;

        public void CreateUser(string email, string fisrtname, string surname, string password, string confirmPassword)
        {
            InitializeLoginFromSession();
            l.CreateUser(email, fisrtname, surname, password, confirmPassword);
            LoginUser(email, password);
        }

        public void LoginUser(string email, string password)
        {
            InitializeLoginFromSession();
            l.LoginUser(email, password);
            HttpContext.Current.Session["email"] = email;
        }

        public void CheckIn()
        {
            //Check if the user has previously logged-in
            CheckLoggedIn();

            //We take the IP address
            string ip = GetClientIP();

            //Check IP
            CheckIP(ip);

            //Save to DB
            InitializePresenceDataMapperFromSession();
            
            string mac = GetClientMac();
            UserPresence up = new UserPresence(DateTime.Now, email, mac, ip);
            
            pdm.Create(up);

        }

        private string GetClientMac()
        {
            throw new NotImplementedException();
        }

        //Not the best checkIP ever, gonna improve when we make more research on subnet masks
        private void CheckIP(string ip)
        {
            if (!ip.StartsWith("10"))
                throw new Exception("IP outside eal");

            //Subnet mask of EAL: 255.255.240.0
        }

        private void CheckLoggedIn()
        {
            if (HttpContext.Current.Session["emai"] != null)
                email = (string)HttpContext.Current.Session["emai"];
            else
                throw new Exception("User not connected");
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
