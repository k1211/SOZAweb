using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SOZA_web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("SozaDb", throwIfV1Schema: false)
        {
        }/*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }*/
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<AndroidClient> AndroidClients { get; set; }
        public DbSet<GPSTrace> GPSTraces { get; set; }
    }
}