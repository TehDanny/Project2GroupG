using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsenceRegistrationService
{
    interface IPresenceDataMapper : IDataMapper<UserPresence>
    {

        //The key here should be the email
        LinkedList<UserPresence> ReadUserHistory(string key);

        LinkedList<LinkedList<UserPresence>> ReadAllUsersHistory();

    }
}
