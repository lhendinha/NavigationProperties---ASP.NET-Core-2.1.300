using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace NavigationProperties.Models
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; } = new List<ApplicationUserRole>();
    }
}