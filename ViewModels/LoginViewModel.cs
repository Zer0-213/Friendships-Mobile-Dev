using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Database;
using Firebase.Database.Query;
using Friendships.Models;
using Friendships.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendships.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        string username;
        [ObservableProperty]
        string password;
        [ObservableProperty]
        bool isLoading;
        [ObservableProperty]
        ProfileModel profile;

        [RelayCommand]
        private async Task SignInPressed()
        {
            try
            {
                IsLoading = true;
                var firebase = new Firebase(new EmailProvider[] { new() });


                var authClient = new FirebaseAuthClient(firebase.Config);

                var user = await authClient.SignInWithEmailAndPasswordAsync(Username, Password);

                Profile = await firebase.GetProfile(user.User.Uid);

                await firebase.RetrieveFriendsList(Profile);

                Profile.ConvertBase64();

                SharedProfile.Profile = Profile;

                await Shell.Current.GoToAsync("///MainTab");


            }
            catch (FirebaseAuthException ex)
            {
                Console.WriteLine(ex.Message);
                await App.Current.MainPage.DisplayAlert("Error", "Invalid Username or Password", "Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        async Task SignUpPressed() => await Shell.Current.GoToAsync(nameof(SignUpView));

    }
}
