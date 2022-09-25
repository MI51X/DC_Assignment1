using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator {
    internal class Implementation : Interface {
        public string Register(string name, string password, out string result) {
            Register register = new Register();
            result = register.reg(name, password);
            return result;
        }

        public int Login(string name, string password, out int token) {
            Login login = new Login();
            token = login.log(name, password);
            return token;
        }

        public string Validate(int token, out string status)
        {
            Validate validate = new Validate();
            status = validate.validate(token);
            return status;
        }
    }
}
