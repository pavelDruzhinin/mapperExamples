using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Common;
using Common.Dto;
using Common.Models;

namespace MapperExamples
{
    public static class ConfigureMappingTests
    {
        private const string PtopResultName = "property-to-property";
        private const string MapperResultName = "mapper";
        private static IMapper _mapper;

        public static void Run()
        {
            _mapper = RegisterMappings();

            ConfigureMapping100Tests();
            ConfigureMapping100Tests();
            ConfigureMapping1KTests();
            ConfigureMapping10KTests();

            GC.Collect();
        }

        private static void ConfigureMapping100Tests()
        {
            var users = Config.CreateObjects<User>(100);

            PerformanceTest.Run("Configure Mapping Test 100 objects",
                () =>
                {
                    var dtos = new List<AnotherUserDto>();

                    foreach (var user in users)
                    {
                        dtos.Add(new AnotherUserDto
                        {
                            UserId = user.Id,
                            FullName = $"{user.FirstName} {user.SurName} {user.LastName}",
                            About = user.About,
                            CreatedUtcDateTime = user.CreatedUtcDateTime,
                            DeleteUtcDateTime = user.DeleteUtcDateTime,
                            IsPartner = user.IsPartner,
                            IsWithoutAccess = user.IsWithoutAccess,
                            Login = user.Login,
                            Password = user.Password,
                            Rating = user.Rating
                        });
                    }

                    var userDto = dtos.OrderBy(x => x.FullName).First();
                    //Console.WriteLine(userDto.FullName);

                    return PtopResultName;
                },
                () =>
                {
                    var dtos = _mapper.Map<List<AnotherUserDto>>(users);

                    var userDto = dtos.OrderBy(x => x.FullName).First();
                    //Console.WriteLine(userDto.FullName);

                    return MapperResultName;
                });
        }

        private static void ConfigureMapping1KTests()
        {
            var users = Config.CreateObjects<User>(1000);

            PerformanceTest.Run("Configure Mapping Test 1K objects",
                () =>
                {
                    var dtos = new List<AnotherUserDto>();

                    foreach (var user in users)
                    {
                        dtos.Add(new AnotherUserDto
                        {
                            UserId = user.Id,
                            FullName = $"{user.FirstName} {user.SurName} {user.LastName}",
                            About = user.About,
                            CreatedUtcDateTime = user.CreatedUtcDateTime,
                            DeleteUtcDateTime = user.DeleteUtcDateTime,
                            IsPartner = user.IsPartner,
                            IsWithoutAccess = user.IsWithoutAccess,
                            Login = user.Login,
                            Password = user.Password,
                            Rating = user.Rating
                        });
                    }

                    var userDto = dtos.Last();

                    //Console.WriteLine(userDto.FullName);

                    return PtopResultName;
                },
                () =>
                {
                    var dtos = _mapper.Map<List<AnotherUserDto>>(users);

                    var userDto = dtos.Last();
                    //Console.WriteLine(userDto.FullName);
                    return MapperResultName;
                });
        }

        private static void ConfigureMapping10KTests()
        {
            var users = Config.CreateObjects<User>(10000);

            PerformanceTest.Run("Configure Mapping Test 10K objects",
                () =>
                {
                    var dtos = new List<AnotherUserDto>();

                    foreach (var user in users)
                    {
                        dtos.Add(new AnotherUserDto
                        {
                            UserId = user.Id,
                            FullName = $"{user.FirstName} {user.SurName} {user.LastName}",
                            About = user.About,
                            CreatedUtcDateTime = user.CreatedUtcDateTime,
                            DeleteUtcDateTime = user.DeleteUtcDateTime,
                            IsPartner = user.IsPartner,
                            IsWithoutAccess = user.IsWithoutAccess,
                            Login = user.Login,
                            Password = user.Password,
                            Rating = user.Rating
                        });
                    }

                    var userDto = dtos.First();
                    //Console.WriteLine(userDto.FullName);

                    return PtopResultName;
                },
                () =>
                {
                    var dtos = _mapper.Map<List<AnotherUserDto>>(users);

                    var userDto = dtos.First();
                    //Console.WriteLine(userDto.FullName);

                    return MapperResultName;
                });
        }

        private static IMapper RegisterMappings()
        {
            var mapperConfiguration = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<User, AnotherUserDto>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.FullName, opt => opt.MapFrom(x => $"{x.FirstName} {x.SurName} {x.LastName}"));

                cfg.CreateMap<BlogPost, BlogPostDto>();
            });

            var mapper = mapperConfiguration.CreateMapper();

            //mapper.Map<UserDto>(new User());

            return mapper;
        }
    }
}