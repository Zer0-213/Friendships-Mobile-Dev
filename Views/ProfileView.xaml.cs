using Friendships.ViewModels;

namespace Friendships.Views;

public partial class ProfileView : ContentPage
{
	public ProfileView()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(ProfilePhotoEdit), typeof(ProfilePhotoEdit));
		Routing.RegisterRoute(nameof(ChooseProfilePhotoView), typeof(ChooseProfilePhotoView));

        BindingContext = new ProfileViewModel();


	}
}