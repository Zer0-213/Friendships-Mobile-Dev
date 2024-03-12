using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;


namespace Friendships.ViewModels
{
    [QueryProperty(nameof(PassedImage),"image")]
  public partial class ProfilePhotoEditViewModel : ObservableObject
    {


        [ObservableProperty]
        Image passedImage;

        [ObservableProperty]
        ImageSource source;

        public ProfilePhotoEditViewModel()
        {
            Source = ImageSource.FromFile("");
        }
        


        [RelayCommand]
        private static async Task CancelButtonClicked()
        {
            await Shell.Current.GoToAsync("..");
        }

        }
}

