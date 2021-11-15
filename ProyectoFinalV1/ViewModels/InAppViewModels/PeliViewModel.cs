using ProyectoFinalV1.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Linq;

namespace ProyectoFinalV1.ViewModels.InAppViewModels
{
    public class PeliViewModel : BaseViewModel
    {
        #region Atributos
        public PeliculaModel peliculaSeleccionada;
        public string title;
        public string backgroundUrl;

        public string fechaEstreno;
        public string popularidad;
        public string mediaVotos;
        public string cantVotos;

        public string overview;

        public string entradaUsuario;
        public OpinionModel nuevaOpinion;
        public ObservableCollection<OpinionModel> opinionesCollection;


        public bool hayOpiniones;
        public bool isVisible;
        public bool isRefreshing;
        #endregion
        #region Propiedades
        public string TitleTxt
        {
            get { return this.title; }
            set { SetValue(ref this.title, value); }
        }
        public string BackgroundUrl
        {
            get { return this.backgroundUrl; }
            set { SetValue(ref this.backgroundUrl, value); }
        }
        public string FechaEstrenoTxt
        {
            get { return this.fechaEstreno; }
            set { SetValue(ref this.fechaEstreno, value); }
        }
        public string PopularidadTxt
        {
            get { return this.popularidad; }
            set { SetValue(ref this.popularidad, value); }
        }
        public string MediaVotosTxt
        {
            get { return this.mediaVotos; }
            set { SetValue(ref this.mediaVotos, value); }
        }
        public string CantVotosTxt
        {
            get { return this.cantVotos; }
            set { SetValue(ref this.cantVotos, value); }
        }
        public string OverViewTxt
        {
            get { return this.overview; }
            set { SetValue(ref this.overview, value); }
        }
        public string EntradaUsuarioTxt
        {
            get { return this.entradaUsuario; }
            set { SetValue(ref this.entradaUsuario, value); }
        }
        public ObservableCollection<OpinionModel> OpinionesItems
        {
            get { return this.opinionesCollection; }
            set { SetValue(ref this.opinionesCollection, value); }
        }
        public bool HayOpinionesTxt
        {
            get { return this.hayOpiniones; }
            set { SetValue(ref this.hayOpiniones, value); }
        }
        public bool IsVisibleTxt
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }
        public bool IsRefreshingTxt
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
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
        #endregion
        #region Metodos
        private async void RefreshMethod()
        {
            IsRefreshingTxt = true;
            OpinionesItems.Clear();
            IsVisibleTxt = true;
            HayOpinionesTxt = false;
            await LlenarOpiniones();
            if (OpinionesItems.Count()>0)
            {
                IsVisibleTxt = false;
                HayOpinionesTxt = true;
            }
            IsRefreshingTxt = false;
        }
        private async Task LlenarOpiniones()
        {
            try
            {
                var opiniones = await App.firebaseBDD.getOpinionesOfMovie(peliculaSeleccionada.movieID);
                foreach (var item in opiniones)
                {
                    item.FechaOpinionTxt = item.fechaOpinion.ToString("dd/MM/yyyy - HH:mm") + ":";
                    UserModel usuario = await App.firebaseBDD.getUserByUsername(item.username);
                    item.NombreUsuarioTxt = usuario.nombre + " " + usuario.apellido + ":";
                    bool exists = false;
                    foreach (var opinion in opinionesCollection)
                    {
                        if (opinion.id == item.id)
                        {
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        OpinionesItems.Add(item);
                    }
                }
            }
            catch(Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error",e.Message,"Aceptar");
            }
                
        }
        public async void EnviarOpinionMethod()
        {
            nuevaOpinion = new OpinionModel();
            nuevaOpinion.fechaOpinion = DateTime.Now;
            nuevaOpinion.username = App.usuarioLogeado.username;
            nuevaOpinion.movieID = peliculaSeleccionada.movieID;
            nuevaOpinion.opinionTXT = EntradaUsuarioTxt;
            try
            {
                string keyOpinion = await App.firebaseBDD.AddOpinion(nuevaOpinion,App.usuarioLogeado);
                nuevaOpinion.id = keyOpinion;
                await App.Current.MainPage.DisplayAlert("Listo","Opinion enviada correctamente.", "Aceptar");
                EntradaUsuarioTxt = "";
                RefreshMethod();
            }
            catch(Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "Aceptar");
            }
        }
        #endregion
        #region Constructor
        public PeliViewModel(PeliculaModel pelicula)
        {
            opinionesCollection = new ObservableCollection<OpinionModel>();
            this.peliculaSeleccionada = pelicula;
            this.TitleTxt = pelicula.title;
            this.BackgroundUrl = pelicula.backgroundUrl;
            this.FechaEstrenoTxt = "Fecha de Estreno: "+pelicula.releaseDate.ToString("dd-MM-yyyy");
            this.PopularidadTxt = "Popularidad: " + pelicula.popularity.ToString();
            this.mediaVotos = "Media de votos: " + pelicula.voteAverage.ToString();
            this.CantVotosTxt = "Cantidad de Votos: " + pelicula.voteCount.ToString();
            this.OverViewTxt = pelicula.overview;
            RefreshMethod();
        }
        #endregion
    }
}
