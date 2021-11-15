﻿using ProyectoFinalV1.ViewModels.InAppViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalV1.Views.InAppPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchMovie : ContentPage
    {
        SearchMovieViewModel contexto;
        public SearchMovie(string busqueda)
        {
            InitializeComponent();
            contexto = new SearchMovieViewModel();
            contexto.BusquedaTxt = busqueda;
            BindingContext = contexto;
        }

        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            
        }
    }
}