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
    public class AbsenceRegistration
    {

        ILoginDataMapper ldm
        {
            get
            {
                if (HttpContext.Current.Session["ldm"] == null)
                    HttpContext.Current.Session["ldm"] = new MsSqlLoginDataMapper();
                return (ILoginDataMapper)HttpContext.Current.Session["ldm"];
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


        IPresenceDataMapper pdm
        {
            get
            {
                if (HttpContext.Current.Session["pdm"] == null)
                    HttpContext.Current.Session["pdm"] = new MsSqlPresenceDataMapper();
                return (IPresenceDataMapper)HttpContext.Current.Session["pdm"];
            }
            set
            {
                HttpContext.Current.Session["pdm"] = value;
            }
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
            throw new FaultException("IP outside eal. IP=" + ip);
        }

        ////Must move this into another class, there's the risk of this class getting too huge
        //string GetClientIP()
        //{
        //    return HttpContext.Current.Request.UserHostAddress;
        //}

        //private string GetClientMac()
        //{
        //    throw new NotImplementedException();
        //}

    }
}
