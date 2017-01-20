using BaufestReintegros.Model;
using BaufestReintegros.Model.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace BaufestReintegros.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            SubmitCommand = new Command(async () => await Submit());
            AttachCommand = new Command(async () => await Attach());
            Users = ServiceHelper.GetListUsers().User;
            SubUnit = ServiceHelper.GetComboChoices("Subunidad").ToArray();
            Motive = ServiceHelper.GetComboChoices("Motivo_x0020_de_x0020_Solicitud").ToArray();
        }

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private string title;
        private string subproject;
        private string amount;
        private string client;
        private string autorization;
        private bool isbusy;
        private User[] users;
        private string[] subunit;
        private string[] motive;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string[] SubUnit
        {
            get { return subunit; }
            set { subunit = value; }
        }
        public string SubProject
        {
            get { return subproject; }
            set { subproject = value; }
        }
        public string[] Motive
        {
            get { return motive; }
            set { motive = value; }
        }
        public string Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public string Client
        {
            get { return client; }
            set { client = value; }
        }
        public string Autorization
        {
            get { return autorization; }
            set { autorization = value; }
        }
        public User[] Users
        {
            get { return users; }
            set { users = value; }
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
        public Command SubmitCommand { get; }
        public Command AttachCommand { get; }

        async Task Submit()
        {
            isbusy = true;
            await Task.Delay(3000);
            isbusy = false;
        }

        async Task Attach()
        {
            await Task.Delay(3000);
        }
    }
}
