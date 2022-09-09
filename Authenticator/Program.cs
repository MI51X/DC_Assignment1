using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Windows;

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
            Console.ReadLine();
            host.Close();
            
        }// end of Main

    }// end of Program

}// end of Authenticator
