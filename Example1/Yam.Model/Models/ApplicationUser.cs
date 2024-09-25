using Microsoft.AspNetCore.Identity;

namespace Yam.Model.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Tags { get; set; }
    }
}
