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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window {

        public int localtoken;

        public Dashboard(int token) {
            InitializeComponent();
            localtoken = token;
            Namet.Text = token.ToString();
        }

        private void AllServices_Click(object sender, RoutedEventArgs e) {
            AvailableServices avail = new AvailableServices(localtoken);
            avail.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e) {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
