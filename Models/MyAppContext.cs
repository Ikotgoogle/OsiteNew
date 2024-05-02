using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;

namespace OsiteNew.Models {
    public class MyAppContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }

        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) {
            Database.EnsureCreated();
        }
    }
}
