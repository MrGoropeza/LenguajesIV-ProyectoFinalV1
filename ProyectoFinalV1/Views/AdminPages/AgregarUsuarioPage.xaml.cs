using ProyectoFinalV1.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalV1.Views.AdminPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarUsuarioPage : ContentPage
    {
        public AgregarUsuarioPage()
        {
            InitializeComponent();
            BindingContext = new AgregarUsuario();
        }
    }
}