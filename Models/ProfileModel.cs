using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Friendships.Models
{
    public partial class ProfileModel : ObservableObject
    {
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string username;
        [ObservableProperty]
        Image profilePicture;
        [ObservableProperty]
        string userUid;
        [ObservableProperty]
        string profilePictureBase64;
        [JsonIgnore]
        public List<ProfileModel> Friends { get; set; }


        public ProfileModel()
        {

            Friends = new List<ProfileModel>();

        }

        public async Task ConvertBase64()
        {
            try
            {

                byte[] imageBytes = Convert.FromBase64String(ProfilePictureBase64);
                Stream stream = new MemoryStream(imageBytes);

                var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{UserUid}.png");

                FileStream fileStream = File.Create(fileName);

                await stream.CopyToAsync(fileStream);
                ProfilePicture = new Image()
                {
                    Source = ImageSource.FromFile(fileName)
                };


                fileStream.Close();



            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ImageStreamToBase64(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            MemoryStream memoryStream = new();

            stream.CopyTo(memoryStream);

            var imageBytes = memoryStream.ToArray();

            ProfilePictureBase64 = Convert.ToBase64String(imageBytes);


        }
    }
}
