using Microsoft.EntityFrameworkCore;
using Website.Models;

namespace Website.Models
{
    public class PortDbContext : DbContext
    {
        public PortDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Skill> skills { get; set; }
        public DbSet<Admin> admin { get; set; }
        public DbSet<Technology> technology { get; set; }
        public DbSet<Project> projects { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Course> Course { get; set; } 
        public DbSet<Resume> Resume { get; set; }
        public DbSet<Profile> profile { get; set; }
        public DbSet<Experience> Experience { get; set; }

    }
}
