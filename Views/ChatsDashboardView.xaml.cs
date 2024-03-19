using Firebase.Database;
using Friendships.Models;
using Friendships.ViewModels;
using System.Text.Json;

namespace Friendships.Views;

public partial class ChatsDashboardView : ContentPage
{
    ProfileModel profile;
    public ChatsDashboardView(ChatsDashboardViewModel vm)
    {
        InitializeComponent();
        
        profile = SharedProfile.Profile;

        BindingContext = vm;

        Routing.RegisterRoute(nameof(FindFriendView), typeof(FindFriendView));
        Routing.RegisterRoute(nameof(ChatsView), typeof(ChatsView));

    }

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            if (e.SelectedItem != null)
            {
                ProfileModel toUser = e.SelectedItem as ProfileModel;
                SharedProfile.ToUser = toUser;

                Firebase firebase = new();
                SharedProfile.Messages = await firebase.RetrieveMessages(profile, toUser);


                await Shell.Current.GoToAsync(nameof(ChatsView));

            }
        }
        catch (FirebaseException ex)
        {
            await Shell.Current.DisplayAlert("Error", "error retrieving messages", "Close");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Close");
        }
    }
}