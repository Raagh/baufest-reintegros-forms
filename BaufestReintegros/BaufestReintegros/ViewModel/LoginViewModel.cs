using BaufestReintegros.Model.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BaufestReintegros.ViewModel 
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login());
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private string username = "pferraggi";
        private string password;
        private string maintext = "Baufest Sharepoint Login";
        private string loginresult;


        public string Username
        {
            get { return username; }
            set
            {
                username = value;
            }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string MainText
        {
            get { return maintext; }
            set { maintext = value; }
        }
        public string LoginResult
        {
            get { return loginresult; }
            set
            {
                loginresult = value;
                OnPropertyChanged();
            }
        }

        public Command LoginCommand { get; }

        async Task Login()
        {
            NetworkCredential credentials = new NetworkCredential();

            credentials.UserName = username;
            credentials.Password = password;
            credentials.Domain = "baunet";

            var result = ServiceHelper.AuthenticateCredentials(credentials);

            LoginResult = result ? "Logeo Correcto" : "Logeo Incorrecto";

        }
    }
}
