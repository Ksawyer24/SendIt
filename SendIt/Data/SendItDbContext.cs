using Microsoft.EntityFrameworkCore;
using SendIt.Models;

namespace SendIt.Data
{
    public class SendItDbContext:DbContext
    {
        public SendItDbContext(DbContextOptions<SendItDbContext> options) :base(options) 
        {
            
        }






        public DbSet<Book> Books { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
