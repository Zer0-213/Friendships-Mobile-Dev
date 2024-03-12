using Firebase.Auth;
using Firebase.Auth.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendships
{
    class Firebase
    {
        FirebaseAuthConfig config;
        string databaseURL;
        public string DatabaseURL
        {

            get { return databaseURL; }

            set { databaseURL = value; }
        }


        public Firebase()
        {
            this.config = new FirebaseAuthConfig();
            config.ApiKey = "AIzaSyA9DRAff1OIFUX8yTsbi3fPwCTQApxskdU";
            config.AuthDomain = "friendships-648c5.firebaseapp.com";
            databaseURL = "https://friendships-648c5-default-rtdb.europe-west1.firebasedatabase.app/";

        }

        public Firebase(FirebaseAuthProvider[] provider)
        {
            this.config = new FirebaseAuthConfig();
            config.ApiKey = "AIzaSyA9DRAff1OIFUX8yTsbi3fPwCTQApxskdU";
            config.AuthDomain = "friendships-648c5.firebaseapp.com";
            config.Providers = provider;
            databaseURL = "https://friendships-648c5-default-rtdb.europe-west1.firebasedatabase.app/";

        }
        
        public FirebaseAuthConfig Config
        {
            get { return config; }
            set { config = value; }
        }
    }
}
