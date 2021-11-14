using ProyectoFinalV1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel();
        }
        private async void Login_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}