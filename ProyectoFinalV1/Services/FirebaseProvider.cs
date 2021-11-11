using Firebase.Database;
using Firebase.Database.Query;
using ProyectoFinalV1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalV1.Services
{
    public class FirebaseProvider
    {
        #region Selects
        public async Task getUsers()
        {
            List<UserModel> usuarios = new List<UserModel>();
            
        }
        #endregion

        #region Inserts
        public async Task AddUser(UserModel user)
        {
            await firebase
                .Child("Users")
                .Child(user.UID)
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

        #endregion

        FirebaseClient firebase;
        public FirebaseProvider()
        {
            firebase = new FirebaseClient("https://proyecto-final-lenguajes-4-default-rtdb.firebaseio.com/");
        }
    }
}
