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
            //if (!context.Roles.Any(r => r.Name == "AppAdmin"))
            //{
            //    var store = new RoleStore<IdentityRole>(context);
            //    var manager = new RoleManager<IdentityRole>(store);
            //    var role = new IdentityRole { Name = "AppAdmin" };

            //    manager.Create(role);
            //}

            //if (!context.Users.Any(u => u.UserName == "founder"))
            //{
            //    var store = new UserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser>(store);
            //    var user = new ApplicationUser { UserName = "founder" };

            //    manager.Create(user, "ChangeItAsap!");
            //    manager.AddToRole(user.Id, "AppAdmin");
            //}
        }
    }
}
