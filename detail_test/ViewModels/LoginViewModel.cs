
using System;  
using System.ComponentModel;  
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using detail_test.Services;

namespace detail_test.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public static string ServerConnection;
        public static string Username;
        public static string Code;
        public static int SubscriptionLevel;
        public static int ProgressPoint;

        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public bool SubmitInfo()
        { 
            if (email==null || password == null)
            {
                //turn on for deploy
                DisplayInvalidLoginPrompt();
                ServerConnection = "";
                Username = email;
                Code = password;
                SubscriptionLevel = 0;
                ProgressPoint = 0;
            
                return false;
            }
            MockServer mockServer = new MockServer("",out string ServerConn);
            bool b= mockServer.checkServer(ServerConn,email, password,true, out int sub, out int prog);
            if (b)
            {
                ServerConnection = ServerConn;
                Username= email;
                Code=password;
                SubscriptionLevel=sub;
                ProgressPoint=prog;
                return true;
            }
            else
            {
                DisplayInvalidLoginPrompt();
                ServerConnection = ServerConn;
                Username = email;
                Code = password;
                SubscriptionLevel = 0;
                ProgressPoint = 0;
                return false;
            }


        }
    }
}