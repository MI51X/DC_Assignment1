using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
//using Registry.Models;
using Newtonsoft.Json;


namespace ServicePublishing {

    class Program {

        private Interface auth;
        public int token;
        public class ResponseModel
        {
            public string Status { get; set; }
            public string Reason { get; set; }
        }
        public class PublishModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string APIendpoint { get; set; }
            public string NumberOfOperands { get; set; }
            public string OperandType { get; set; }
        }

        static void Main(string[] args) {

            var mc = new Program();
            mc.Menu();
                

        }//end of main

         void Menu() {
            int option;// for program options

            Console.WriteLine("Choose an Option to get started \n1.Register\n2.login\n");
            option = Int32.Parse(Console.ReadLine());

            var mc = new Program();

            try {
                mc.AuthConn(option);
            } catch (Exception a) {
                Console.Write(a.Message);
            }// end of try
        }

        public int AuthConn(int option) {

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
                        token = result1;

                        Program p = new Program();
                        p.Publish(result1);
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

        public void Publish(int token) {


            int option;// for program options

            Console.WriteLine("\n\n\nChoose an Option to get started \n1.Publish service\n2.Unpublish service\n");
            option = Int32.Parse(Console.ReadLine());


            switch (option) {
                case 1:
                    Console.WriteLine("\n\n-------------------PUBLISH A NEW SERVICE-------------------\n\n");
                    Console.WriteLine("Enter the requested details of the service to be published.\n");

                    Console.WriteLine("Service Name: ");
                    string serviceName = Console.ReadLine();
                    Console.WriteLine("Description: ");
                    string serviceDescription = Console.ReadLine();
                    Console.WriteLine("API Endpoint: ");
                    string serviceApiEndpoint = Console.ReadLine();
                    Console.WriteLine("Number of Operands: ");
                    string serviceNumOperands = Console.ReadLine();
                    Console.WriteLine("Operand Type: ");
                    string serviceOperandType = Console.ReadLine();

                    PublishModel publishModel = new PublishModel();

                    publishModel.Name = serviceName;
                    publishModel.Description = serviceDescription;
                    publishModel.APIendpoint = serviceApiEndpoint;
                    publishModel.NumberOfOperands = serviceNumOperands;
                    publishModel.OperandType = serviceOperandType;

                    RestClient restClient = new RestClient("http://localhost:54473/");
                    RestRequest restRequest = new RestRequest("api/Registry/publish?token="+ token, Method.Post);

                    /*Console.WriteLine(publishModel.NumberOfOperands);
                    Console.WriteLine(serviceNumOperands);
                    Console.WriteLine(token);*/

                   // restRequest.AddParameter("token",token);
                    restRequest.AddJsonBody(JsonConvert.SerializeObject(publishModel));

                    RestResponse restResponse = restClient.Execute(restRequest);

                    if (JsonConvert.DeserializeObject<ResponseModel>(restResponse.Content).Status == "Denied" || JsonConvert.DeserializeObject<ResponseModel>(restResponse.Content).Status == "Authentication Server Offline")
                    {
                        Console.WriteLine("\nError: Your authentication token has expired, please log in again.\n");
                        var mc = new Program();
                        mc.Menu();
                    }

                    if (restResponse.Content != null)
                    {
                        Console.WriteLine("\nPublished new service successfully!\n");
                        Console.WriteLine(restResponse.Content);
                        Program p = new Program();
                        p.Publish(token);
                    }
                    else
                    {
                        Console.WriteLine("\nFailed to publish new service!\n");
                        Program p = new Program();
                        p.Publish(token);
                    }

                    /*Console.WriteLine("\nPublished new service successfully!\n");
                    Console.WriteLine(restResponse.Content);
                    Program p2 = new Program();
                    p2.Publish(token);*/

                    break;

                case 2:
                    Console.WriteLine("\n\n-------------------UNPUBLISH AN EXISTING SERVICE-------------------\n\n");

                    Console.WriteLine("List of existing services and their API endpoints:\n");

                    RestClient restClient1 = new RestClient("http://localhost:54473/");
                    RestRequest allServices = new RestRequest("api/Registry/allServices?token=" + token, Method.Get);

                    RestResponse allServicesResponse = restClient1.Execute(allServices);

                    List<PublishModel> serviceList = JsonConvert.DeserializeObject<List<PublishModel>>(allServicesResponse.Content);

                    foreach (PublishModel service in serviceList) {
                        Console.WriteLine("Service Name: " + service.Name);
                        Console.WriteLine("API Endpoint: " + service.APIendpoint + "\n");
                    }

                    Console.WriteLine("\nEnter the requested details of the service to be unpublished.\n");
                    Console.WriteLine("API Endpoint: ");
                    string unpublishServiceApiEndpoint = Console.ReadLine();

                    RestRequest restRequest1 = new RestRequest("api/Registry/unpublish/{endpoint}?token=" + token, Method.Delete);

                   // restRequest1.AddParameter("token", token);
                    restRequest1.AddUrlSegment("endpoint", unpublishServiceApiEndpoint);

                    RestResponse restResponse1 = restClient1.Execute(restRequest1);

                    /*if (JsonConvert.DeserializeObject<ResponseModel>(restResponse1.Content).Status == "Denied" || JsonConvert.DeserializeObject<ResponseModel>(restResponse1.Content).Status == "Authentication Server Offline")
                    {
                        Console.WriteLine("\nError: Your authentication token has expired, please log in again.\n");
                        var mc = new Program();
                        mc.Menu();
                    }*/

                    if (restResponse1.Content != null) {
                        Console.WriteLine("\nUnpublished service successfully!\n");
                        Program p = new Program();
                        p.Publish(token);
                    } else {
                        Console.WriteLine("\nFailed to unpublish service!\n");
                        Program p = new Program();
                        p.Publish(token);
                    }

                    break;

                    default:
                    Console.Write("Invalid Option Selected");
                    break;

            }// end of switch

        }// end of publish

    }// end of program
}
