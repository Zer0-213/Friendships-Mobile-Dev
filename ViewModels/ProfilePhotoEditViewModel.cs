using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Friendships.Models;
using Microsoft.Maui.Controls.Shapes;
using SkiaSharp;
using Syncfusion.Maui.Core.Carousel;



namespace Friendships.ViewModels
{
    [QueryProperty(nameof(PassedImage), "image")]
    [QueryProperty(nameof(Profile), "profile")]
    public partial class ProfilePhotoEditViewModel : ObservableObject
    {


        [ObservableProperty]
        byte[] passedImage;

        [ObservableProperty]
        ImageSource imageData;

        [ObservableProperty]
        ProfileModel profile;

        [ObservableProperty]
        RectangleGeometry rectangleClip;

        [RelayCommand]
        private static async Task CancelButtonClicked()
        {
            await Shell.Current.GoToAsync("..");
        }


        [RelayCommand]
        private static async Task SwipeBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task SaveClip()
        {

            var image = SKImage.FromEncodedData(PassedImage);
            var bm = SKBitmap.FromImage(image);


            var canvas = new SKCanvas(bm);

            canvas.DrawBitmap(bm, new SKPoint(0, 0));
            canvas.ClipRect(new SKRect((float)RectangleClip.Rect.Left, (float)RectangleClip.Rect.Top, (float)RectangleClip.Rect.Right, (float)RectangleClip.Rect.Bottom));

            canvas.Save();

            byte[] imageBytes;
            using (var imageStream = new MemoryStream())
            {
                bm.Encode(SKEncodedImageFormat.Png, 100).SaveTo(imageStream);
                imageBytes = imageStream.ToArray();
            }


            Profile.ProfilePictureBase64 = Convert.ToBase64String(imageBytes);

            var f = new Firebase();
            await f.CreateProfile(Profile);

            await Shell.Current.GoToAsync("..");
        }
    }

}

