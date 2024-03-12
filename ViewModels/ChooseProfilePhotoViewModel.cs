using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CropperImage.MAUI;

namespace Friendships.ViewModels
{
    public partial class ChooseProfilePhotoViewModel:ObservableObject
    {

        CropperImageView cropper;

        public ChooseProfilePhotoViewModel(CropperImageView cropper)
        {
            cropper.DefaultImageSource = "default_pfp.png";

            this.cropper = cropper;
        }


        [RelayCommand]
        private static async Task CancelButtonClicked()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}

