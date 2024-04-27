using Friendships.ViewModels;
using SkiaSharp;
using System.Runtime.CompilerServices;
using Friendships.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO;

namespace Friendships.Views;

public partial class ProfilePhotoEdit : ContentPage
{
    private double _originalX, _originalY, _originalWidth, _originalHeight;

    ProfilePhotoEditViewModel viewModel;

    public ProfilePhotoEdit()
    {
        InitializeComponent();

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
            "Ngo9BigBOggjHTQxAR8/V1NAaF5cWWJCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXxfcXRcRGdYVEJ3VkU=");

        viewModel = new ProfilePhotoEditViewModel();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        viewModel.RectangleClip = RectangleClip;
        viewModel.ImageData = ImageSource.FromStream(() => new MemoryStream(viewModel.PassedImage));
    }

    private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                _originalX = RectangleClip.Rect.X;
                _originalY = RectangleClip.Rect.Y;
                break;
            case GestureStatus.Running:
                // Move the rectangle
                RectangleClip.Rect = new Rect(_originalX + e.TotalX,
                    _originalY + e.TotalY,
                    RectangleClip.Rect.Width,
                    RectangleClip.Rect.Height);
                break;
            case GestureStatus.Completed:
            case GestureStatus.Canceled:
                break;
        }
    }

    private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
    {
        switch (e.Status)
        {
            case GestureStatus.Started:
                _originalWidth = RectangleClip.Rect.Width;
                _originalHeight = RectangleClip.Rect.Height;
                break;
            case GestureStatus.Running:
                // Zoom the rectangle
                double newWidth = _originalWidth * e.Scale;
                double newHeight = _originalHeight * e.Scale;

                // Make sure the rectangle doesn't become too small
                if (newWidth >= 50 && newHeight >= 50)
                {
                    RectangleClip.Rect = new Rect(RectangleClip.Rect.X, RectangleClip.Rect.Y, newWidth, newHeight);
                }

                break;
            case GestureStatus.Completed:
            case GestureStatus.Canceled:
                break;
        }
    }
}



