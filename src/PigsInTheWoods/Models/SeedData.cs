using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using PigsInTheWoods.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PigsInTheWoods.Models
{
    public class SeedData : ISeedData
    {
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            // seed roles
            var roles = Enum.GetNames(typeof(Role));
            var roleStore = new RoleStore<IdentityRole>(context);

            foreach (var role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole
                    {
                        Name = role,
                        NormalizedName = role.ToUpper(),
                    });
                }
            }

            // seed users
            var defaultUsers = new List<ApplicationUser>();

            using (StreamReader reader = new StreamReader(File.OpenRead("./seeddata.json")))
            {
                var str = reader.ReadToEnd();
                dynamic dyn = JObject.Parse(str);

                var emails = dyn.adminemails;

                foreach (string email in emails)
                {
                    defaultUsers.Add(new ApplicationUser
                    {
                        Email = email,
                        NormalizedEmail = email.ToLower(),
                        UserName = email,
                        NormalizedUserName = email.ToLower(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                    });
                }
            }

            foreach (var user in defaultUsers)
            {
                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(user, "secret");
                    user.PasswordHash = hashed;

                    var userStore = new UserStore<ApplicationUser>(context);
                    var result = await userStore.CreateAsync(user);
                }
            }

            await context.SaveChangesAsync();

            _userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            await AssignRoles(defaultUsers, roles);

            await context.SaveChangesAsync();
        }

        private async Task AssignRoles(IEnumerable<ApplicationUser> users, IEnumerable<string> roles)
        {
            foreach (var user in users)
            {
                var userInDb = await _userManager.FindByEmailAsync(user.Email);
                var result = await _userManager.AddToRolesAsync(userInDb, roles);
            }
        }
    }
}
