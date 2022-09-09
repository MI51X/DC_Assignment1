using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServicePublishing {
        [ServiceContract]
        public interface Interface {
            [OperationContract]
            String Register(String name, String password, out string result);

            [OperationContract]
            int Login(String name, String password, out int token);

        }// end of Interface
}
