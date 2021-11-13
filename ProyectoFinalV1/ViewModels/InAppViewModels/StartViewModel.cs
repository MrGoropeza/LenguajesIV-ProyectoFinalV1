using GalaSoft.MvvmLight.Command;
using ProyectoFinalV1.Models;
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
        public ObservableCollection<CardMovieModel> enCinesCollection;
        public ObservableCollection<CardMovieModel> pelisPopularesCollection;

        private int paginaPelisPopulares;
        public int itemTreshold;
        private bool IsBusy;



        public bool isRefreshing;
        public bool isRunning;
        public bool isVisible;
        public bool isEnabled;
        #endregion

        #region Propiedades
        public ObservableCollection<CardMovieModel> EnCinesItems
        {
            get { return this.enCinesCollection; }
            set { SetValue(ref this.enCinesCollection, value); }
        }
        public ObservableCollection<CardMovieModel> PelisPopularesItems
        {
            get { return this.pelisPopularesCollection; }
            set { SetValue(ref this.pelisPopularesCollection, value); }
        }
        public int PaginaPelisPopulares
        {
            get { return this.paginaPelisPopulares; }
            set { SetValue(ref this.paginaPelisPopulares, value); }
        }
        public int ItemTreshold
        {
            get { return this.itemTreshold; }
            set { SetValue(ref this.itemTreshold, value); }
        }
        public bool IsRefreshingTxt
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
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

        
        #endregion

        #region Metodos
        private async void RefreshMethod()
        {
            IsRefreshingTxt = true;
            await LlenarEnCines();
            PaginaPelisPopulares = 1;
            PelisPopularesItems.Clear();
            await LlenarPelisPopulares();
            IsRefreshingTxt = false;
        }
        private async void ActualizarPelisPopularesMethod()
        {
            List<PeliculaModel> peliculasRequest = await App.tmdbProvider.getPopulares(PaginaPelisPopulares);
            
            foreach (PeliculaModel peli in peliculasRequest)
            {
                CardMovieModel card = new CardMovieModel()
                {
                    imageUrl = App.tmdbProvider.getUrlFromPath(peli.posterPath, 500),
                    title = peli.title
                };
                if (!PelisPopularesItems.Contains(card))
                {
                    PelisPopularesItems.Add(card);
                }
            }
            PaginaPelisPopulares++;
        }
        private async Task LlenarPelisPopulares()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            List<PeliculaModel> peliculasRequest = await App.tmdbProvider.getPopulares(PaginaPelisPopulares);
            foreach (PeliculaModel peli in peliculasRequest)
            {
                PelisPopularesItems.Add(new CardMovieModel()
                {
                    imageUrl = App.tmdbProvider.getUrlFromPath(peli.posterPath, 500),
                    title = peli.title
                });
            }
            PaginaPelisPopulares++;
            IsBusy = false;
        }
        private async Task LlenarEnCines()
        {
            EnCinesItems.Clear();
            List<PeliculaModel> peliculasRequest = await App.tmdbProvider.getEnCines();
            foreach (PeliculaModel peli in peliculasRequest)
            {
                EnCinesItems.Add(new CardMovieModel()
                {
                    imageUrl = App.tmdbProvider.getUrlFromPath(peli.posterPath,500),
                    title = peli.title
                }) ;
            }
        }
        #endregion

        #region Constructor
        public StartViewModel()
        {
            EnCinesItems = new ObservableCollection<CardMovieModel>();
            pelisPopularesCollection = new ObservableCollection<CardMovieModel>();
            RefreshMethod();
            ItemTreshold = 1;
        }
        #endregion
    }
}
