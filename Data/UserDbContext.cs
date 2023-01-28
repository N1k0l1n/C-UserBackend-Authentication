using Microsoft.EntityFrameworkCore;
using UserBackend.Models;

namespace UserBackend.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

        //Making a Unique Email
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Calling the user entity and tell it that the user is unique
            modelBuilder.Entity<User>(entity => {entity.HasIndex (e => e.Email).IsUnique(); } );
        }
    }
}
