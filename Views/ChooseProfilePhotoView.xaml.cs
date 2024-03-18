using Friendships.ViewModels;

namespace Friendships.Views;

public partial class ChooseProfilePhotoView : ContentPage
{
    public ChooseProfilePhotoView()
    {
        InitializeComponent();

        BindingContext = new ChooseProfilePhotoViewModel(cropper);
    }



}