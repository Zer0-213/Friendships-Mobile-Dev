using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Friendships.Models;
using Microsoft.Maui.Controls.Shapes;
using Image = Microsoft.Maui.Controls.Image;

using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using Path = Microsoft.Maui.Controls.Shapes.Path;


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

        [ObservableProperty]
        RectangleGeometry rectangleClip;



        public ProfilePhotoEditViewModel(RectangleGeometry rectangleClip)
        {
            RectangleClip = rectangleClip;

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

                var sourceBitmap = GetBitmapFromImage();

                var filePath = (System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    $"{Profile.Username}.png"));

                var stream = File.Create(filePath);

                var image = SKImage.FromBitmap(sourceBitmap);

                var data = image.Encode(SKEncodedImageFormat.Png, 100);

                data.SaveTo(stream);

                stream.Close();
                data.AsStream().Close();



                Profile.ProfilePicture = new Image()
                {
                    Source = ImageSource.FromFile(filePath)
                };

                Profile.ImageStreamToBase64(File.OpenRead(filePath));

                var firebase = new Firebase();

                await firebase.CreateProfile(Profile);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error", "error saving image", "close");
            }
            finally
            {
                await Shell.Current.GoToAsync("..");

            }
        }

        private SKBitmap GetBitmapFromImage()
        {
            var ms = new MemoryStream();

            var imageStream = File.OpenRead(PassedImage);

            imageStream.CopyTo(ms);
            ms.Position = 0;

            imageStream.Close();

            var s = SKBitmap.Decode(ms);

            ms.Close();

            return s;
        }

        private SKBitmap ClipBitmap(SKBitmap sourceBitmap)
        {
            SKBitmap clippedBitmap = new SKBitmap(sourceBitmap.Width, sourceBitmap.Height);

            using (var canvas = new SKCanvas(clippedBitmap))
            {
                canvas.ClipRect(new SKRect((float)RectangleClip.Rect.Left, (float)RectangleClip.Rect.Top, (float)RectangleClip.Rect.Right, (float)RectangleClip.Rect.Bottom));

                canvas.DrawBitmap(sourceBitmap, new SKPoint());

            }

            return clippedBitmap;
        }


        [RelayCommand]
        private static async Task SwipeBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}

