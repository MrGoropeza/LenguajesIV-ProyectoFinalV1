using GalaSoft.MvvmLight.Command;
using ProyectoFinalV1.Models;
using ProyectoFinalV1.Views.InAppPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoFinalV1.ViewModels.InAppViewModels
{
    public class AmigosViewModel : BaseViewModel
    {
        #region atributos
        private string busqueda;

        private ObservableCollection<UserModel> amigosCollection;
        private object amigoSelection;

        private bool noHayResultados;
        private bool isRefreshing;
        private bool isVisible;
        #endregion
        #region propiedades
        public string BusquedaTxt
        {
            get { return this.busqueda; }
            set { SetValue(ref this.busqueda, value); }
        }
        public ObservableCollection<UserModel> AmigosItems
        {
            get { return this.amigosCollection; }
            set { SetValue(ref this.amigosCollection, value); }
        }
        public object AmigoSelection
        {
            get { return this.amigoSelection; }
            set { SetValue(ref this.amigoSelection, value); }
        }
        public bool NoHayResultadosTxt
        {
            get { return this.noHayResultados; }
            set { SetValue(ref this.noHayResultados, value); }
        }
        public bool IsRefreshingTxt
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public bool IsVisibleTxt
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }
        #endregion
        #region Comandos
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(RefreshMethod);
            }
        }
        public ICommand AmigoSelectCommand
        {
            get
            {
                return new RelayCommand(AmigoSelectMethod);
            }
        }
        #endregion
        #region Metodos
        private async void LlenarAmigos()
        {
            try
            {
                var request = await App.firebaseBDD.getAmigosDeUser(App.usuarioLogeado.username);
                foreach(UserModel amigo in request)
                {
                    amigo.nomYapel = amigo.nombre + " " + amigo.apellido;
                    var imageRequest = await App.firebaseBDD.getImageUrlFromUser(amigo.username);
                    if (imageRequest.Equals("none"))
                    {
                        amigo.imageUrl = "defaultUser.png";
                    }
                    else
                    {
                        amigo.imageUrl = imageRequest;
                    }
                    AmigosItems.Add(amigo);
                }
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error",e.Message,"Aceptar");
            }
        }
        private async void RefreshMethod()
        {
            IsRefreshingTxt = true;
            IsVisibleTxt = true;
            AmigosItems.Clear();
            LlenarAmigos();
            await Task.Delay(1000);
            IsRefreshingTxt = false;
            IsVisibleTxt = false;
        }
        private async void AmigoSelectMethod()
        {
            if (amigoSelection != null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new DetalleUsuarioPage((UserModel)amigoSelection));
            }
            AmigoSelection = null;
        }
        #endregion
        #region Constructor
        public AmigosViewModel()
        {
            amigosCollection = new ObservableCollection<UserModel>();
            LlenarAmigos();
        }
        #endregion
    }
}
