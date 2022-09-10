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

namespace ClientGUI {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window {

        private Interface auth;

        public Login() {
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
        }// end of login class

        private void Login_Click(object sender, RoutedEventArgs e) {

            string name = NameBox.Text;
            string password = PasswordBox.Text;

            int result;

            auth.Login(name, password, out result);

            MessageBox.Show(result.ToString());
            
        }// end of login click

        private void Back_Click(object sender, RoutedEventArgs e) {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }// end of window
}
