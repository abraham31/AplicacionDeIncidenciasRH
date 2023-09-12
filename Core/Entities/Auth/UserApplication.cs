using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Auth
{
    public class UserApplication : IdentityUser
    {
        public string Names { get; set; }
    }
}
