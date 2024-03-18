using Friendships.ViewModels;

namespace Friendships.Views;

public partial class FindFriendView : ContentPage
{
	public FindFriendView()
	{
		InitializeComponent();

		BindingContext = new FindFriendViewModel();
	}
}