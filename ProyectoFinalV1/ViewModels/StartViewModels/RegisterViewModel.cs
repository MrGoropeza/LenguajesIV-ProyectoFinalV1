using Firebase.Auth;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using ProyectoFinalV1.Models;
using ProyectoFinalV1.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoFinalV1.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Attributes
        public string email;
        public string password;
        public string username;
        public string nombre;
        public string apellido;
        public string edad;
        private string firebaseToken;

        private bool isInserted;
        public bool isRunning;
        public bool isVisible;
        public bool isEnabled;
        #endregion

        #region Properties
        public string EmailTxt
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string PasswordTxt
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public string UserNameTxt
        {
            get { return this.username; }
            set { SetValue(ref this.username, value); }
        }
        public string NameTxt
        {
            get { return this.nombre; }
            set { SetValue(ref this.nombre, value); }
        }
        public string ApellidoTxt
        {
            get { return this.apellido; }
            set { SetValue(ref this.apellido, value); }
        }
        public string AgeTxt
        {
            get { return this.edad; }
            set { SetValue(ref this.edad, value); }
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
        public bool IsRunningTxt
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        #endregion

        #region Commands
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(RegisterMethod);
            }
        }
        #endregion
        #region Methods
        private async void RegisterMethod()
        {
            if (string.IsNullOrEmpty(this.username))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar un nombre de usuario",
                    "Accept");
                return;
            }
            
            List<string> usernamesExistentes = await App.firebaseBDD.getAllUsernames();
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
                    "Debes ingresar un correo.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar una contraseña.",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.nombre))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar tu nombre.",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.edad))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar tu edad.",
                    "Accept");
                return;
            }
            string WebAPIkey = "AIzaSyB_W2TRS2rCXcjfY3UAswlKKP_t_I5IKY0";

            IsRunningTxt = true;
            IsVisibleTxt = true;

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            
            try
            {

                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(EmailTxt.ToString(), PasswordTxt.ToString());
                firebaseToken = auth.FirebaseToken;
                auth.User.LastName = this.apellido;
                auth.User.FirstName = this.nombre;
                this.isInserted = false;
                UserModel nuevoUsuario = new UserModel();
                nuevoUsuario.apellido = this.apellido;
                nuevoUsuario.edad = this.edad;
                nuevoUsuario.nombre = this.nombre;
                nuevoUsuario.username = this.username;
                nuevoUsuario.email = this.email;
                await App.firebaseBDD.AddUser(nuevoUsuario);
                this.isInserted = true;
                IsRunningTxt = false;
                IsVisibleTxt = false;
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await authProvider.DeleteUserAsync(firebaseToken);
                if (!isInserted)
                {
                    await App.firebaseBDD.RemoveUser(this.username);
                }
                
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }

        }
        #endregion
        #region Constructor
        public RegisterViewModel()
        {
            IsEnabledTxt = true;
        }
        #endregion
    }
}
