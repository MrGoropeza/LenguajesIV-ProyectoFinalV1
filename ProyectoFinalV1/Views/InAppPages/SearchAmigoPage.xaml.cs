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
    public partial class SearchAmigoPage : ContentPage
    {
        SearchAmigoViewModel contexto;
        public SearchAmigoPage(string busqueda)
        {
            InitializeComponent();
            contexto = new SearchAmigoViewModel();
            contexto.BusquedaTxt = busqueda;
            BindingContext = contexto;
        }
    }
}