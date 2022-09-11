﻿using Newtonsoft.Json;
using RestSharp;
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

namespace ClientGUI {
    /// <summary>
    /// Interaction logic for AvailableServices.xaml
    /// </summary>
    public partial class AvailableServices : Window {

        public class PublishModel {
            public string Name { get; set; }
            public string Description { get; set; }
            public string APIendpoint { get; set; }
            public string NumberOfOperands { get; set; }
            public string OperandType { get; set; }
        }

        int localtoken;
        
        string localport = "http://localhost:54473/";

        public AvailableServices(int token) {
            InitializeComponent();
            localtoken = token;
            Namet.Text = token.ToString();

            try {
                RestClient restClient = new RestClient(localport);
                RestRequest restRequest = new RestRequest("api/Registry/allServices", Method.Get);
                RestResponse restResponse = restClient.Execute(restRequest);
                
                List<PublishModel> publishModels = new List<PublishModel>();

                foreach (var item in JsonConvert.DeserializeObject<List<PublishModel>>(restResponse.Content)) {
                    list.Items.Add("Name: " + item.Name + "\nDescription: " + item.Description + "\nAPIendpoint: " + item.APIendpoint + "\nNumber of Operands: " + item.NumberOfOperands + "\nOperandType: " + item.OperandType + "\n");
                }

            } catch(Exception e){
                MessageBox.Show(e.Message);
                return;
            }


        }

        private void Back_Click(object sender, RoutedEventArgs e) {
            Dashboard dash = new Dashboard(localtoken);
            dash.Show();
            this.Close();
        }
    }
}
