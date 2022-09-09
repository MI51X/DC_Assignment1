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

            string reglocraw = Directory.GetCurrentDirectory() + @"\datastore\registration.txt";

            string registrationData = model.Name + "=" + model.Password;

            var data = File.ReadAllLines(reglocraw);
            bool found = false;
            foreach (var line in data) {
                if (line.Split('=')[0].ToLower().ToLower() == model.Name.ToString()) {
                    found = true;
                }// end of if
            }// end of foreach

            if (!found) {
                File.AppendAllText(reglocraw, Environment.NewLine + registrationData);
                return "Successfully Generated";
            } else {
                return "Registration already exist";
            }// end of if else

        }// end of reg
        
    }// end of Register

}// end of Authenticator
