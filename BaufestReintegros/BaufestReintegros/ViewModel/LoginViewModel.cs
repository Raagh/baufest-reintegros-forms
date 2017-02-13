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


        private string _username;
        private string _password;
        private string _maintext = "BAUFEST REINTEGROS";
        private bool _isbusy;


        public string Username
        {
            get { return _username; }
            set{ _username = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public string MainText
        {
            get { return _maintext; }
            set { _maintext = value; }
        }
        public bool IsBusy
        {
            get { return _isbusy; }
            set
            {
                _isbusy = value;
                OnPropertyChanged();
            }
        }
        public Command LoginCommand { get; }


        async Task Login()
        {
            NetworkCredential credentials = new NetworkCredential();
            credentials.UserName = _username;
            credentials.Password = _password;
            credentials.Domain = "baunet";
            IsBusy = true;
           
            var result = ServiceHelper.AuthenticateCredentials(credentials);

            IsBusy = false;
        
            if (result)
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            else
                await Application.Current.MainPage.DisplayAlert("Login", "Incorrect User and/or Password", "OK");
          
        }
    }
}
