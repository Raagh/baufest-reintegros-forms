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


        private string username;
        private string password;
        private string maintext = "Baufest Reintegros";
        private bool isbusy;


        public string Username
        {
            get { return username; }
            set{ username = value; }
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
        public bool IsBusy
        {
            get { return isbusy; }
            set
            {
                isbusy = value;
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
