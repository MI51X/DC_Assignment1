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

            progress.Dispatcher.Invoke(() => progress.IsIndeterminate = true, DispatcherPriority.Background);


            AvailableServices services = new AvailableServices(localtoken) ;
            services.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            services.Show();
            this.Close();

        }


        private void Logout_Click(object sender, RoutedEventArgs e) {
            MainWindow main = new MainWindow();
            main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            main.Show();
            this.Close();
        }

        private void SearchServicesButton_Click(object sender, RoutedEventArgs e)
        {
            SearchService searchService = new SearchService(localtoken);
            searchService.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            searchService.Show();
            this.Close();
        }
    }
}
