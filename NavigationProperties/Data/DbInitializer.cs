using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NavigationProperties.Models;
using System;
using System.Threading.Tasks;

namespace NavigationProperties.Data
{
    public class DbInitializer
    {
        #region Initialize
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager, ILogger<DbInitializer> logger)
        {
            context.Database.EnsureCreated();

            // Look for any roles.
            if (await context.Roles.AnyAsync() == false)
            {
                await CreateRoles(roleManager, logger);
            }

            // Look for any users.
            if (await context.Users.AnyAsync() == false)
            {
                await CreateUser(userManager, logger);
            }
        }
        #endregion

        #region CreateRoles
        private static async Task CreateRoles(RoleManager<ApplicationRole> roleManager, ILogger<DbInitializer> logger)
        {
            var rolesNames = new ApplicationRole[]
            {
                new ApplicationRole("DefaultRole")
            };

            foreach (ApplicationRole role in rolesNames)
            {
                logger.LogInformation($"Create the role `{ role.Name }` for application");
                var ir = await roleManager.CreateAsync(new ApplicationRole(role.Name));
                if (ir.Succeeded)
                {
                    logger.LogDebug($"Created the role `{ role.Name }` successfully");
                }
                else
                {
                    var exception = new ApplicationException($"Default role `{ role.Name }` cannot be created");
                    logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                    throw exception;
                }
            }
        }
        #endregion

        #region CreateUser
        private static async Task CreateUser(UserManager<ApplicationUser> userManager, ILogger<DbInitializer> logger)
        {
            var creatorUsers = new ApplicationUser[]
            {
                new ApplicationUser { UserName = "deso@nickrizos.com", Email = "deso@nickrizos.com", EmailConfirmed = true },
            };

            foreach (ApplicationUser user in creatorUsers)
            {
                var result = await userManager.CreateAsync(user, "BHfkzyNMZ2#");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "DefaultRole");
                    logger.LogDebug($"Created default user `{ user.Email }` successfully");
                }
                else
                {
                    var exception = new ApplicationException($"Default user `{ user.Email }` cannot be created");
                    logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(result));
                    throw exception;
                }
            }
        }
        #endregion

        #region GetIdentiryErrorsInCommaSeperatedList
        private static string GetIdentiryErrorsInCommaSeperatedList(IdentityResult ir)
        {
            string errors = null;
            foreach (var identityError in ir.Errors)
            {
                errors += identityError.Description;
                errors += ", ";
            }
            return errors;
        }
        #endregion
    }
}