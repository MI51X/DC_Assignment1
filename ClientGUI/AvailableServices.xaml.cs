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
        int localtoken;

        public AvailableServices(int token) {
            InitializeComponent();
            localtoken = token;
            Namet.Text = token.ToString();
        }

        private void Back_Click(object sender, RoutedEventArgs e) {
            Dashboard dash = new Dashboard(localtoken);
            dash.Show();
            this.Close();
        }
    }
}
