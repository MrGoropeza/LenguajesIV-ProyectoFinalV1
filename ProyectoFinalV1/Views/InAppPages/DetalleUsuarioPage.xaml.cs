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
    public partial class DetalleUsuarioPage : ContentPage
    {
        DetalleUsuarioViewModel contexto;
        public DetalleUsuarioPage(UserModel seleccionado)
        {
            InitializeComponent();
            contexto = new DetalleUsuarioViewModel(seleccionado);
            BindingContext = contexto;
        }

    }
}