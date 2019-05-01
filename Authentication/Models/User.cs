using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud_In_A_Box.Authentication.Models
{

    public enum EUserRole
    {
        Guest = 0,
        User = 1,
        Moderator = 2,
        Administrator = 3,
    }

    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public EUserRole Role { get; set; }

        public UserState GetUserState()
        {
            return new UserState
            {
                Id = Id,
                DisplayName = Username,
            };
        }
    }
}
