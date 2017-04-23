using System;
using System.Linq;
using Common.DataAccess;
using Common.Models;
using Ploeh.AutoFixture;

namespace Common.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BlogContext db)
        {
            var fixture = new Fixture();

            var users = fixture.Build<User>().Without(x => x.Id).Without(x => x.ReadPosts).CreateMany(10000).ToList();

            db.Users.AddRange(users);
            db.SaveChanges();

            var blogs = fixture.Build<BlogPost>().Without(x => x.Id).With(x => x.WhoHasReads, users.Take(10).ToList()).CreateMany(1000).ToList();

            db.BlogPosts.AddRange(blogs);
            db.SaveChanges();
        }
    }
}
