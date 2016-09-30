using System.ServiceModel;

namespace AbsenceRegistrationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAbsenceRegistration" in both code and config file together.
    [ServiceContract]
    public interface IAbsenceRegistration
    {
        [OperationContract]
        void DoWork();
    }
}
