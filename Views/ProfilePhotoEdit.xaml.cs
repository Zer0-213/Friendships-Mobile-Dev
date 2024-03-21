using Friendships.ViewModels;

namespace Friendships.Views;

public partial class ProfilePhotoEdit : ContentPage
{
    double _originalX, _originalY;
    double _originalRadiusX, _originalRadiusY;

    public ProfilePhotoEdit()
	{
		InitializeComponent();

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF5cWWJCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXxfcXRcRGdYVEJ3VkU=");
        BindingContext = new ProfilePhotoEditViewModel(potraitImage); 
	}

    private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                _originalX = EllipseClip.Center.X;
                _originalY = EllipseClip.Center.Y;
                break;
            case GestureStatus.Running:

                var newX = _originalX + e.TotalX;
                var newY = _originalY + e.TotalY;
                EllipseClip.Center = new Point(newX, newY);
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
                _originalRadiusX = EllipseClip.RadiusX;
                _originalRadiusY = EllipseClip.RadiusY;
                break;
            case GestureStatus.Running:

                EllipseClip.RadiusX = _originalRadiusX * e.Scale;
                EllipseClip.RadiusY = _originalRadiusY * e.Scale;
                break;
            case GestureStatus.Completed:
            case GestureStatus.Canceled:
                break;
        }
    }

}

