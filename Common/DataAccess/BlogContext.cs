using System.Data.Entity;
using Common.Mappings;
using Common.Models;

namespace Common.DataAccess
{
    public class BlogContext : DbContext
    {
        public BlogContext() : base("BlogContext")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new BlogPostMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}