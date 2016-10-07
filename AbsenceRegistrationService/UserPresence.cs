using System;
using System.Runtime.Serialization;

namespace AbsenceRegistrationService
{
    [DataContract]
    public class UserPresence
    {
        [DataMember]
        private string email;
        [DataMember]
        private string mac, ip;
        [DataMember]
        private DateTime dt;

        public UserPresence(DateTime dt, string email, string mac, string ip)
        {
            this.dt = dt;
            this.email = email;
            this.mac = mac;
            this.ip = ip;
        }
        public bool EqualsPresence(UserPresence up)
        {
            if(this.email.Equals(up.GetEmail())&& this.mac.Equals(up.GetMac())&& this.ip.Equals(up.GetIp())&& this.dt.Equals(up.GetDate()))
            {
                return true;
            }
            return false;
        }
        public DateTime GetDate()
        {
            return this.dt;
        }
        public string GetMac()
        {
            return this.mac;
        }
        public string GetIp()
        {
            return this.ip;
        }
        public string GetEmail()
        {
            return this.email;
        }
    }
}