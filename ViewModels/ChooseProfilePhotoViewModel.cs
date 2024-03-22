using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CropperImage.MAUI;
using Friendships.Models;

namespace Friendships.ViewModels
{
    [QueryProperty(nameof(Profile), "profile")]
    public partial class ChooseProfilePhotoViewModel : ObservableObject
    {

        [ObservableProperty]
        CropperImageView cropper;
        [ObservableProperty]
        ProfileModel profile;

        public ChooseProfilePhotoViewModel(CropperImageView cropper)
        {

            this.cropper = cropper;


        }


        [RelayCommand]
        private static async Task CancelButtonClicked()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task ConfirmButtonClicked()
        {
            try
            {

                await Cropper.CropImageAsync(true);

                MemoryStream ms = new();
                ms.Write(Cropper.CroppedImageBytes);
                ms.Position = 0;

                Profile.ProfilePicture = new Image() { Source = ImageSource.FromStream(() => ms) };

                Profile.ImageStreamToBase64(ms);

                ms.Close();


                Firebase firebase = new();

                await firebase.CreateProfile(Profile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Error saving picture", "Close");

            }
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private static async Task SwipeBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

