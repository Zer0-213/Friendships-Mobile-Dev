using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Friendships.Models;
using Friendships.Views;

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

                        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.png");

                        FileStream fileStream = File.Create(fileName);


                        await photoStream.CopyToAsync(fileStream);

                        fileStream.Close();
                        photoStream.Dispose();

                        await Shell.Current.GoToAsync(nameof(ProfilePhotoEdit), new Dictionary<string, object>
                        {
                            ["profile"] = Profile,
                            ["image"] = fileName,

                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        await Shell.Current.DisplayAlert("Error", "Error saving picture", "Ok");
                    }

                    break;

                case "Choose from gallery":

                    await Shell.Current.GoToAsync("ChooseProfilePhotoView", new Dictionary<string, object>
                    {
                        ["profile"] = Profile,
                    });
                    break;
            }
        }

    }
}
