using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider {
    [ServiceContract]
    public interface Interface {
        [OperationContract]
        String Register(String name, String password, out string result);

        [OperationContract]
        int Login(String name, String password, out int token);

        [OperationContract]
        string Validate(int token, out string status);

    }// end of Interface
}// end of Authenticator
