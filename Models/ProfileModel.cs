using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendships.Models
{
    public partial class ProfileModel : ObservableObject
    {
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string username;
        [ObservableProperty]
        string profilePicture;
        [ObservableProperty]
        Image profileImage;

        public ProfileModel(string name, string username, string profilePicture)
        {
            this.name = name;
            this.username = username;
            this.profilePicture = profilePicture;
            if (string.IsNullOrWhiteSpace(profilePicture)) {
                ProfilePicture = "default_pfp.png";
            }
        }
    }
}
