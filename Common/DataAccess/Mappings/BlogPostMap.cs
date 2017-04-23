using System.Data.Entity.ModelConfiguration;
using Common.Models;

namespace Common.Mappings
{
    public class BlogPostMap : EntityTypeConfiguration<BlogPost>
    {
        public BlogPostMap()
        {
            ToTable("BlogPosts");

            HasKey(x => x.Id);

            HasMany(x => x.WhoHasReads);
        }
    }
}