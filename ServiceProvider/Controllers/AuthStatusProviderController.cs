using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;

namespace ServiceProvider.Controllers {

    [RoutePrefix("api/ServiceProvider")]

    public class AuthStatusProviderController : ApiController {

        public class ResponseModel {
            public string Status { get; set; }
            public string Reason { get; set; }
        }

        private Interface auth;

        [Route("authstatusprovider/{token}")]
        [Route("authstatusprovider")]
        [HttpGet]
        public ResponseModel Get(int token) {

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
            
            if(result == "validated") {
                ResponseModel rs = new ResponseModel();
                rs.Status = "Approved";
                rs.Reason = "Valid Token";
                return rs;
            } else {
                ResponseModel rs = new ResponseModel();
                rs.Status = "Denied";
                rs.Reason = "Authentication Error";
                return rs;
            }
            
        }// end of get

    }//end of validation controller
}
