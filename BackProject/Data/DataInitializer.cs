using Microsoft.AspNetCore.Identity;
using BackProject.DAL;
using Microsoft.EntityFrameworkCore;

namespace BackProject.Data
{
    public class DataInitializer
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private AppDbContext _dbcontext;

        public DataInitializer(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbcontext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbcontext = dbcontext;
        }

        public async Task SeedData()
        {
            await _dbcontext.Database.MigrateAsync();

            var roles = new List<string> { RoleConstants.AdminRole, RoleConstants.ModeratorRole };

            foreach (var role in roles)
            {
                var existRole = await _roleManager.FindByNameAsync(role);

                if (existRole != null) continue;

                var roleResult = await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = role
                });

                if (!roleResult.Succeeded)
                {
                    //logging
                }
            }

            var existUser = await _userManager.FindByNameAsync("Admin");

            if (existUser != null) return;

            var user = new User
            {
                UserName = "Admin",
                Email = "admin@code"
            };

            var result = await _userManager.CreateAsync(user, "123456");

            if (!result.Succeeded)
            {
                //logging
            };

            result = await _userManager.AddToRoleAsync(user, RoleConstants.AdminRole);

            if (!result.Succeeded)
            {
                //logging
            }
        }
    }
}
