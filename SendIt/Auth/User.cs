using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SendIt.Auth
{
    public class User:IdentityUser
    {
        [Required]
        public string FirsttName { get; set; } = string.Empty;

 
        
    }

}


