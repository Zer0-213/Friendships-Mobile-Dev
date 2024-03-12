using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CropperImage.MAUI;
using Friendships.Models;
using System.ComponentModel;


namespace Friendships.ViewModels
{
    [QueryProperty(nameof(PassedImage), "image")]
    [QueryProperty(nameof(Profile), "profile")]
    public partial class ProfilePhotoEditViewModel : ObservableObject
    {


        [ObservableProperty]
        string passedImage;

        [ObservableProperty]
        ProfileModel profile;

        PortraitView cropper;

        [ObservableProperty]
        ImageSource source;

        public ProfilePhotoEditViewModel(PortraitView cropper)
        {
            Source = ImageSource.FromFile("");

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

                var photoStream = await cropper.CaptureAsync();

                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "profile.png");

                FileStream fileStream = File.Create(fileName);

                await photoStream.CopyToAsync(fileStream);

                fileStream.Close();

                Profile.ProfilePicture = fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error", "Error saving image", "Ok");
            }
            finally
            {
                await Shell.Current.GoToAsync("..");
            }
        }


    }


}

