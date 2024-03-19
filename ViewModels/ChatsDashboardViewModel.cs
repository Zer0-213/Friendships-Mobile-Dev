using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Friendships.Models;
using Friendships.Views;
using System.Collections.ObjectModel;


namespace Friendships.ViewModels
{
    public partial class ChatsDashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        ProfileModel profile;

        public ObservableCollection<ProfileModel> FriendsList { get; set; }
        public ChatsDashboardViewModel()
        {
            Profile = SharedProfile.Profile;

            FriendsList = new ObservableCollection<ProfileModel>();

            foreach (ProfileModel item in Profile.Friends)
            {
                FriendsList.Add(item);
            }
        }

        [RelayCommand]
        async Task GoToFindFriendsScreen()
        {
            await Shell.Current.GoToAsync(nameof(FindFriendView), new Dictionary<string, object>
            {
                ["profile"] = Profile,
            });
        }

        [RelayCommand]
        async Task ClickUser(ProfileModel user)
        {
            SharedProfile.ToUser = user;

            Firebase firebase = new();
            SharedProfile.Messages = await firebase.RetrieveMessages(Profile, user);


            await Shell.Current.GoToAsync(nameof(ChatsView));

        }
    }
}
