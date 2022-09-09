using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator {
    internal class Register {
        
        internal class RegisterModel {
            public string Name { get; set; }
            public string Password { get; set; }
        }// end of RegisterModel
        
        public string reg(string name, string password) {
            RegisterModel model = new RegisterModel();
            model.Name = name;
            model.Password = password;

            string loglocraw = Directory.GetCurrentDirectory() + @"\datastore\registration.txt";

            string registrationData = "Name: " + model.Name + " Password: " + model.Password;

            if (!File.Exists(loglocraw)) {
                FileStream fs = File.Create(loglocraw);
                File.AppendAllText(loglocraw, Environment.NewLine + registrationData);
                return "Successfully Generated";
            } else {
                File.AppendAllText(loglocraw, Environment.NewLine + registrationData);
                return "Successfully Generated";
            }// end of if 
            
        }// end of reg
        
    }// end of Register

}// end of Authenticator
