using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Authenticator
{
    internal class Validate
    {
        public string validate(int token)
        {
            string tokenFileLocation = Directory.GetCurrentDirectory() + @"\datastore\token.txt";

            List<string> tokens = File.ReadLines(tokenFileLocation).ToList();

            bool tokenExists = tokens.Exists(x => x == token.ToString());

            if (tokenExists)
            {
                return "validated";
            }
            else
            {
                return "not validated";
            }
        }
    }
}
