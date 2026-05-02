using Microsoft.AspNetCore.Identity;

namespace CoreFitness.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ProfileImagePath { get; set; } = string.Empty;
    }
}