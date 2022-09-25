using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace ServiceProvider.Controllers {
    public class AuthStatusProvider {

        public class ResponseModel {
            public string Status { get; set; }
            public string Reason { get; set; }
        }

        private Interface auth;

        public ResponseModel Get(int token, out ResponseModel response) {

            ChannelFactory<Interface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8100/Authenticator";
            try {
                foobFactory = new ChannelFactory<Interface>(tcp, URL);
                auth = foobFactory.CreateChannel();
            } catch {
                //MessageBox.Show("Server is offline");
            }// end of try catch

            auth.Validate(token, out string result);

            if (result == "validated") {
                ResponseModel rs = new ResponseModel();
                rs.Status = "Approved";
                rs.Reason = "Valid Token";
                response = rs;
                return response;
            } else {
                ResponseModel rs = new ResponseModel();
                rs.Status = "Denied";
                rs.Reason = "Authentication Error";

                response = rs;
                return response;
            }

        }// end of get
    }
}