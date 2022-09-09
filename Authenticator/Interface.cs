using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator {
    [ServiceContract]
    public interface Interface {
        [OperationContract]
        String Register(String name, String password, out string result);


    }// end of Interface
}// end of Authenticator
