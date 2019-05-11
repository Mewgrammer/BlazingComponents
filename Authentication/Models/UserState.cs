namespace Cloud_In_A_Box.Authentication.Models
{
    public class UserState
    {

        public string Id { get; set; }
        public string DisplayName { get; set; }
        public bool IsLoggedIn { get; set; }

        public EUserRole Role { get; set; }

    }
}
