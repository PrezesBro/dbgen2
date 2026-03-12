using DBGenerator.Models;
using DBGenerator.Models.Ads;
using DBGenerator.Models.Blog;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DBGenerator.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Database> Databases { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Datas> Datas { get; set; }
        public DbSet<Ads> Ads { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostElement> PostElements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PostElement>(pe =>
            {
                pe.Property(e => e.Content2).IsRequired(false);
                pe.Property(e => e.Content3).IsRequired(false);
                pe.Property(e => e.Content4).IsRequired(false);
            });

            base.OnModelCreating(builder);
        }
    }
}
