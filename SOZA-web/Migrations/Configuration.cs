namespace SOZA_web.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SOZA_web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SOZA_web.Models.ApplicationDbContext context)
        {
            var adminRole = "AppAdmin";
            var defaultAdminName = "admin@admin.pl";
            var defaultAdminPass = "123456";

            if (!context.Roles.Any(r => r.Name == adminRole))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = adminRole };

                roleManager.Create(role);
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var adminUser = context.Users.FirstOrDefault(u => u.UserName == defaultAdminName);
            if (adminUser != null)
                userManager.Delete(adminUser);

            var newAdminUser = new ApplicationUser
            {
                UserName = defaultAdminName,
                SafeLatLng = new ApplicationUser.Location()
            };
            userManager.Create(newAdminUser, defaultAdminPass);
            userManager.AddToRole(newAdminUser.Id, adminRole);
        }
    }
}
