using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsenceRegistrationService
{
    interface IPresenceDataMapper : IDataMapper<UserPresence,string>
    {

        //The key here should be the email
        LinkedList<UserPresence> ReadUserHistory(string key);

        LinkedList<UserPresence> ReadAllUsersHistory();

    }
}
