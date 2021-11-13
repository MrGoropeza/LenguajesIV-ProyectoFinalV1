using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoFinalV1.ViewModels.StartViewModels
{
    public class RecuperarClaveViewModel : BaseViewModel
    {
        #region Atributos
        public string email;


        public bool isEnabled;
        public bool isRunning;
        public bool isVisible;
        #endregion

        #region Propiedades
        public string EmailTxt
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
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
        public ICommand RecuperarCommand
        {
            get
            {
                return new RelayCommand(RecuperarMethod);
            }
        }


        #endregion

        #region Metodos
        private async void RecuperarMethod()
        {
            try
            {
                await App.firebaseAuth.SendPasswordResetEmailAsync(email);
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Alert", e.Message, "OK");
            }
            IsRunningTxt = true;
            IsVisibleTxt = true;
            IsEnabledTxt = false;

            await Task.Delay(20);
            await Application.Current.MainPage.Navigation.PopAsync();

        }
        #endregion

        #region Constructor
        public RecuperarClaveViewModel()
        {
            IsEnabledTxt = true;
        }
        #endregion
    }
}
