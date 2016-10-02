using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsenceRegistrationService
{
    public interface IDataMapper <ObjectType, KeyType>
    {

        void Create(ObjectType obj);
        ObjectType Read(KeyType key);
        void Update(ObjectType obj);
        void Delete(KeyType key);

    }
}
