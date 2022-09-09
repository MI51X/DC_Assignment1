using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator {
    internal class Login {

        public int log(String name, String password) {
            string search = name + "=" + password;

            string reglocraw = Directory.GetCurrentDirectory() + @"\datastore\registration.txt";
            string tokenlocraw = Directory.GetCurrentDirectory() + @"\datastore\token.txt";

            var data = File.ReadAllLines(reglocraw);

            bool found = false;

            for (int i = 0; i < data.Length; i++) {
                if (data[i] == search) {
                    found = true;
                }// end of if
            }// end of for

            if (found) {

                Random random = new Random();
                
                int token = random.Next(100000, 999999);

                File.AppendAllText(tokenlocraw, Environment.NewLine + token);

                return token;
                
            } else {
                return 0;
            }// end of if else
        }
    }
}
