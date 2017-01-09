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

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Subproject
        {
            get { return subproject; }
            set { subproject = value; }
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

        async Task Submit()
        {
            isbusy = true;
            await Task.Delay(3000);
            isbusy = false;
        }
    }
}
