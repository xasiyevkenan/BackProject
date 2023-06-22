using Microsoft.AspNetCore.Identity;

namespace BackProject.DAL
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Lastname { get; set; }
    }
}
