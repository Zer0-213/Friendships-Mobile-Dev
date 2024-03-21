using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CropperImage.MAUI;
using Firebase.Database;
using Friendships.Models;
using System.Buffers.Text;
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
                if (photoStream == null)
                {
                    Profile.ProfilePicture.Source = "default_pfp";
                    Profile.ProfilePictureBase64 = "";
                }
                else
                {

                    var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{Profile.UserUid}.png");

                    FileStream fileStream = File.Create(fileName);

                    await photoStream.CopyToAsync(fileStream);


                    Profile.ProfilePicture = new Image()
                    {
                        Source = fileName
                    };

                    Profile.ImageStreamToBase64(fileStream);

                    fileStream.Close();

                }

                Firebase firebase = new();

              await  firebase.CreateProfile(Profile,false);
            }
            catch (FirebaseException ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.ToString(), "Close");
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

        [RelayCommand]
       private static async Task SwipeBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}

