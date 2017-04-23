using System;
using System.Linq;
using AutoMapper;
using Common;
using Common.DataAccess;
using Common.Dto;
using Common.Models;
using MapperExamples.Infrastructure;

namespace MapperExamples
{
    public static class EfMappingTests
    {
        private const string PtopResultName = "property-to-property";
        private const string MapperResultName = "mapper";

        public static void Run()
        {
            RegisterMappings();
            RunEFMapping100Tests();
            RunEFMapping100Tests();
            RunEFMapping1KTests();
            RunEFMapping10KTests();

            GC.Collect();
        }

        private static void RunEFMapping100Tests()
        {
            var count = 100;

            PerformanceTest.Run("EF Mapping Test 100 objects",
                () =>
                {
                    using (var db = new BlogContext())
                    {
                        var users = db.Users.Take(count).Select(x => new AnotherUserDto
                        {
                            UserId = x.Id,
                            FullName = string.Concat(x.FirstName, " ", x.SurName, " ", x.LastName),
                            About = x.About,
                            CreatedUtcDateTime = x.CreatedUtcDateTime,
                            DeleteUtcDateTime = x.DeleteUtcDateTime,
                            IsPartner = x.IsPartner,
                            IsWithoutAccess = x.IsWithoutAccess,
                            Login = x.Login,
                            Password = x.Password,
                            Rating = x.Rating
                        }).ToList();

                        var user = users.OrderBy(x => x.FullName).First();

                        return PtopResultName;
                    }
                },
                () =>
                {
                    using (var db = new BlogContext())
                    {
                        var users = db.Users.Take(count).ProjectToList<AnotherUserDto>();

                        var user = users.OrderBy(x => x.FullName).First();

                        return MapperResultName;
                    }
                });
        }

        private static void RunEFMapping1KTests()
        {
            var count = 1000;

            PerformanceTest.Run("EF Mapping Test 1000 objects",
                () =>
                {
                    using (var db = new BlogContext())
                    {
                        var blogs = db.BlogPosts.Take(count).Select(x => new BlogPostDto
                        {
                            Id = x.Id,
                            Description = x.Description,
                            DeleteUtcDateTime = x.DeleteUtcDateTime,
                            RawContent = x.RawContent,
                            SocialDescription = x.SocialDescription,
                            LinkedPosts = x.LinkedPosts,
                            LastUpdateUtcDateTime = x.LastUpdateUtcDateTime,
                            Content = x.Content,
                            Title = x.Title,
                            CountReaders = x.WhoHasReads.Count,
                            PublishedUtcDateTime = x.PublishedUtcDateTime
                        }).ToList();

                        var blog = blogs.OrderBy(x => x.CountReaders).First();

                        return PtopResultName;
                    }
                },
                () =>
                {
                    using (var db = new BlogContext())
                    {
                        var dtos = db.BlogPosts.Take(count).ProjectToList<BlogPostDto>();

                        var userDto = dtos.OrderBy(x => x.CountReaders).First();

                        return MapperResultName;
                    }
                });
        }

        private static void RunEFMapping10KTests()
        {
            var count = 10000;

            PerformanceTest.Run("EF Mapping Test 10000 objects",
                () =>
                {
                    using (var db = new BlogContext())
                    {
                        var dtos = db.Users.Take(count).Select(x => new AnotherUserDto
                        {
                            UserId = x.Id,
                            FullName = string.Concat(x.FirstName, " ", x.SurName, " ", x.LastName),
                            About = x.About,
                            CreatedUtcDateTime = x.CreatedUtcDateTime,
                            DeleteUtcDateTime = x.DeleteUtcDateTime,
                            IsPartner = x.IsPartner,
                            IsWithoutAccess = x.IsWithoutAccess,
                            Login = x.Login,
                            Password = x.Password,
                            Rating = x.Rating
                        }).ToList();

                        var userDto = dtos.OrderBy(x => x.FullName).First();

                        return PtopResultName;
                    }
                },
                () =>
                {
                    using (var db = new BlogContext())
                    {
                        var dtos = db.Users.Take(count).ProjectToList<AnotherUserDto>();

                        var userDto = dtos.OrderBy(x => x.FullName).First();

                        return MapperResultName;
                    }
                });
        }

        private static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, AnotherUserDto>()
                    .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.Id))
                    .ForMember(x => x.FullName, opt => opt.MapFrom(x => string.Concat(x.FirstName, " ", x.SurName, " ", x.LastName)));
                cfg.CreateMap<BlogPost, BlogPostDto>()
                    .ForMember(dest => dest.CountReaders, opt => opt.MapFrom(x => x.WhoHasReads.Count));
            });
        }
    }
}