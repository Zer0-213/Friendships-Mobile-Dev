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
                ms.Position = 0;

                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "profile.png");

                FileStream fileStream = File.Create(fileName);

                await ms.CopyToAsync(fileStream);

                fileStream.Close();

                Profile.ProfilePicture = fileName;

                Firebase firebase = new Firebase();

                await firebase.CreateProfile(Profile,false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Error saving picture", "Close");

            }
            await Shell.Current.GoToAsync("..");
        }
    }
}

