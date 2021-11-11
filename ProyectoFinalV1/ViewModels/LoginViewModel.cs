using Firebase.Auth;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
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
            string WebAPIkey = "AIzaSyB_W2TRS2rCXcjfY3UAswlKKP_t_I5IKY0";

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(EmailTxt.ToString(), PasswordTxt.ToString());
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);

                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
                //await Application.Current.MainPage.Navigation.PushAsync(new pruebas());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Invalid useremail or password", "OK");
            }

            this.IsVisibleTxt = true;
            this.IsRunningTxt = true;
            this.IsEnabledTxt = false;

            await Task.Delay(20);

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
