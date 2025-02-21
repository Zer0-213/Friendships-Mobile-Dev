﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Firebase.Database.Query;
using Friendships.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Friendships.ViewModels
{
    public partial class SignUpViewModel: ObservableObject
    {

        [ObservableProperty]
        string fullName;
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string password;
        [ObservableProperty]
        bool isLoading;

        [RelayCommand]
        private async Task SignUpButtonPressed()
        {

            if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Invalid Details", "Please fill out all forms", "Ok");
                return;
            }

            try
            {

                Firebase config = new(new EmailProvider[] { new() });

                var authClient = new FirebaseAuthClient(config.Config);



                var user = await authClient.CreateUserWithEmailAndPasswordAsync(Email, Password, FullName);

                await config.CreateProfile(new ProfileModel()
                {
                    Name = FullName,
                    Username = Email.Split("@")[0],
                    UserUid = user.User.Uid
                });


                await Application.Current.MainPage.DisplayAlert("Alert", "User registered successfully", "Ok");

                await Shell.Current.GoToAsync("..");


            }
            catch (FirebaseAuthException ex)
            {
                await Shell.Current.DisplayAlert("Invalid Details", ex.Reason.ToString(), "Ok");
            }
            catch (FirebaseException ex)
            {
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Sign Up Error", "Something went wrong adding profile to database",
                    "Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Sign Up Error", "Internal error", "Ok");
            }
            finally
            {
                FullName = "";
                Email = "";
                Password = "";

            }


        }

        [RelayCommand]
        private static async Task SwipeBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
