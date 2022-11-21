using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<IResult> CreateUser(User model)
        {
            var errors = _userService.Validate(model);

            if (string.IsNullOrEmpty(errors))
            {
                var result = _userService.CreateUser(model);

                return result;
            }

            return new Result()
            {
                IsSuccess = false,
                Errors = errors
            };
        }
    }
}
