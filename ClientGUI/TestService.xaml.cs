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
using RestSharp;
using Newtonsoft.Json;
using System.Windows.Threading;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for TestService.xaml
    /// </summary>
    public partial class TestService : Window
    {
        public class OutputJson
        {
            public int output;
        }

        int localToken;

        string serviceProviderPort = "http://localhost:52776/";
        
        int noOfOperands;

        SearchService.PublishModel savedPublishModel;
        public TestService(int token, SearchService.PublishModel publishModel)
        {
            InitializeComponent();

            localToken = token;
            savedPublishModel = publishModel;

            ServiceNameBox.Text = "Name: " + publishModel.Name;
            ServiceDescriptionBox.Text = "Description: " + publishModel.Description;
            ServiceApiEndpointBox.Text = "API Endpoint: " + publishModel.APIendpoint;
            ServiceOperandTypeBox.Text = "Operand Type: " + publishModel.OperandType;

            noOfOperands = publishModel.NumberOfOperands;

            for (int i = 0; i < noOfOperands; i++)
            {
                TextBox textBox = new TextBox { Text = "Number " + (i + 1).ToString(), Name = "NumTextBox" + (i + 1).ToString() };
                numStackPanel.Children.Add(textBox);
                numStackPanel.RegisterName(textBox.Name, textBox);
            }
        }

        private async void TestServiceButton_Click(object sender, RoutedEventArgs e)
        {
            progress.Dispatcher.Invoke(() => progress.IsIndeterminate = true, DispatcherPriority.Background);
            Task TS = new Task(TService);
            TS.Start();
            await TS;
            progress.Dispatcher.Invoke(() => progress.IsIndeterminate = false, DispatcherPriority.Background);
        }

        public void TService() {
            this.Dispatcher.Invoke((Action)(() => {


            RestClient restClient = new RestClient(serviceProviderPort);
            RestRequest restRequest = new RestRequest(savedPublishModel.APIendpoint, Method.Get);
            restRequest.AddParameter("token", localToken);

            for (int i = 0; i < noOfOperands; i++) {
                TextBox textBox = (TextBox)this.numStackPanel.FindName("NumTextBox" + (i + 1));
                restRequest.AddParameter("num" + (i + 1), textBox.Text);
            }

            RestResponse restResponse = restClient.Execute(restRequest);

            MessageBox.Show("Answer = " + JsonConvert.DeserializeObject<OutputJson>(restResponse.Content).output, "Answer", MessageBoxButton.OK, MessageBoxImage.Information);

            
                AnswerTextBlock.Text = "Answer = " + JsonConvert.DeserializeObject<OutputJson>(restResponse.Content).output;
            }));
           
        }
    }
}
