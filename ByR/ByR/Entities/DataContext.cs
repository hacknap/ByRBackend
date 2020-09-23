using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ByR.Entities;

namespace ByR.Entities
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<Role> Role { get; set; }

    }
}
