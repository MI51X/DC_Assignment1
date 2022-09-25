using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Newtonsoft.Json;
using RestSharp;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for SearchService.xaml
    /// </summary>
    public partial class SearchService : Window
    {
        public class PublishModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string APIendpoint { get; set; }
            public int NumberOfOperands { get; set; }
            public string OperandType { get; set; }
        }

        public class ResponseModel
        {
            public string Status { get; set; }
            public string Reason { get; set; }
        }

        int localToken;

        string localPort = "http://localhost:54473/";

        List<PublishModel> availableServices = new List<PublishModel>();

        public SearchService(int token)
        {
            InitializeComponent();
            ServiceSelectButton.IsEnabled = false ;
            localToken = token;
        }

        private async void ServiceSearchButton_Click(object sender, RoutedEventArgs e)
        {
            progress.Dispatcher.Invoke(() => progress.IsIndeterminate = true, DispatcherPriority.Background);
            Task search = new Task(Search);
            search.Start();

            await search;

            progress.Dispatcher.Invoke(() => progress.IsIndeterminate = false, DispatcherPriority.Background);
        }

        public void Search() {
            this.Dispatcher.Invoke((Action)(() => {
                ServiceListBox.Items.Clear();
            }));

            RestClient restClient = new RestClient(localPort);
            RestRequest restRequest = new RestRequest("api/registry/search", Method.Get);

            this.Dispatcher.Invoke((Action)(() => {
                restRequest.AddParameter("searchname", ServiceSearchBox.Text);
            }));

            restRequest.AddParameter("token", localToken);
            RestResponse restResponse = restClient.Execute(restRequest);

            try {
                availableServices = JsonConvert.DeserializeObject<List<PublishModel>>(restResponse.Content);
               
                
                Application.Current.Dispatcher.Invoke(new Action((() => {
                    if (availableServices != null) { ServiceSelectButton.IsEnabled = true; }
                    foreach (var service in availableServices) {
                    ServiceListBox.Items.Add("Name: " + service.Name + "\nDescription: " + service.Description + "\nAPI Endpoint: " + service.APIendpoint + "\nNumber of Operands: " + service.NumberOfOperands + "\nOperand Type: " + service.OperandType + "\n");
                    }
                })));
                
            } catch(Exception e) {
                ResponseModel errorResponse = JsonConvert.DeserializeObject<ResponseModel>(restResponse.Content);
                if(errorResponse.Status == "Denied")
                {
                    MessageBox.Show("Error: Your authentication token has expired, please log in again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }else if(errorResponse.Status == "Authentication Server Offline")
                {
                    MessageBox.Show("Error: The authenticator service is offline, please make sure it's running & log in again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Error: " + e + "\nServer Response: " + restResponse.Content, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Application.Current.Dispatcher.Invoke(new Action((() => {
                    this.Close();
                })));
            }
        }

        private void ServiceSelectButton_Click(object sender, RoutedEventArgs e)
        {
            int index = ServiceListBox.SelectedIndex;
            PublishModel selectedService = new PublishModel();
            selectedService = availableServices[index];

            TestService testService = new TestService(localToken, selectedService);
            testService.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            testService.Show();
        }
    }
}
