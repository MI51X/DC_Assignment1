using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Registry.Controllers
{
    [ServiceContract]
    public interface Interface
    {
        [OperationContract]
        string Validate(int token, out string status);
    }
}