using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cloud_In_A_Box.Authentication.Models
{
    public class LoginForm
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name needs to have between 5 and 50 characters.")]
        public string Username { get; set; } = "";

        [Required]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Password needs to be at least 5 characters long.")]
        public string Password { get; set; } = "";
    }
}
