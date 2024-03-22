using Friendships.ViewModels;
using SkiaSharp;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Friendships.Views;

public partial class ProfilePhotoEdit : ContentPage
{
    private double _originalX, _originalY, _originalWidth, _originalHeight;

    ProfilePhotoEditViewModel viewModel;

    public ProfilePhotoEdit()
	{
		InitializeComponent();

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF5cWWJCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXxfcXRcRGdYVEJ3VkU=");

        viewModel = new ProfilePhotoEditViewModel(RectangleClip);
        BindingContext = viewModel; 
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



    private async void SaveClip_Clicked(object sender, EventArgs e)
    {
        const int clipWidth = 200;
        const int clipHeight = 200;


        var clipBitmap = new SKBitmap((int)RectangleClip.Rect.Right + clipWidth, (int)RectangleClip.Rect.Bottom + clipHeight);

        using var surface = SKSurface.Create(new SKImageInfo((int)RectangleClip.Rect.Right + clipWidth, (int)RectangleClip.Rect.Bottom + clipHeight));

        var filePath = (System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            $"{viewModel.Profile.Username}.png"));

        var canvas = surface.Canvas;
        canvas.DrawBitmap(clipBitmap, new SKRect((float)RectangleClip.Rect.Left, (float)RectangleClip.Rect.Top, (float)RectangleClip.Rect.Right + clipWidth, (float)RectangleClip.Rect.Bottom + clipHeight),
            new SKRect(0, 0, clipWidth, clipHeight));

        using var imageStream = new MemoryStream();
        surface.Snapshot().Encode(SKEncodedImageFormat.Png, 100).SaveTo(imageStream);

        var fileStream = File.Create(filePath);

        await imageStream.CopyToAsync(fileStream);
        imageStream.Close();

        viewModel.Profile.ProfilePicture = new Image()
        {
            Source = ImageSource.FromFile(filePath)
        };
        viewModel.Profile.ImageStreamToBase64(fileStream);

        fileStream.Close();

        var f = new Firebase();
        await f.CreateProfile(viewModel.Profile);

        await Shell.Current.GoToAsync("..");
    }
}

