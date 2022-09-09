using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServicePublishing {

    class Program {

        private Interface auth;

        static void Main(string[] args) {

            int option;// for program options

                Console.WriteLine("Choose an Option to get started \n1.Register\n2.login\n");
                option = Int32.Parse(Console.ReadLine());

                var mc = new Program();

                try {
                    mc.AuthConn(option);
                } catch (Exception a) {
                    Console.Write(a.Message);
                }// end of try

        }//end of main

        private int AuthConn(int option) {

            try {
                ChannelFactory<Interface> foobFactory;
                NetTcpBinding tcp = new NetTcpBinding();
                string URL = "net.tcp://localhost:8100/Authenticator";
                foobFactory = new ChannelFactory<Interface>(tcp, URL);
                auth = foobFactory.CreateChannel();
            } catch {
                Console.WriteLine("Port Error to TCP Authenticator");
            }

            switch (option) {
                case 1:
                    Console.WriteLine("Enter Name: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter Password: ");
                    string password = Console.ReadLine();

                    string result;
                    auth.Register(name, password, out result);

                    Console.WriteLine(result);
                    
                    break;

                case 2:
                    Console.WriteLine("Enter Name: ");
                    string name1 = Console.ReadLine();
                    Console.WriteLine("Enter Password: ");
                    string password1 = Console.ReadLine();

                    int result1;
                    auth.Login(name1, password1,out result1);

                    if(result1 != 0) {
                        Console.WriteLine(result1);
                    } else {
                        Console.WriteLine("Invalid Name and/or Password");
                    }                   

                    break;
                default:
                    Console.Write("Invalid Option Selected");
                    break;
            }// end of switch 

            return 0;

        }// end of authconn

    }// end of program
}
