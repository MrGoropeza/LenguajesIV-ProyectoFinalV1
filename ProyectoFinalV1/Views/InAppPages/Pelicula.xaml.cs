using ProyectoFinalV1.Models;
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
        public Pelicula(PeliculaModel pelicula)
        {
            InitializeComponent();
            BindingContext = new PeliViewModel(pelicula);
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}