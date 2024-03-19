using Friendships.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendships.ViewModels
{
    public static class SharedProfile
    {
        public static ProfileModel Profile { get; set; }
        public static ProfileModel ToUser {  get; set; }
        public static List<MessageModel> Messages { get; set; }
    }
}
