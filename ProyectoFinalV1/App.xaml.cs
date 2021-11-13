using Firebase.Auth;
using Firebase.Database;
using ProyectoFinalV1.Services;
using ProyectoFinalV1.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinalV1
{
    public partial class App : Application
    {
        public static FirebaseProvider firebaseBDD;
        public static FirebaseAuthProvider firebaseAuth;
        public static PeliculasProvider tmdbProvider;
        private string WebAPIkey = "AIzaSyB_W2TRS2rCXcjfY3UAswlKKP_t_I5IKY0";
        public static FirebaseAuthLink autenticacion;
        public App()
        {
            InitializeComponent();
            firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            firebaseBDD = new FirebaseProvider();
            tmdbProvider = new PeliculasProvider();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
