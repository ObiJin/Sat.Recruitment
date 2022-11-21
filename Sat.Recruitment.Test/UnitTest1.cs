using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Repository;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        private readonly IServiceProvider _services;

        public UnitTest1()
        {
            _services = DISetup();
        }

        [Fact]
        public void Test1()
        {
            var serviceScope = _services.CreateScope();
            var userService = serviceScope.ServiceProvider.GetRequiredService<IUserService>();
            var userController = new UsersController(userService);

            var model = new User { Name = "Mike", Email = "mike@gmail.com", Address = "Av. Juan G", Phone = "+349 1122354215", UserType = "Normal",  Money = 124m };

            var result = userController.CreateUser(model).Result;

            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            var serviceScope = _services.CreateScope();
            var userService = serviceScope.ServiceProvider.GetRequiredService<IUserService>();
            var userController = new UsersController(userService);

            var model = new User { Name = "Agustina", Email = "Agustina@gmail.com", Address = "Av. Juan G", Phone = "+349 1122354215", UserType = "Normal", Money = 124m };

            var result = userController.CreateUser(model).Result;


            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        private IServiceProvider DISetup()
        {
            //Setting Dependency Injection
            var services = new ServiceCollection()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserFactory, UserFactory>()
                .AddScoped<IUserRepository, FileUserRepository>();

            return services.BuildServiceProvider();
        }
    }
}
