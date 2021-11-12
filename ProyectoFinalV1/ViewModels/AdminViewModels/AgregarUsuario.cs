using Firebase.Auth;
using Firebase.Database;
using GalaSoft.MvvmLight.Command;
using ProyectoFinalV1.Models;
using ProyectoFinalV1.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoFinalV1.ViewModels.AdminViewModels
{
    public class AgregarUsuario : BaseViewModel
    {
        #region Atributos
        public string username;
        public string email;
        public string nombre;
        public string apellido;
        public string edad;
        public string password;
        public bool isRunning;
        public bool isVisible;
        public bool isEnabled;
        #endregion
        #region Propiedades
        public string UsernameTxt
        {
            get { return this.username; }
            set { SetValue(ref this.username, value); }
        }
        public string EmailTxt
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string NombreTxt
        {
            get { return this.nombre; }
            set { SetValue(ref this.nombre, value); }
        }
        public string ApellidoTxt
        {
            get { return this.apellido; }
            set { SetValue(ref this.apellido, value); }
        }
        public string EdadTxt
        {
            get { return this.edad; }
            set { SetValue(ref this.edad, value); }
        }
        public string PasswordTxt
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public bool IsRunningTxt
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsVisibleTxt
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }
        public bool IsEnabledTxt
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion
        #region Comandos
        public ICommand AgregarUsuarioCommand
        {
            get
            {
                return new RelayCommand(AgregarUsuarioMethod);
            }
        }
        #endregion
        #region Metodos
        private async void AgregarUsuarioMethod()
        {
            if (string.IsNullOrEmpty(this.username))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email.",
                    "Accept");
                return;
            }
            FirebaseProvider firebaseBDD = new FirebaseProvider();
            List<string> usernamesExistentes = await firebaseBDD.getAllUsernames();
            if (usernamesExistentes.Contains(this.username))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Nombre de Usuario ya existente",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password.",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.nombre))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a name.",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.edad))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an age.",
                    "Accept");
                return;
            }
            string WebAPIkey = "AIzaSyB_W2TRS2rCXcjfY3UAswlKKP_t_I5IKY0";

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(EmailTxt.ToString(), PasswordTxt.ToString());
                string gettoken = auth.FirebaseToken;
                UserModel nuevoUsuario = new UserModel();
                nuevoUsuario.apellido = this.apellido;
                nuevoUsuario.edad = this.edad;
                nuevoUsuario.nombre = this.nombre;
                nuevoUsuario.UID = gettoken;
                nuevoUsuario.username = this.username;


                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
            
        }
        #endregion
        #region Constructor
        public AgregarUsuario()
        {
            this.isEnabled = true;
        }
        #endregion
    }
}
