using GalaSoft.MvvmLight.Command;
using ProyectoFinalV1.Models;
using ProyectoFinalV1.Views.InAppPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoFinalV1.ViewModels.InAppViewModels
{
    public class SearchAmigoViewModel : BaseViewModel
    {
        #region atributos
        private string busqueda;

        private ObservableCollection<UserModel> searchCollection;
        private object busquedaSelection;

        private int itemTreshold;



        private bool noHayResultados;
        private bool isRunning;
        private bool isRefreshing;
        private bool isVisible;
        #endregion
        #region propiedades
        public string BusquedaTxt
        {
            get { return this.busqueda; }
            set { SetValue(ref this.busqueda, value); }
        }
        public ObservableCollection<UserModel> SearchItems
        {
            get { return this.searchCollection; }
            set { SetValue(ref this.searchCollection, value); }
        }
        public object BusquedaSelection
        {
            get { return this.busquedaSelection; }
            set { SetValue(ref this.busquedaSelection, value); }
        }
        public int ItemTreshold
        {
            get { return this.itemTreshold; }
            set { SetValue(ref this.itemTreshold, value); }
        }
        public bool NoHayResultadosTxt
        {
            get { return this.noHayResultados; }
            set { SetValue(ref this.noHayResultados, value); }
        }
        public bool IsRunningTxt
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
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
        public ICommand BuscarCommand
        {
            get
            {
                return new RelayCommand(BuscarMethod);
            }
        }
        public ICommand BusquedaSelectCommand
        {
            get
            {
                return new RelayCommand(BusquedaSelectMethod);
            }
        }
        #endregion
        #region Metodos
        private async void RefreshMethod()
        {
            IsRunningTxt = true;
            IsVisibleTxt = true;
            searchCollection.Clear();
            await LlenarBusqueda();
            await Task.Delay(1000);
            IsVisibleTxt = false;
            IsRunningTxt = false;
        }
        private async Task LlenarBusqueda()
        {
            try
            {
                var request = await App.firebaseBDD.getAllUsersOnceAsync();
                if (request.Count() > 0)
                {
                    NoHayResultadosTxt = false;
                    foreach (var item in request)
                    {
                        if (item.apellido.ToLowerInvariant().Contains(BusquedaTxt)
                            || item.nombre.ToLowerInvariant().Contains(BusquedaTxt)
                            || item.username.ToLowerInvariant().Contains(BusquedaTxt))
                        {
                            item.nomYapel = item.nombre + " " + item.apellido;
                            var imageRequest = await App.firebaseBDD.getImageUrlFromUser(item.username);
                            if (imageRequest.Equals("none"))
                            {
                                item.imageUrl = "defaultUser.png";
                            }
                            else
                            {
                                item.imageUrl = imageRequest;
                            }
                            SearchItems.Add(item);
                        }
                    }
                }
                else
                {
                    NoHayResultadosTxt = true;
                }
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error Busqueda", e.Message, "Aceptar");
            }
        }
        private async void BuscarMethod()
        {
            if (string.IsNullOrEmpty(BusquedaTxt))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar algo que buscar.", "Aceptar");
            }
            else
            {
                RefreshMethod();
            }
        }
        private async void BusquedaSelectMethod()
        {
            if (busquedaSelection != null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new DetalleUsuarioPage((UserModel)BusquedaSelection));
            }
            BusquedaSelection = null;
        }
        #endregion
        #region Constructor
        public SearchAmigoViewModel()
        {
            searchCollection = new ObservableCollection<UserModel>();
            RefreshMethod();
            itemTreshold = 1;
        }
        #endregion
    }
}
