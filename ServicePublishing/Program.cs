using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Registry.Models;
using Newtonsoft.Json;


namespace ServicePublishing {

    class Program {

        private Interface auth;

        static void Main(string[] args) {

            int option;// for program options

                Console.WriteLine("Choose an Option to get started \n1.Register\n2.login\n3.Publish service\n4.Unpublish service\n");
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

                case 3:
                    Console.WriteLine("\n\n-------------------PUBLISH A NEW SERVICE-------------------\n\n");
                    Console.WriteLine("Enter the requested details of the service to be published.\n");
                   
                    Console.WriteLine("Service Name: ");
                    string serviceName = Console.ReadLine();
                    Console.WriteLine("Description: ");
                    string serviceDescription = Console.ReadLine();
                    Console.WriteLine("API Endpoint: ");
                    string serviceApiEndpoint = Console.ReadLine();
                    Console.WriteLine("Number of Operands: ");
                    int serviceNumOperands = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Operand Type: ");
                    string serviceOperandType = Console.ReadLine();

                    PublishModel publishModel = new PublishModel();

                    publishModel.Name = serviceName;
                    publishModel.Description = serviceDescription;
                    publishModel.APIendpoint = serviceApiEndpoint;
                    publishModel.NumberOfOperands = serviceNumOperands;
                    publishModel.OperandType = serviceOperandType;

                    RestClient restClient = new RestClient("http://localhost:54473/");
                    RestRequest restRequest = new RestRequest("api/Registry/publish");

                    restRequest.AddJsonBody(publishModel);

                    RestResponse restResponse = restClient.Post(restRequest);

                    if(restResponse.Content != null)
                    {
                        Console.WriteLine("\nPublished new service successfully!\n");
                    }
                    else
                    {
                        Console.WriteLine("\nFailed to publish new service!\n");
                    }

                    break;

                case 4:
                    Console.WriteLine("\n\n-------------------UNPUBLISH AN EXISTING SERVICE-------------------\n\n");

                    Console.WriteLine("List of existing services and their API endpoints:\n");

                    RestClient restClient1 = new RestClient("http://localhost:54473/");
                    RestRequest allServices = new RestRequest("api/Registry/allServices", Method.Get);

                    RestResponse allServicesResponse = restClient1.Execute(allServices);

                    List<PublishModel> serviceList = JsonConvert.DeserializeObject<List<PublishModel>>(allServicesResponse.Content);

                    foreach(PublishModel service in serviceList)
                    {
                        Console.WriteLine("Service Name: " + service.Name);
                        Console.WriteLine("API Endpoint: " + service.APIendpoint + "\n");
                    }

                    Console.WriteLine("\nEnter the requested details of the service to be unpublished.\n");
                    Console.WriteLine("API Endpoint: ");
                    string unpublishServiceApiEndpoint = Console.ReadLine();

                    RestRequest restRequest1 = new RestRequest("api/Registry/unpublish/{endpoint}", Method.Delete);

                    restRequest1.AddUrlSegment("endpoint", unpublishServiceApiEndpoint);

                    RestResponse restResponse1 = restClient1.Execute(restRequest1);

                    if (restResponse1.Content != null)
                    {
                        Console.WriteLine("\nUnpublished service successfully!\n");
                    }
                    else
                    {
                        Console.WriteLine("\nFailed to unpublish service!\n");
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
