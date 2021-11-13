using ProyectoFinalV1.ViewModels.StartViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalV1.Views.StartPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecuperarClave : ContentPage
    {
        public RecuperarClave()
        {
            InitializeComponent();
            BindingContext = new RecuperarClaveViewModel();
        }

        private async void Volver_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}