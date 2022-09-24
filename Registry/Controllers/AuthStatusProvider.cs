using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace Registry.Controllers
{
    public class AuthStatusProvider
    {
        public class ResponseModel
        {
            public string Status { get; set; }
            public string Reason { get; set; }
        }

        private Interface auth;
        public bool AuthStatusCheck(int token, out ResponseModel responseModel)
        {
            ResponseModel rs = new ResponseModel();
            ChannelFactory<Interface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8100/Authenticator";
            try
            {
                foobFactory = new ChannelFactory<Interface>(tcp, URL);
                auth = foobFactory.CreateChannel();
                auth.Validate(token, out string result);

                if (result == "validated")
                {
                    responseModel = rs;
                    return true;
                }
                else
                {
                    rs.Status = "Denied";
                    rs.Reason = "Authentication Error";
                    responseModel = rs;
                    return false;
                }
            }
            catch
            {
                rs.Status = "Authentication Server Offline";
                rs.Reason = "The token couldn't be validated because the authentication server is offline";
                responseModel = rs;
                return false;
            }
        }
    }
}