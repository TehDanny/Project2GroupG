using System.Collections.Generic;
using System.ServiceModel;

namespace AbsenceRegistrationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAbsenceRegistration" in both code and config file together.
    [ServiceContract]
    public interface IAbsenceRegistration
    {

        //Should those passwords already be encrypted by the client?
        [OperationContract]
        bool CreateUser(string email, string fisrtname, string surname, string password, string confirmPassword);

        [OperationContract]
        bool LoginUser(string email, string password);

        [OperationContract]
        void CheckIn(string ip, string mac);

        [OperationContract]
        LinkedList<UserPresence> GetAllUsersHistory();

        [OperationContract]
        bool GetUserPresent(string email);
    }
}
