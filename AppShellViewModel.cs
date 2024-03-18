using CommunityToolkit.Mvvm.ComponentModel;
using Friendships.Models;
using Friendships.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace Friendships
{
    public partial class AppShellViewModel : ObservableObject
    {
        [ObservableProperty]
        ProfileModel profile;
        [ObservableProperty]
        Image pfp;

        public AppShellViewModel()
        {
            Profile = SharedProfile.Profile;
        }
    }
}
