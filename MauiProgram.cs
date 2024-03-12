using Microsoft.Extensions.Logging;
using CropperImage.MAUI;
using Syncfusion.Maui.Core.Hosting;
using Friendships.Models;
using Friendships.ViewModels;
using Friendships.Views;

namespace Friendships
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.ConfigureSyncfusionCore();
            builder
                .UseMauiApp<App>()
                .UseImageCropper()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(new ProfileModel("test","test123",""));
            builder.Services.AddSingleton <ProfileViewModel>();
            builder.Services.AddSingleton(serviceProvider=> new ProfileView()
            {
                BindingContext = serviceProvider.GetService<ProfileViewModel>()
            });
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
