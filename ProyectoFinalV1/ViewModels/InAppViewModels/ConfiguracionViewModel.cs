using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoFinalV1.ViewModels.InAppViewModels
{
    public class ConfiguracionViewModel : BaseViewModel
    {
        #region Atributos
        public string username;
        public string email;
        public string nombre;
        public string apellido;
        public string edad;

        public bool isVisible;
        public bool isRunning;
        public bool isEnabled;
        #endregion
        #region Propiedades
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
        public string UserNameTxt
        {
            get { return this.username; }
            set { SetValue(ref this.username, value); }
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
        public bool IsVisibleTxt
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }
        public bool IsRunningTxt
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsEnabledTxt
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion
        #region Comandos
        public ICommand CambiarDatosCommand
        {
            get
            {
                return new RelayCommand(CambiarDatosMethod);
            }
        }
        public ICommand EnviarCambiosCommand
        {
            get
            {
                return new RelayCommand(EnviarCambiosMethod);
            }
        }
        public ICommand CancelarCambiosCommand
        {
            get
            {
                return new RelayCommand(CancelarCambiosMethod);
            }
        }
        public ICommand CambiarPassCommand
        {
            get
            {
                return new RelayCommand(CambiarPassMethod);
            }
        }
        public ICommand LogoutCommand
        {
            get
            {
                return new RelayCommand(LogoutMethod);
            }
        }
        #endregion
        #region Metodos
        private void CambiarDatosMethod()
        {
            IsEnabledTxt = true;
            IsVisibleTxt = true;
        }
        private void CancelarCambiosMethod()
        {
            IsEnabledTxt = false;
            IsVisibleTxt = false;
        }
        private async void EnviarCambiosMethod()
        {
            IsEnabledTxt = false;
            IsRunningTxt = true;
            try
            {
                App.usuarioLogeado.apellido = ApellidoTxt;
                App.usuarioLogeado.nombre = NombreTxt;
                App.usuarioLogeado.edad = EdadTxt;
                App.autenticacion = await App.autenticacion.GetFreshAuthAsync();
                await App.firebaseBDD.UpdateDatosUser(App.usuarioLogeado);
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "Aceptar");
                LlenarDatosFromServer();
            }

            IsVisibleTxt = false;
            IsRunningTxt = false;
            IsVisibleTxt = false;
        }
        private async void CambiarPassMethod()
        {
            try
            {
                await App.firebaseAuth.SendPasswordResetEmailAsync(email);
                await App.Current.MainPage.DisplayAlert("Cambiar Contraseña", "Correo para cambiar contraseña enviado correctamente", "Aceptar");
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error",e.Message,"Aceptar");
            }
            
        }
        private async void LogoutMethod()
        {
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }
        private void LlenarDatosFromServer()
        {
            UserNameTxt = App.usuarioLogeado.username;
            EmailTxt = App.usuarioLogeado.email;
            NombreTxt = App.usuarioLogeado.nombre;
            ApellidoTxt = App.usuarioLogeado.apellido;
            EdadTxt = App.usuarioLogeado.edad;
        }
        #endregion
        #region Constructor
        public ConfiguracionViewModel()
        {
            isEnabled = false;
            LlenarDatosFromServer();
        }
        #endregion
    }
}
