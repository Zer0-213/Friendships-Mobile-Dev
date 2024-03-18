using Friendships.ViewModels;

namespace Friendships.Views;

public partial class LoginView : ContentPage
{

    public LoginView()
    {
        InitializeComponent();

        BindingContext = new LoginViewModel();

        Routing.RegisterRoute(nameof(SignUpView), typeof(SignUpView));

        Routing.RegisterRoute("MainTab", typeof(ChatsDashboardView));

    }

}


