using ProyectoFinalV1.Models;
using ProyectoFinalV1.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TMDbLib;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Prueba : ContentPage
    {
        public Prueba()
        {
            InitializeComponent();
            BindingContext = new PeliculasProvider();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}