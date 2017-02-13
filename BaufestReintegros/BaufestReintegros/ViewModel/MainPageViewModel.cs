using BaufestReintegros.Model;
using BaufestReintegros.Model.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
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
            ResetCommand = new Command(async () => await Reset());
            Users = ServiceHelper.GetListUsers().User;
            SubUnit = ServiceHelper.GetComboChoices(SharepointFields.Subunidad).ToArray();
            Motive = ServiceHelper.GetComboChoices(SharepointFields.MotivoSolicitud).ToArray();
        }

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #region - Private Properties -

        private string _title;
        private User _selectedPersona;
        private string _selectedSubUnit;
        private string _subproject;
        private string _selectedMotive;
        private string _amount;
        private string _client;
        private string _autorization;
        private User _selectedPersonaAutorization;
        private bool _isbusy;
        private string _picturepath;

        #endregion

        #region - Public Properties -

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public User SelectedPersona
        {
            get { return _selectedPersona; }
            set
            {
                _selectedPersona = value;
                OnPropertyChanged();
            }
        }
        public string[] SubUnit { get; set; }
        public string SelectedSubUnit
        {
            get { return _selectedSubUnit; }
            set
            {
                _selectedSubUnit = value;
                OnPropertyChanged();
            }
        }
        public string SubProject
        {
            get { return _subproject; }
            set
            {
                _subproject = value;
                OnPropertyChanged();
            }
        }
        public string[] Motive { get; set; }
        public string SelectedMotive
        {
            get { return _selectedMotive; }
            set
            {
                _selectedMotive = value;
                OnPropertyChanged();
            }
        }
        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date { get; set; }
        public string Client
        {
            get { return _client; }
            set
            {
                _client = value;
                OnPropertyChanged();
            }
        }
        public string Autorization
        {
            get { return _autorization; }
            set
            {
                _autorization = value;
                OnPropertyChanged();
            }
        }
        public User SelectedPersonaAutorization
        {
            get { return _selectedPersonaAutorization; }
            set
            {
                _selectedPersonaAutorization = value;
                OnPropertyChanged();
            }
        }
        public User[] Users { get; set; }
        public bool IsBusy
        {
            get { return _isbusy; }
            set
            {
                _isbusy = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region - Commands -

        public Command SubmitCommand { get; }
        public Command AttachCommand { get; }
        public Command ResetCommand { get; }

        async Task Submit()
        {
            if (string.IsNullOrEmpty(_picturepath))
            {
                await Application.Current.MainPage.DisplayAlert("No picture attached", "You cannot submit without a picture", "OK");
                return;
            }

            IsBusy = true;

            Reintegro newReintegro = new Reintegro();
            newReintegro.Title = Title;
            newReintegro.Persona = SelectedPersona.ID + ";#" + SelectedPersona.LoginName;
            newReintegro.Subunidad = SelectedSubUnit;
            newReintegro.Subproyecto = SubProject;
            newReintegro.Motivo = SelectedMotive;
            newReintegro.Importe = Amount;
            //newReintegro.Fecha = Date;
            newReintegro.ClienteProyecto = Client;
            newReintegro.DebeAutorizar = Autorization;
            newReintegro.PersonaAutorizante = SelectedPersonaAutorization.ID + ";#" + SelectedPersonaAutorization.LoginName;

            int reintegroId = ServiceHelper.SubmitReintegro(newReintegro);

            if (reintegroId != 0)
            {
                ServiceHelper.AttachPicturesToReintegro(reintegroId, _picturepath);
                ClearFields();
                await Application.Current.MainPage.DisplayAlert("Reintegro Presentado", "La operacion se realizo exitosamente", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Reintegro Incorrecto", "An error ocurred", "OK");
            }
                        
            IsBusy = false;
        }

        async Task Attach()
        {
            IsBusy = true;

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });


            if (file == null)
                return;

            _picturepath = file.Path;

            IsBusy = false;

        }

        async Task Reset()
        {
            ClearFields();
        }

        #endregion

        #region - Methods -

        private void ClearFields()
        {
            Title = string.Empty;
            SelectedPersona = null;
            SelectedSubUnit = string.Empty;
            SubProject = string.Empty;
            SelectedMotive = null;
            Amount = string.Empty;
            Date = DateTime.MinValue;
            Client = string.Empty;
            Autorization = string.Empty;
            SelectedPersonaAutorization = null;
        }

        #endregion
    }
}
