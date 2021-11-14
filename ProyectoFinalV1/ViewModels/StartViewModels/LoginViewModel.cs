using Firebase.Auth;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using ProyectoFinalV1.Models;
using ProyectoFinalV1.Views;
using ProyectoFinalV1.Views.InAppPages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoFinalV1.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Atributos
        public string email;
        public string password;
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
        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(LoginMethod);
            }
        }
        #endregion
        #region Methods
        private async void LoginMethod()
        {
            this.IsVisibleTxt = true;
            this.IsRunningTxt = true;
            this.IsEnabledTxt = false;
            if (string.IsNullOrEmpty(this.email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar un correo.",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar una contraseña.",
                    "Aceptar");
                return;
            }

            try
            {
                App.autenticacion = await App.firebaseAuth.SignInWithEmailAndPasswordAsync(EmailTxt.ToString(), PasswordTxt.ToString());
                UserModel logeado = await App.firebaseBDD.getUserByEmail(this.email);
                if (logeado.email != "none")
                {
                    App.usuarioLogeado = logeado;
                }
                await Application.Current.MainPage.Navigation.PushAsync(new PrincipalTabbedPage());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
                
            }

            

            await Task.Delay(20);

            this.IsVisibleTxt = false;
            this.IsRunningTxt = false;
            this.IsEnabledTxt = true;

        }

        public async void ResetPasswordEmail()
        {
            string WebAPIkey = "AIzaSyBFwirWkzuv9RZX9lmhHFKqas9bYLHjwCE";

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                await authProvider.SendPasswordResetEmailAsync(email);
            }
            catch (Exception ex)
            {

            }

        }
        #endregion
        #region Constructor
        public LoginViewModel()
        {
            this.IsEnabledTxt = true;
        }
        #endregion
    }
}
