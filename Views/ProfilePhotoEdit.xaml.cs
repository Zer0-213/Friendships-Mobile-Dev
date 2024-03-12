using Friendships.ViewModels;

namespace Friendships.Views;

public partial class ProfilePhotoEdit : ContentPage
{

    public ProfilePhotoEdit()
	{
		InitializeComponent();

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF5cWWJCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXxfcXRcRGdYVEJ3VkU=");
        BindingContext = new ProfilePhotoEditViewModel(potraitImage); 
	}

}