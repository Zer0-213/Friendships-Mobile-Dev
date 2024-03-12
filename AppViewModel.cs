using CommunityToolkit.Mvvm.ComponentModel;
using Friendships.Models;


namespace Friendships
{
    public partial class AppShellViewModel:ObservableObject
    {
        [ObservableProperty]
        ProfileModel profileModel;

    }
}
