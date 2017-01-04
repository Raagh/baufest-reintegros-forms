using BaufestReintegros.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;

namespace BaufestReintegros.ViewModel 
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private string username = "pferraggi";
        private string password;
        private string maintext = "Baufest Sharepoint Login";


        public string Username
        {
            get { return username; }
            set { username = value; }
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


        public void LoginCommand()
        {
            NetworkCredential credentials = new NetworkCredential();

            credentials.UserName = username;
            credentials.Password = password;
            credentials.Domain = "baunet";

            ServiceHelper.AuthenticateCredentials(credentials);
        }
    }
}
