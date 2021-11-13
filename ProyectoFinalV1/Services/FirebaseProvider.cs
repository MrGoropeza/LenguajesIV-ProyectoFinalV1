using Firebase.Database;
using Firebase.Database.Query;
using ProyectoFinalV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProyectoFinalV1.Services
{
    public class FirebaseProvider
    {
        #region Selects
        public async Task<List<UserModel>> getAllUsersOnceAsync()
        {
            List<UserModel> usuarios = new List<UserModel>();
            var test = await firebase
                .Child("Users")
                .OnceAsync<UserModel>();
            foreach (var u in test)
            {
                usuarios.Add(u.Object);
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
        #endregion

        #region Inserts
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
        public async Task AddOpinion(OpinionModel opinion, UserModel user)
        {
            var op = await firebase
                .Child("Opinions")
                .PostAsync(opinion);
            await firebase.Child("Users")
                .Child(user.UID)
                .Child("Opinions")
                .PutAsync(opinion);
        }
        public async Task AddFriend(UserModel user,UserModel friend)
        {
            await firebase.Child("Users")
                .Child(user.UID)
                .Child("Friends")
                .PutAsync(new FriendModel().ID=friend.UID);
        }
        #endregion

        #region Updates

        #endregion

        #region Deletes
        public async Task RemoveUser(string username)
        {
            await firebase.Child("Users")
                .Child(username)
                .DeleteAsync();
        }
        #endregion

        static FirebaseClient firebase = new FirebaseClient("https://proyecto-final-lenguajes-4-default-rtdb.firebaseio.com/");
        public FirebaseProvider()
        {
            
        }
    }
}
