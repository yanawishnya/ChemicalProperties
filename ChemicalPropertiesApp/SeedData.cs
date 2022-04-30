/*using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChemicalPropertiesApp;

public class SeedData
{
    public static async Task EnsureSeedData(IServiceProvider provider)
    {
        var roleMgr = provider.GetRequiredService<RoleManager<IdentityRole>>();
        foreach (var roleName in Roles.AllRoles)
        {
            var role = roleMgr.FindByNameAsync(roleName).Result;
            if (role != null) continue;
            var result = roleMgr.CreateAsync(new IdentityRole { Name = roleName }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First());
            }
        }

        var userMgr = provider.GetRequiredService<UserManager<IdentityUser>>();
        
        var adminResult = await userMgr.CreateAsync(DefaultUsers.Administrator, "123Asd;");
        var userResult = await userMgr.CreateAsync(DefaultUsers.User, "123Sdf;");

        if (adminResult.Succeeded || userResult.Succeeded)
        {
            var adminUser = await userMgr.FindByEmailAsync(DefaultUsers.Administrator.Email);
            var basicUser = await userMgr.FindByEmailAsync(DefaultUsers.User.Email);

            await userMgr.AddToRoleAsync(adminUser.ToString(), Roles.Administrator);
            await userMgr.AddToRoleAsync(basicUser.ToString(), Roles.User);
        }
    }

    public static class Roles
    {
        public const string Administrator = "Администратор";
        public const string User = "Пользователь";

        public static IEnumerable<string> AllRoles
        {
            get
            {
                yield return Administrator;
                yield return User;
            }
        }
    }

    public static class DefaultUsers
    {
        public static readonly IdentityUser Administrator = new()
        {
            Email = "admin@admin.ru",
            EmailConfirmed = true,
            UserName = "admin@admin.ru"
        };

        public static readonly IdentityUser User = new()
        {
            Email = "user@user.ru",
            EmailConfirmed = true,
            UserName = "user@user.ru"
        };
        
        public static IEnumerable<IdentityUser> AllUsers
        {
            get
            {
                yield return Administrator;
                yield return User;
            }
        }
    }
}*/