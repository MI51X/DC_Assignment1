using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window {
        private Interface auth;

        public class Reg {
            public string status { get; set; }
        }

        private string name;
        private string password;

        public Registration() {
            InitializeComponent();
            ChannelFactory<Interface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8100/Authenticator";
            try {
                foobFactory = new ChannelFactory<Interface>(tcp, URL);
                auth = foobFactory.CreateChannel();
            } catch {
                MessageBox.Show("Server is offline");
            }// end of try catch
        }// end of registratiopm class

        private async void Register_Click(object sender, RoutedEventArgs e) {

            Task<Reg> register = new Task<Reg>(Insert);
            register.Start();

            Loginb.IsEnabled = false;
            Backb.IsEnabled = false;
            progress.IsIndeterminate = true;

            Reg st = await register;

            UpdateGUI(st);

        }// end of register click

        private Reg Insert() {
            this.Dispatcher.Invoke((Action)(() => {
                name = NameBox.Text;
                password = PasswordBox.Text;
            }));

            string result;
            
            auth.Register(name, password, out result);

            Reg r = new Reg();
            r.status = result;

            return r;

        } // end of insert
        
        private void UpdateGUI(Reg r) {
            Application.Current.Dispatcher.Invoke(new Action((() => {
                Loginb.IsEnabled = true;
                Backb.IsEnabled = true;
                progress.Dispatcher.Invoke(() => progress.IsIndeterminate = false, DispatcherPriority.Background);
            })));

            MessageBox.Show(r.status);
        }

        private void Back_Click(object sender, RoutedEventArgs e) {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }// end of window
}
