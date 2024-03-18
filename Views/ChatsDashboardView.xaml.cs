using Firebase.Database;
using Friendships.Models;
using Friendships.ViewModels;

namespace Friendships.Views;

public partial class ChatsDashboardView : ContentPage
{
    private ChatsDashboardViewModel _viewModel;
    public ChatsDashboardView()
    {
        InitializeComponent();

        _viewModel = new ChatsDashboardViewModel();

        BindingContext = _viewModel;

        Routing.RegisterRoute(nameof(FindFriendView), typeof(FindFriendView));
        Routing.RegisterRoute(nameof(ChatsView), typeof(ChatsView));
    }

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

        try
        {
            Console.WriteLine("Selected");

            ProfileModel user = e.SelectedItem as ProfileModel;

            Firebase firebase = new();

            var messages = firebase.RetrieveMessages(_viewModel.Profile, user);

            await Shell.Current.GoToAsync(nameof(ChatsView), new Dictionary<string, object>
            {
                ["messages"] = messages,
                ["user"] = user
            }) ;
        }
        catch(FirebaseException ex)
        {
            await Shell.Current.DisplayAlert("Error", "error retreiving messages", "Close");
        }

    }
}