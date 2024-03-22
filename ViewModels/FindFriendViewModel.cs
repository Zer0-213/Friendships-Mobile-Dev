using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database;
using Firebase.Database.Query;
using Friendships.Models;


namespace Friendships.ViewModels
{
    [QueryProperty(nameof(Profile), "profile")]
    public partial class FindFriendViewModel : ObservableObject
    {

        [ObservableProperty]
        ProfileModel profile;
        [ObservableProperty]
        bool isUserNotFound;
        [ObservableProperty]
        bool isUserFound;
        [ObservableProperty]
        string textEntry;
        [ObservableProperty]
        ProfileModel userProfile;
        [ObservableProperty]
        Image userProfilePicture;
        [ObservableProperty]
        bool addButtonEnabled;

        [RelayCommand]
        async Task SearchButtonClickedCommand()
        {
            try
            {
                Firebase firebase = new Firebase();
                FirebaseClient client = new(firebase.DatabaseURL);

                var userUid = await client.Child("usernames").Child(TextEntry).OnceSingleAsync<String>();

                ProfileModel user = await client.Child("profiles").Child(userUid).OnceSingleAsync<ProfileModel>();

               await user.ConvertBase64();

                UserProfile = user;

                IsUserFound = true;

                for (int i = 0; i < Profile.Friends.Count; i++)
                {
                    if (Profile.Friends[i].UserUid == userUid)
                    {
                        AddButtonEnabled = false;
                        return;
                    }
                }

                AddButtonEnabled = true;

            }
            catch (FirebaseException ex)
            {
                Console.WriteLine(ex.Message);
                IsUserNotFound = true;
            }
        }

        [RelayCommand]
        async Task AddFriend()
        {
            try
            {
                FirebaseClient client = new(new Firebase().DatabaseURL);

                Profile.Friends.Add(UserProfile);

                await client.Child("friends").Child(Profile.UserUid).Child(UserProfile.UserUid).PutAsync<String>(UserProfile.UserUid);

                await client.Child("friends").Child(UserProfile.UserUid).Child(Profile.UserUid).PutAsync<String>(Profile.UserUid);

                AddButtonEnabled = false;
            }
            catch (FirebaseException ex)
            {
                await Shell.Current.DisplayAlert("Error adding friend", ex.Message, "Close");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error adding friend", ex.Message, "Close");
            }

        }

        [RelayCommand]
        async static Task SwipeBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
