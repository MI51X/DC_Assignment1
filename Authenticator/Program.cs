using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Windows;
using System.IO;

namespace Authenticator {

    class Program {

        static void Main(string[] args) {

            ServiceHost host;
            NetTcpBinding tcp = new NetTcpBinding();
            host = new ServiceHost(typeof(Implementation));

            try {
                host.AddServiceEndpoint(typeof(Interface), tcp, "net.tcp://0.0.0.0:8100/Authenticator");
                host.Open();
            } catch {
                MessageBox.Show("Port 8100 is used by some other process");
            }// end of try catch

            Console.WriteLine("Server Online \n");

            Console.Write("Enter timeout for tokens in Minutes: ");
            int timeout = Convert.ToInt32(Console.ReadLine());
            Task.Delay(new TimeSpan(0,timeout,0)).ContinueWith(o => { TokenCleaner(); });

            void TokenCleaner() {
                string TokenLocRaw = Directory.GetCurrentDirectory() + @"\datastore\token.txt";
                File.WriteAllText(TokenLocRaw, String.Empty);
                Console.WriteLine("\nSet time elapsed\nTokens wiped");
            }

            Console.ReadLine();
            host.Close();

        }// end of Main

    }// end of Program

}// end of Authenticator
