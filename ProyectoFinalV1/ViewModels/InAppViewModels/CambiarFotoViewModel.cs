using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoFinalV1.ViewModels.InAppViewModels
{
    public class CambiarFotoViewModel : BaseViewModel
    {
        #region atributos
        private string imageUrl;
        private MediaFile file;
        private Image imagen;

        private bool isVisible;
        private bool isRunning;
        #endregion
        #region propiedades
        public string ImageUrl
        {
            get { return this.imageUrl; }
            set { SetValue(ref this.imageUrl, value); }
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
        #endregion
        #region comandos
        public ICommand ElegirFotoCommand
        {
            get
            {
                return new RelayCommand(ElegirFotoMethod);
            }
        }
        public ICommand SubirFotoCommand
        {
            get
            {
                return new RelayCommand(SubirFotoMethod);
            }
        }
        #endregion
        #region metodos
        private async void ElegirFotoMethod()
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                imagen.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async void SubirFotoMethod()
        {
            IsRunningTxt = true;
            IsVisibleTxt = true;
            try
            {
                await App.firebaseBDD.AddProfilePicToUser(App.usuarioLogeado.username, file.GetStream());
                await App.Current.MainPage.DisplayAlert("Listo","Foto subida correctamente","Aceptar");
                await App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error","Intente subir de nuevo la imagen.","Aceptar");
            }
            IsRunningTxt = false;
            IsVisibleTxt = false;
        }
        #endregion
        #region constructor
        public CambiarFotoViewModel(Image imagen)
        {
            this.imagen = imagen;
            if (App.usuarioLogeado.imageUrl.Equals("none"))
            {
                this.imagen.Source = "defaultUser.png";
            }
            ImageUrl = App.usuarioLogeado.imageUrl;
        }
        #endregion
    }
}
