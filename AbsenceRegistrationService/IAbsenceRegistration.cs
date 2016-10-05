﻿using System.ServiceModel;

namespace AbsenceRegistrationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAbsenceRegistration" in both code and config file together.
    [ServiceContract]
    public interface IAbsenceRegistration
    {

        //Should those passwords already be encrypted by the client?
        [OperationContract]
        void CreateUser(string email, string fisrtname, string surname, string password, string confirmPassword);

        [OperationContract]
        void LoginUser(string email, string password);
    }
}
