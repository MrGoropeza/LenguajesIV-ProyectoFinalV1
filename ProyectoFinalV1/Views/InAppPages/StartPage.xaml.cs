using ProyectoFinalV1.ViewModels.InAppViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalV1.Views.InAppPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        private StartViewModel contexto;
        public StartPage()
        {
            InitializeComponent();
            this.contexto = new StartViewModel();
            BindingContext = contexto;
        }

        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchMovie(contexto.BusquedaTxt));
        }
    }
}