using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database;
using Friendships.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendships.ViewModels
{
    [QueryProperty(nameof(Messages), "messages")]
    [QueryProperty(nameof(ToUser), "toUser")]
    public partial class ChatsViewModel : ObservableObject
    {

        [ObservableProperty]
        List<MessageModel> messages;
        [ObservableProperty]
        ProfileModel toUser;
        [ObservableProperty]
        ProfileModel profile;
        [ObservableProperty]
        string text;
        [ObservableProperty]
        bool sendButtonEnabled = true;

        public ObservableCollection<MessageModel> MessageList { get; set; } = new();


        public ChatsViewModel()
        {
            Profile = SharedProfile.Profile;
            Messages = SharedProfile.Messages;
            ToUser = SharedProfile.ToUser;

            foreach (MessageModel message in Messages)
            {
                if (Profile.UserUid == message.UserUid)
                {
                    message.Position = "End";
                }
                else message.Position = "Start";

                MessageList.Add(message);
            }

        }

        [RelayCommand]
        async Task AddMessage()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Text))
                {
                    return;
                }

                MessageModel message = new MessageModel()
                {
                    UserUid = Profile.UserUid,
                    TextMessage = Text
                };

                Messages.Add(message);
                Firebase firebase = new Firebase();

                await firebase.StoreMessages(Profile, ToUser, Messages);

                MessageList.Add(message);

                Text = "";

                OnPropertyChanged(nameof(MessageList));
            }
            catch (FirebaseException ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Close");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error", "error sending message", "Close");
            }
        }

    }
}
