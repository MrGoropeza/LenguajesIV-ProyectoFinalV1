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
    public class SearchMovieViewModel : BaseViewModel
    {
        #region atributos
        private string busqueda;

        private ObservableCollection<PeliculaModel> searchCollection;
        private int paginaBusqueda;
        private object busquedaSelection;

        private int itemTreshold;
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
        public ObservableCollection<PeliculaModel> SearchItems
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
        public ICommand ActualizarBusquedaCommand
        {
            get
            {
                return new RelayCommand(ActualizarBusquedaMethod);
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
            IsRefreshingTxt = true;
            IsVisibleTxt = true;
            searchCollection.Clear();
            paginaBusqueda = 1;
            await Task.Delay(50);
            await LlenarBusqueda();
            IsVisibleTxt = false;
            IsRefreshingTxt = false;
        }
        private async Task LlenarBusqueda()
        {
            try
            {
                var request = await App.tmdbProvider.getMovieFromSearch(BusquedaTxt, paginaBusqueda, 500, 500);
                if (request.Count() > 0)
                {
                    NoHayResultadosTxt = false;
                    foreach (var item in request)
                    {
                        SearchItems.Add(item);
                    }
                }
                else
                {
                    NoHayResultadosTxt = true;
                }
                
                paginaBusqueda++;
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
        private async void ActualizarBusquedaMethod()
        {
            var request = await App.tmdbProvider.getMovieFromSearch(BusquedaTxt, paginaBusqueda, 500, 500);
            foreach (PeliculaModel peli in request)
            {
                bool exists = false;
                foreach (PeliculaModel p in request)
                {
                    if (p.movieID == peli.movieID)
                    {
                        exists = true;
                    }
                }
                if (!exists)
                {
                    SearchItems.Add(peli);
                }

            }
            paginaBusqueda++;
        }
        private async void BusquedaSelectMethod()
        {
            if (busquedaSelection != null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new Pelicula((PeliculaModel)busquedaSelection));
            }
            BusquedaSelection = null;
        }
        #endregion
        #region Constructor
        public SearchMovieViewModel()
        {
            searchCollection = new ObservableCollection<PeliculaModel>();
            RefreshMethod();
            paginaBusqueda = 1;
            itemTreshold = 1;
        }
        #endregion
    }
}
