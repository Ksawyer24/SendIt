using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SendIt.Auth;

namespace SendIt.Data
{
    public class SendAuthDbContext:IdentityDbContext<User>
    {
        public SendAuthDbContext(DbContextOptions <SendAuthDbContext>options): base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Roles into Database
            var mainAdminRoleId = "92243373-b7d8-45da-aee9-4b77a8149697";

            var roles = new List<IdentityRole>
            {

               new IdentityRole
               {
                 Id = mainAdminRoleId,
                 ConcurrencyStamp = mainAdminRoleId,
                 Name = "Admin",
                 NormalizedName = "ADMIN"
               }

            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }


    }
}
