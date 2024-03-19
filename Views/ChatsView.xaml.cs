using Friendships.Models;
using Friendships.ViewModels;
using Syncfusion.Maui.Core.Carousel;

namespace Friendships.Views;

public partial class ChatsView : ContentPage
{

    public ChatsView()
    {
        InitializeComponent();

        BindingContext = new ChatsViewModel();
    }
}