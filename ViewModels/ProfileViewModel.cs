using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Friendships.Models;
using Friendships.Views;
using Microsoft.Maui.Storage;

namespace Friendships.ViewModels
{
    partial class ProfileViewModel : ObservableObject
    {

        [ObservableProperty]
        ProfileModel profile;
        [ObservableProperty]
        Image pfp;


        public ProfileViewModel()
        {
            Profile = SharedProfile.Profile;

        }

        [RelayCommand]
        private async Task ProfileImageClicked()
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("Select photo", "Cancel", null, "Take Photo", "Choose from gallery");

            FileResult photo;

            try
            {
                switch (action)
                {
                    case "Take Photo":
                        if (!MediaPicker.Default.IsCaptureSupported)
                        {
                            await Shell.Current.DisplayAlert("Error", "Camera not supported on this device", "Ok");
                            return;
                        }
                        photo = await MediaPicker.Default.CapturePhotoAsync();

                        break;

                    case "Choose from gallery":
                        photo = await MediaPicker.Default.PickPhotoAsync();
                        break;

                    default:
                        return;

                }

                if (photo == null)
                {
                    return;
                }

                Stream photoStream = await photo.OpenReadAsync();
                var memoryStream = new MemoryStream();
                await photoStream.CopyToAsync(memoryStream);
                var photoBytes = memoryStream.ToArray();

                var navigation = new NavigationPage();


                await Shell.Current.GoToAsync(nameof(ProfilePhotoEdit), new Dictionary<string, object>
                {
                    ["profile"] = Profile,
                    ["image"] = photoBytes,

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error", "Error saving picture", "Ok");
            }



        }
    }

}

