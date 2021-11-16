using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using ProyectoFinalV1.Models;
using ProyectoFinalV1.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProyectoFinalV1.Services
{
    public class FirebaseProvider
    {
        #region Selects
        public async Task<string> getImageUrlFromUser(string username)
        {
            string imageUrl;
            try
            {
                imageUrl = await firebaseStorage
                .Child("perfiles")
                .Child(username + ".jpg")
                .GetDownloadUrlAsync();
            }
            catch
            {
                imageUrl = "none";
            }
            return imageUrl;
        }
        public async Task<List<FriendModel>> getAmigosFriendModelOnceAsync(string username)
        {
            List<FriendModel> amigos = new List<FriendModel>();
            var request = await firebase
                .Child("Users")
                .Child(username)
                .Child("Amigos")
                .OnceAsync<FriendModel>();
            foreach (var x in request)
            {
                amigos.Add(x.Object);
            }
            return amigos;
        }
        public async Task<List<UserModel>> getAmigosDeUser(string username)
        {
            List<UserModel> amigos = new List<UserModel>();
            var request = await firebase
                .Child("Users")
                .Child(username)
                .Child("Amigos")
                .OnceAsync<FriendModel>();
            foreach(var x in request)
            {
                UserModel amigo = await getUserByUsername(x.Object.username);
                amigos.Add(amigo);
            }
            return amigos;
        }
        public async Task<List<OpinionModel>> getOpinionesOfMovie(int movieID)
        {
            List<OpinionModel> opiniones = new List<OpinionModel>();
            var request = await firebase
                .Child("Opinions")
                .Child(movieID.ToString())
                .OnceAsync<OpinionModel>();
            
            foreach(var op in request)
            {
                opiniones.Add(new OpinionModel() { 
                    fechaOpinion=op.Object.fechaOpinion,
                    id=op.Key,
                    movieID=op.Object.movieID,
                    opinionTXT=op.Object.opinionTXT,
                    username=op.Object.username,
                });
            }
            return opiniones;
        }
        public async Task<UserModel> getUserByUsername(string username)
        {
            UserModel buscado = new UserModel();
            buscado.username = null;
            var usuarios = await firebase.Child("Users").OnceAsync<UserModel>();
            foreach (var user in usuarios)
            {
                if (user.Object.username.Equals(username))
                {
                    buscado = user.Object;
                    break;
                }
            }
            return buscado;
        }
        public async Task<UserModel> getUserByEmail(string email)
        {
            UserModel buscado = new UserModel();
            buscado.email = "none";
            var usuarios = await firebase
                .Child("Users")
                .OnceAsync<UserModel>();
            foreach(var user in usuarios)
            {
                if (user.Object.email.Equals(email))
                {
                    buscado = user.Object;
                    break;
                }
            }
            return buscado;
        }
        public async Task<List<UserModel>> getAllUsersOnceAsync()
        {
            List<UserModel> usuarios = new List<UserModel>();
            var test = await firebase
                .Child("Users")
                .OnceAsync<UserModel>();
            foreach (var user in test)
            {
                usuarios.Add(user.Object);
            }
            return usuarios;
        }
        public async Task<List<string>> getAllUsernames()
        {
            //data inicial
            List<UserModel> usuarios = await getAllUsersOnceAsync();
            //suscripcion
            var users = firebase.Child("Users")
                .AsObservable<UserModel>()
                .Subscribe(eventReceived => {
                    // check to see what type of FirebaseEvent was detected by the subscription
                    if (eventReceived.EventType == Firebase.Database.Streaming.FirebaseEventType.Delete)
                        // record deleted - remove from ObservableCollection
                        usuarios.Remove(eventReceived.Object);

                    // record was added or updated
                    if (eventReceived.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                    {
                        // see if the inserted/updated object is already in our ObservableCollection
                        if (usuarios.Contains(eventReceived.Object))
                        {
                            int tempIndex = usuarios.IndexOf(eventReceived.Object);
                            usuarios[tempIndex] = eventReceived.Object;
                        }
                        else
                        {
                            usuarios.Add(eventReceived.Object);
                        }
                    }
                });
            List<string> usernames = new List<string>();
            foreach(UserModel user in usuarios)
            {
                usernames.Add(user.username);
            }

            return usernames;
        }
        public async Task<List<FriendModel>> getAmigosFriendModelAsObservable(string username)
        {
            //data inicial
            List<FriendModel> amigos = await getAmigosFriendModelOnceAsync(username);
            var request = firebase
                .Child("Users")
                .Child(username)
                .Child("Amigos")
                .AsObservable<FriendModel>()
                .Subscribe(eventReceived => {
                    // check to see what type of FirebaseEvent was detected by the subscription
                    if (eventReceived.EventType == Firebase.Database.Streaming.FirebaseEventType.Delete)
                        // record deleted - remove from ObservableCollection
                        amigos.Remove(eventReceived.Object);

                    // record was added or updated
                    if (eventReceived.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                    {
                        // see if the inserted/updated object is already in our ObservableCollection
                        if (amigos.Contains(eventReceived.Object))
                        {
                            int tempIndex = amigos.IndexOf(eventReceived.Object);
                            amigos[tempIndex] = eventReceived.Object;
                        }
                        else
                        {
                            amigos.Add(eventReceived.Object);
                        }
                    }
                });
            return amigos;
        }
        #endregion

        #region Inserts
        public async Task<string> AddProfilePicToUser(string username, Stream imagen)
        {
            var storageImage = await firebaseStorage
                .Child("perfiles")
                .Child(username+".jpg")
                .PutAsync(imagen);
            string imgurl = storageImage;
            return imgurl;
        }
        public async Task AddUser(UserModel user)
        {
            await firebase
                .Child("Users")
                .Child(user.username)
                .PutAsync(user);
        }
        public async Task AddClass(ClassModel clase)
        {
            await firebase
                .Child("Class")
                .PostAsync(clase);
        }
        public async Task<string> AddOpinion(OpinionModel opinion,UserModel user)
        {
            var opinionMandada = await firebase
                .Child("Opinions")
                .Child(opinion.movieID.ToString())
                .PostAsync(opinion);
            InsertOpinionUser inserOp = new InsertOpinionUser();
            inserOp.movieID = opinion.movieID;
            await firebase
                .Child("Users")
                .Child(user.username)
                .Child("Opiniones")
                .Child(opinionMandada.Key)
                .PutAsync(opinion.movieID);
            return opinionMandada.Key;
        }
        public async Task AddFriend(UserModel user,UserModel friend)
        {
            FriendModel invitacion = new FriendModel();
            invitacion.aceptado = false;
            invitacion.sender = user.username;
            invitacion.username = friend.username;
            await firebase.Child("Users")
                .Child(user.username)
                .Child("Amigos")
                .Child(friend.username)
                .PatchAsync(invitacion);
            invitacion.username = user.username;
            await firebase.Child("Users")
                .Child(friend.username)
                .Child("Amigos")
                .Child(user.username)
                .PatchAsync(invitacion);
        }
        public async Task AddInvitacionAceptada(UserModel user, UserModel friend,FriendModel invitacion)
        {
            invitacion.aceptado = true;

            invitacion.username = friend.username;
            await firebase.Child("Users")
                .Child(user.username)
                .Child("Amigos")
                .Child(friend.username)
                .PatchAsync(invitacion);

            invitacion.username = user.username;
            await firebase.Child("Users")
                .Child(friend.username)
                .Child("Amigos")
                .Child(user.username)
                .PatchAsync(invitacion);
        }
        #endregion

        #region Updates
        public async Task UpdateDatosUser(UserModel user)
        {
            UpdateDatosUser update = new UpdateDatosUser() {
                apellido = user.apellido,
                nombre = user.nombre,
                edad = user.edad,
            };
            await firebase.Child("Users")
                .Child(user.username)
                .PatchAsync(update);
        }
        #endregion

        #region Deletes
        public async Task RemoveFriend(UserModel user,UserModel friend)
        {
            await firebase.Child("Users")
                .Child(user.username)
                .Child("Amigos")
                .Child(friend.username)
                .DeleteAsync();
            await firebase.Child("Users")
                .Child(friend.username)
                .Child("Amigos")
                .Child(user.username)
                .DeleteAsync();
        }
        public async Task RemoveUser(string username)
        {
            await firebase.Child("Users")
                .Child(username)
                .DeleteAsync();
        }
        #endregion

        static FirebaseClient firebase;
        static FirebaseStorage firebaseStorage;
        public FirebaseProvider()
        {
            firebase = new FirebaseClient("https://proyecto-final-lenguajes-4-default-rtdb.firebaseio.com/");
            firebaseStorage = new FirebaseStorage("proyecto-final-lenguajes-4.appspot.com");
        }
    }
}
