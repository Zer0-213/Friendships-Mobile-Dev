using Friendships.Models;
using Friendships.ViewModels;
using Microsoft.Maui.Controls.Internals;
using Syncfusion.Maui.Core.Carousel;
using System;

namespace Friendships.Views;

public partial class ProfileView : ContentPage
{
	ProfileViewModel viewModel;
	public ProfileView() 

    {
		InitializeComponent();

        viewModel = new ProfileViewModel();
        var imageData = Convert.FromBase64String(viewModel.Profile.ProfilePictureBase64);
        viewModel.Pfp = new Image() { Source = ImageSource.FromStream((() => new MemoryStream(imageData))) };

        BindingContext = viewModel;

        Routing.RegisterRoute(nameof(ProfilePhotoEdit), typeof(ProfilePhotoEdit));
		Routing.RegisterRoute("Home", typeof(LoginView));

	}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        Console.WriteLine("hit");
        var imageData = Convert.FromBase64String(viewModel.Profile.ProfilePictureBase64);
        viewModel.Pfp = new Image(){Source = ImageSource.FromStream((() => new MemoryStream(imageData)))};

    }
}