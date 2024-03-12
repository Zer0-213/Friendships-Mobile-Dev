using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Friendships.ViewModels
{
    public partial class ChooseProfilePhotoViewModel:ObservableObject
    {


        [RelayCommand]
        private static async Task CancelButtonClicked()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}

