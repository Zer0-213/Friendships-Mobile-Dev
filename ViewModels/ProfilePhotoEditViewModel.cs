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
        private static async Task SwipeBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}

