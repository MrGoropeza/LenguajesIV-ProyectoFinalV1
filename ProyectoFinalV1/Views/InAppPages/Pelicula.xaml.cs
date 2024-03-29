﻿using ProyectoFinalV1.Models;
using ProyectoFinalV1.ViewModels.InAppViewModels;
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
    public partial class Pelicula : ContentPage
    {
        PeliViewModel contexto;
        public Pelicula(PeliculaModel pelicula)
        {
            InitializeComponent();
            contexto = new PeliViewModel(pelicula);
            BindingContext = contexto;
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            contexto.EnviarOpinionMethod();
        }
    }
}