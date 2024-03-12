using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Friendships.Views;

namespace Friendships.ViewModels
{
    partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        ImageSource profilePicture;


        public ProfileViewModel()
        {
            ProfilePicture = "default_pfp.png";
        }

        [RelayCommand]
        private async Task ProfileImageClicked()
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("Select photo", "Cancel", null, "Take Photo", "Choose from gallery");
            switch (action)
            {
                case "Take Photo":
                    if (!MediaPicker.Default.IsCaptureSupported)
                    {
                        await Shell.Current.DisplayAlert("Error", "Camera not supported on this device", "Ok");
                        return;
                    }

                    try
                    {
                        FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                    if (photo == null)
                    {
                        return;
                    }


                        Stream photoStream = await photo.OpenReadAsync();

                        Image image = new();
                        image.Source = ImageSource.FromStream(() => photoStream);

                        await Shell.Current.GoToAsync(nameof(ProfilePhotoEdit), new Dictionary<string, object>
                        {
                            ["image"] = image
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        await Shell.Current.DisplayAlert("Error", "Error saving picture", "Ok");
                    }

                    break;

                case "Choose from gallery":

                    await Shell.Current.GoToAsync("ChooseProfilePhotoView");
                    break;
            }
        }

    }
}
