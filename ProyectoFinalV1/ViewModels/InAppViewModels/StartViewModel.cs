using GalaSoft.MvvmLight.Command;
using ProyectoFinalV1.Models;
using ProyectoFinalV1.Views;
using ProyectoFinalV1.Views.InAppPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoFinalV1.ViewModels.InAppViewModels
{
    public class StartViewModel : BaseViewModel
    {
        #region Atributos
        public ObservableCollection<PeliculaModel> enCinesCollection;
        public object enCinesSelection;

        public ObservableCollection<PeliculaModel> pelisPopularesCollection;
        private int paginaPelisPopulares;
        public object pelisPopularesSelection;

        public ObservableCollection<PeliculaModel> topRatedCollection;
        private int paginaTopRated;
        public object topRatedSelection;

        public int itemTreshold;
        public bool isRefreshing;
        public bool isRunning;
        public bool isVisible;
        public bool isEnabled;
        #endregion

        #region Propiedades
        public ObservableCollection<PeliculaModel> EnCinesItems
        {
            get { return this.enCinesCollection; }
            set { SetValue(ref this.enCinesCollection, value); }
        }
        public ObservableCollection<PeliculaModel> PelisPopularesItems
        {
            get { return this.pelisPopularesCollection; }
            set { SetValue(ref this.pelisPopularesCollection, value); }
        }
        public ObservableCollection<PeliculaModel> TopRatedItems
        {
            get { return this.topRatedCollection; }
            set { SetValue(ref this.topRatedCollection, value); }
        }
        public object EnCinesSelection
        {
            get { return this.enCinesSelection; }
            set { SetValue(ref this.enCinesSelection, value); }
        }
        public object PelisPopularesSelection
        {
            get { return this.pelisPopularesSelection; }
            set { SetValue(ref this.pelisPopularesSelection, value); }
        }
        public object TopRatedSelection
        {
            get { return this.topRatedSelection; }
            set { SetValue(ref this.topRatedSelection, value); }
        }
        public int ItemTreshold
        {
            get { return this.itemTreshold; }
            set { SetValue(ref this.itemTreshold, value); }
        }
        public bool IsRefreshingTxt
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
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
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(RefreshMethod);
            }
        }
        public ICommand ActualizarPelisPopularesCommand
        {
            get
            {
                return new RelayCommand(ActualizarPelisPopularesMethod);
            }
        }
        public ICommand ActualizarTopRatedCommand
        {
            get
            {
                return new RelayCommand(ActualizarTopRatedMethod);
            }
        }
        public ICommand EnCinesSelectCommand
        {
            get
            {
                return new RelayCommand(EnCinesSelectMethod);
            }
        }
        public ICommand PelisPopularesSelectCommand
        {
            get
            {
                return new RelayCommand(PelisPopularesSelectMethod);
            }
        }
        public ICommand TopRatedSelectCommand
        {
            get
            {
                return new RelayCommand(TopRatedSelectMethod);
            }
        }


        #endregion

        #region Metodos
        private async void EnCinesSelectMethod()
        {
            await selectInCollection("enCines");
        }
        private async void PelisPopularesSelectMethod()
        {
            await selectInCollection("pelisPopulares");
        }
        private async void TopRatedSelectMethod()
        {
            await selectInCollection ("topRated");
        }
        private async Task selectInCollection(string nombreColeccion)
        {
            switch (nombreColeccion)
            {
                case "enCines":
                    if (enCinesSelection != null)
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new Pelicula((PeliculaModel)enCinesSelection));
                    }
                    EnCinesSelection = null;
                    break;
                case "pelisPopulares":
                    if (pelisPopularesSelection != null)
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new Pelicula((PeliculaModel)pelisPopularesSelection));
                    }
                    PelisPopularesSelection = null;
                    break;
                case "topRated":
                    if (topRatedSelection != null)
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new Pelicula((PeliculaModel)topRatedSelection));
                    }
                    TopRatedSelection = null;
                    break;
            }
        }
        private async void RefreshMethod()
        {
            IsRefreshingTxt = true;

            EnCinesItems.Clear();
            await LlenarColeccion("enCines");

            PelisPopularesItems.Clear();
            paginaPelisPopulares = 1;
            await LlenarColeccion("pelisPopulares");

            TopRatedItems.Clear();
            paginaTopRated = 1;
            await LlenarColeccion("topRated");

            IsRefreshingTxt = false;
        }
        private async void ActualizarPelisPopularesMethod()
        {
            await ActualizarColeccion("pelisPopulares");
        }
        private async void ActualizarTopRatedMethod()
        {
            await ActualizarColeccion("topRated");
        }
        private async Task ActualizarColeccion(string nombreColeccion)
        {
            List<PeliculaModel> peliculasRequest = new List<PeliculaModel>();
            switch (nombreColeccion)
            {
                case "pelisPopulares":
                    peliculasRequest = await App.tmdbProvider.getPopulares(paginaPelisPopulares,500,500);
                    foreach (PeliculaModel peli in peliculasRequest)
                    {
                        bool exists = false;
                        foreach (PeliculaModel p in PelisPopularesItems)
                        {
                            if (p.movieID == peli.movieID)
                            {
                                exists = true;
                            }
                        }
                        if (!exists)
                        {
                            PelisPopularesItems.Add(peli);
                        }

                    }
                    paginaPelisPopulares++;
                    break;
                case "topRated":
                    peliculasRequest = await App.tmdbProvider.getTopRated(paginaTopRated,500,500);
                    foreach (PeliculaModel peli in peliculasRequest)
                    {
                        bool exists = false;
                        foreach (PeliculaModel p in TopRatedItems)
                        {
                            if (p.movieID == peli.movieID)
                            {
                                exists = true;
                            }
                        }
                        if (!exists)
                        {
                            TopRatedItems.Add(peli);
                        }
                    }
                    paginaTopRated++;
                    break;
            }
        }


        private async Task LlenarColeccion(string nombreColeccion)
        {
            List<PeliculaModel> peliculasRequest = new List<PeliculaModel>();
            switch (nombreColeccion)
            {
                case "enCines":
                    peliculasRequest = await App.tmdbProvider.getEnCines(500, 500);
                    foreach (PeliculaModel peli in peliculasRequest)
                    {
                        EnCinesItems.Add(peli);
                    }
                    break;
                case "pelisPopulares":
                    peliculasRequest = await App.tmdbProvider.getPopulares(paginaPelisPopulares,500,500);
                    foreach (PeliculaModel peli in peliculasRequest)
                    {
                        PelisPopularesItems.Add(peli);
                    }
                    paginaPelisPopulares++;
                    break;
                case "topRated":
                    peliculasRequest = await App.tmdbProvider.getTopRated(paginaTopRated,500,500);
                    foreach (PeliculaModel peli in peliculasRequest)
                    {
                        TopRatedItems.Add(peli);
                    }
                    paginaTopRated++;
                    break;
            }
        }
        #endregion

        #region Constructor
        public StartViewModel()
        {
            EnCinesItems = new ObservableCollection<PeliculaModel>();
            pelisPopularesCollection = new ObservableCollection<PeliculaModel>();
            topRatedCollection = new ObservableCollection<PeliculaModel>();
            RefreshMethod();
            ItemTreshold = 1;
        }
        #endregion
    }
}
