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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window {

        private Interface auth;
        
        public class Token{
           public int result { get; set; }
        }

        private string name;
        private string password;
        
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

        private async void Login_Click(object sender, RoutedEventArgs e) {
            
            Task<Token> login = new Task<Token>(LogIn);
            login.Start();

            Loginb.IsEnabled = false;
            Backb.IsEnabled = false;
            progress.IsIndeterminate = true;

            Token res = await login;
            
            UpdateGUI(res);
            
        }// end of login click

        private Token LogIn() {
            try {
                this.Dispatcher.Invoke((Action)(() => {
                     name = NameBox.Text;
                     password = PasswordBox.Text;
                }));

                    int result;

                    auth.Login(name, password, out result);

                    Token tk = new Token();
                    tk.result = result;

                    return tk;
        
            } catch(Exception e) {
                MessageBox.Show(e.Message);
            }// end of try

            return null;
            
        }// end of LogIn

        private void UpdateGUI(Token t) {
            
            Application.Current.Dispatcher.Invoke(new Action((() => {
                Loginb.IsEnabled = true;
                Backb.IsEnabled = true;
                progress.Dispatcher.Invoke(() => progress.IsIndeterminate = false, DispatcherPriority.Background);
            })));

            if (t.result != 0) {
                Dashboard dashboard = new Dashboard(t.result);
                dashboard.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                dashboard.Show();
                this.Close();
            } else {
                MessageBox.Show("Invalid username or password");
            }// end of if else 
            
        }

        private void Back_Click(object sender, RoutedEventArgs e) {
            MainWindow main = new MainWindow();
            main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            main.Show();
            this.Close();
        }
    }// end of window
}
