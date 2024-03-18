using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendships.Models
{
    public partial class MessageModel : ObservableObject
    {
        [ObservableProperty]
        string textMessage;
    }
}
