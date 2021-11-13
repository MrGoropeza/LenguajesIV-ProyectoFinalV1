using ProyectoFinalV1.Services;
using ProyectoFinalV1.Views.AdminPages;
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
    public partial class AdminPage : ContentPage
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private async void InsertarUsuario_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarUsuarioPage());
        }

        private async void VerUsers_Clicked(object sender, EventArgs e)
        {
            FirebaseProvider firebaseBDD = new FirebaseProvider();
            List<string> usernames = await firebaseBDD.getAllUsernames();
            labelUsers.Text = "";
            foreach(string user in usernames)
            {
                Console.WriteLine(user);
                labelUsers.Text +=user+"\n";
            }
            labelUsers.IsVisible = true;
        }
    }
}