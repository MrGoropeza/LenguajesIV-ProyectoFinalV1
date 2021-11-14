using ProyectoFinalV1.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalV1.ViewModels.InAppViewModels
{
    public class PeliViewModel : BaseViewModel
    {
        #region Atributos
        public PeliculaModel peliculaSeleccionada;
        public string title;
        public string backgroundUrl;
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
        #endregion
        #region Comandos

        #endregion
        #region Metodos

        #endregion
        #region Constructor
        public PeliViewModel(PeliculaModel pelicula)
        {
            this.peliculaSeleccionada = pelicula;
            this.TitleTxt = pelicula.title;
            this.BackgroundUrl = pelicula.backgroundUrl;
            Console.WriteLine(BackgroundUrl);
        }
        #endregion
    }
}
