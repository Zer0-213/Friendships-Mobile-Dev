using Friendships.ViewModels;

namespace Friendships.Views;

public partial class SignUpView : ContentPage
{
	public SignUpView()
	{
		InitializeComponent();
        BindingContext = new SignUpViewModel();
    }
}