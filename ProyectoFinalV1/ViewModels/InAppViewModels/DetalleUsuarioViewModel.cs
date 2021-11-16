using GalaSoft.MvvmLight.Command;
using ProyectoFinalV1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoFinalV1.ViewModels.InAppViewModels
{
    public class DetalleUsuarioViewModel : BaseViewModel
    {
        #region atributos
        private UserModel actual;
        private FriendModel invitacion;

        private string imageUrl;
        private string nombre;
        private string username;
        private string edad;
        private string situacion;
        private string addFriend;
        private string removeFriend;

        private bool isRefreshing;
        private bool addEnabled;
        private bool removeEnabled;
        #endregion
        #region propiedades
        public string ImageUrl
        {
            get { return this.imageUrl; }
            set { SetValue(ref this.imageUrl, value); }
        }
        public string NomTxt
        {
            get { return this.nombre; }
            set { SetValue(ref this.nombre, value); }
        }
        public string UsernameTxt
        {
            get { return this.username; }
            set { SetValue(ref this.username, value); }
        }
        public string EdadTxt
        {
            get { return this.edad; }
            set { SetValue(ref this.edad, value); }
        }
        public string SituacionTxt
        {
            get { return this.situacion; }
            set { SetValue(ref this.situacion, value); }
        }
        public string AddFriendTxt
        {
            get { return this.addFriend; }
            set { SetValue(ref this.addFriend, value); }
        }
        public string RemoveFriendTxt
        {
            get { return this.removeFriend; }
            set { SetValue(ref this.removeFriend, value); }
        }
        public bool IsRefreshingTxt
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public bool AddEnabledTxt
        {
            get { return this.addEnabled; }
            set { SetValue(ref this.addEnabled, value); }
        }
        public bool RemoveEnabledTxt
        {
            get { return this.removeEnabled; }
            set { SetValue(ref this.removeEnabled, value); }
        }
        #endregion
        #region comandos
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(RefreshMethod);
            }
        }
        public ICommand AddFriendCommand
        {
            get
            {
                return new RelayCommand(AddFriendMethod);
            }
        }
        public ICommand RemoveFriendCommand
        {
            get
            {
                return new RelayCommand(RemoveFriendMethod);
            }
        }
        #endregion
        #region metodos
        private async void RefreshMethod()
        {
            IsRefreshingTxt = true;
            await analizarSituacionConUsuario();
            actual = await App.firebaseBDD.getUserByUsername(actual.username);
            await Task.Delay(1000);
            IsRefreshingTxt = false;
        }
        private async void AddFriendMethod()
        {
            try
            {
                if (SituacionTxt.Equals("No es tu amigo"))
                {
                    await App.firebaseBDD.AddFriend(App.usuarioLogeado, actual);
                }else if(SituacionTxt.Equals("¿Aceptar solicitud de amistad?"))
                {
                    await App.firebaseBDD.AddInvitacionAceptada(App.usuarioLogeado, actual,invitacion);
                }
                
                RefreshMethod();
            }
            catch(Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error",e.Message, "Aceptar");
            }
            //await App.Current.MainPage.DisplayAlert("Error","aun no implementado","Aceptar");
        }
        private async void RemoveFriendMethod()
        {
            try
            {
                await App.firebaseBDD.RemoveFriend(App.usuarioLogeado,actual);
                RefreshMethod();
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "Aceptar");
            }
        }
        private async Task analizarSituacionConUsuario()
        {
            try
            {
                if (actual.username.Equals(App.usuarioLogeado.username))
                {
                    SituacionTxt = "Este es tu perfil";
                    AddFriendTxt = "Añadir amigo";
                    RemoveFriendTxt = "Eliminar amigo";
                    RemoveEnabledTxt = false;
                    AddEnabledTxt = false;
                }
                else
                {
                    var request = await App.firebaseBDD.getAmigosFriendModelAsObservable(App.usuarioLogeado.username);
                    bool exists = false;
                    foreach (var user in request)
                    {
                        if (user.username.Equals(actual.username))
                        {
                            exists = true;
                            this.invitacion = user;
                            if (user.aceptado)
                            {
                                SituacionTxt = "Es tu amigo";
                                AddFriendTxt = "Añadir amigo";
                                RemoveFriendTxt = "Eliminar amigo";
                                AddEnabledTxt = false;
                                RemoveEnabledTxt = true;
                            }
                            else
                            {
                                if (user.sender.Equals(App.usuarioLogeado.username))
                                {
                                    SituacionTxt = "Aún no aceptó tu solicitud";
                                    AddFriendTxt = "Enviar Solicitud";
                                    RemoveFriendTxt = "Cancelar Solicitud";
                                    AddEnabledTxt = false;
                                    RemoveEnabledTxt = true;
                                }
                                else
                                {
                                    SituacionTxt = "¿Aceptar solicitud de amistad?";
                                    AddFriendTxt = "Aceptar Solicitud";
                                    RemoveFriendTxt = "Rechazar Solicitud";
                                    AddEnabledTxt = true;
                                    RemoveEnabledTxt = true;
                                }
                            }
                        } 
                    }
                    if (!exists)
                    {
                        SituacionTxt = "No es tu amigo";
                        AddFriendTxt = "Añadir amigo";
                        RemoveFriendTxt = "Eliminar amigo";
                        AddEnabledTxt = true;
                        RemoveEnabledTxt = false;
                    }
                }
            }
            catch(Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error",e.Message,"Aceptar");
            }
        }
        #endregion
        #region constructor
        public DetalleUsuarioViewModel(UserModel actualUser)
        {
            actual = actualUser;
            ImageUrl = actualUser.imageUrl;
            UsernameTxt = actualUser.username;
            NomTxt = actualUser.nomYapel;
            EdadTxt = "Edad: " + actualUser.edad;
            RefreshMethod();
        }
        #endregion
    }
}
