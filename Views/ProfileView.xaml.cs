using Friendships.ViewModels;

namespace Friendships.Views;

public partial class ProfileView : ContentPage
{
	public ProfileView()
	{
		InitializeComponent();

		BindingContext = new ProfileViewModel();

        Routing.RegisterRoute(nameof(ProfilePhotoEdit), typeof(ProfilePhotoEdit));
		Routing.RegisterRoute("Home", typeof(LoginView));

	}
}