using ContactsApp.Data.Map;
using ContactsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new ContactMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
