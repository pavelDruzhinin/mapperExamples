using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Common;
using Common.Dto;
using Common.Models;

namespace MapperExamples
{
    public static class SampleMappingTests
    {
        private const string PtopResultName = "property-to-property";
        private const string MapperResultName = "mapper";
        private static IMapper _mapper;

        public static void Run()
        {
            _mapper = RegisterMappings();
            RunSampleMapping100Test();
            RunSampleMapping100Test();
            RunSampleMapping1KTest();
            RunSampleMapping10KTest();

            GC.Collect();
        }

        private static void RunSampleMapping100Test()
        {
            var users = Config.CreateObjects<User>(100);

            PerformanceTest.Run("Sample Mapping Test 100 objects",
                () =>
                {
                    var dtos = new List<UserDto>();

                    foreach (var user in users)
                    {
                        dtos.Add(new UserDto
                        {
                            Id = user.Id,
                            About = user.About,
                            CreatedUtcDateTime = user.CreatedUtcDateTime,
                            DeleteUtcDateTime = user.DeleteUtcDateTime,
                            FirstName = user.FirstName,
                            IsPartner = user.IsPartner,
                            IsWithoutAccess = user.IsWithoutAccess,
                            LastName = user.LastName,
                            Login = user.Login,
                            Password = user.Password,
                            Rating = user.Rating,
                            SurName = user.SurName
                        });
                    }

                    var userDto = dtos.FirstOrDefault();

                    return PtopResultName;
                },
                () =>
                {
                    var dtos = _mapper.Map<List<UserDto>>(users);

                    var userDto = dtos.FirstOrDefault();

                    return MapperResultName;
                });
        }

        private static void RunSampleMapping1KTest()
        {
            var users = Config.CreateObjects<User>(1000);

            PerformanceTest.Run("Sample Mapping Test 1K objects",
                () =>
                {
                    var dtos = new List<UserDto>();

                    foreach (var user in users)
                    {
                        dtos.Add(new UserDto
                        {
                            Id = user.Id,
                            About = user.About,
                            CreatedUtcDateTime = user.CreatedUtcDateTime,
                            DeleteUtcDateTime = user.DeleteUtcDateTime,
                            FirstName = user.FirstName,
                            IsPartner = user.IsPartner,
                            IsWithoutAccess = user.IsWithoutAccess,
                            LastName = user.LastName,
                            Login = user.Login,
                            Password = user.Password,
                            Rating = user.Rating,
                            SurName = user.SurName
                        });
                    }

                    var userDto = dtos.OrderBy(x => x.FirstName).FirstOrDefault();

                    return PtopResultName;
                },
                () =>
                {
                    var dtos = _mapper.Map<List<UserDto>>(users);

                    var userDto = dtos.OrderBy(x => x.FirstName).FirstOrDefault();

                    return MapperResultName;
                });
        }

        private static void RunSampleMapping10KTest()
        {
            var blogs = Config.CreateObjects<BlogPost>(10000);

            PerformanceTest.Run("Sample Mapping Test 10K objects",
                () =>
                {
                    var dtos = new List<BlogPostDto>();

                    foreach (var blog in blogs)
                    {
                        dtos.Add(new BlogPostDto
                        {
                            Id = blog.Id,
                            Content = blog.Content,
                            Description = blog.Description,
                            LastUpdateUtcDateTime = blog.LastUpdateUtcDateTime,
                            DeleteUtcDateTime = blog.DeleteUtcDateTime,
                            LinkedPosts = blog.LinkedPosts,
                            PublishedUtcDateTime = blog.PublishedUtcDateTime,
                            RawContent = blog.RawContent,
                            SocialDescription = blog.SocialDescription,
                            Title = blog.Title
                        });
                    }

                    var blogDto = dtos.LastOrDefault();

                    return PtopResultName;
                },
                () =>
                {
                    var dtos = _mapper.Map<List<BlogPostDto>>(blogs);

                    var blogDto = dtos.LastOrDefault();

                    return MapperResultName;
                });
        }

        private static IMapper RegisterMappings()
        {
            var mapperConfiguration = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<BlogPost, BlogPostDto>();
            });

            var mapper = mapperConfiguration.CreateMapper();

            //mapper.Map<UserDto>(new User());

            return mapper;
        }
    }
}