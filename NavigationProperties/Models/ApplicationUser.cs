using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace NavigationProperties.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [ProtectedPersonalData]
        public override string Id { get => base.Id; set => base.Id = value; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; } = new List<ApplicationUserRole>();
    }
}