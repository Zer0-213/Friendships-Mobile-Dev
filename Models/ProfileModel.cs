using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        string profileImagePath; 
        [ObservableProperty]
        string userUid;
        [JsonIgnore]
        public List<ProfileModel> Friends { get; set; }


        public ProfileModel()
        {


            ProfilePicture = "default_pfp";

            Friends = new List<ProfileModel>();

        }

        public void ConvertBase64()
        {
            try
            {


                byte[] imageBytes = Convert.FromBase64String(ProfilePicture);


                Stream stream = new MemoryStream(imageBytes);

                string tempFileName = Path.Combine(Path.GetTempPath(), $"{Username}.png");

                try
                {
                    File.WriteAllBytes(tempFileName, imageBytes);


                    ProfileImagePath = tempFileName;
                }
                catch (Exception ex)
                {
                    // Handle exceptions accordingly
                    Console.WriteLine($"Error converting image: {ex.Message}");
                    ProfileImagePath = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
