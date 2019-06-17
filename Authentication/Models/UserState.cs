using System.Collections;
using System.Collections.Generic;

namespace BlazingComponents.Authentication.Models
{
    public class UserState
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public bool IsLoggedIn { get; set; }
        public EUserRole Role { get; set; }
        public IDictionary<string, object> UserData { get; set; }
    }
}
