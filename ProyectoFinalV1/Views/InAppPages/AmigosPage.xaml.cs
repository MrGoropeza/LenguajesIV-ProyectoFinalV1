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
    public partial class AmigosPage : ContentPage
    {
        AmigosViewModel contexto;
        public AmigosPage()
        {
            InitializeComponent();
            contexto = new AmigosViewModel();
            BindingContext = contexto;
        }

        private async void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchAmigoPage(contexto.BusquedaTxt));
        }
    }
}