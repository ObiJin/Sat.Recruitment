using Sat.Recruitment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.Recruitment.Domain.Entities
{
    public sealed class UserService : IUserService
    {
        private readonly IUserFactory _userFactory;
        private readonly IUserRepository _userRepository;

        public UserService(IUserFactory userFactory, IUserRepository userRepository)
        {
            _userFactory = userFactory;
            _userRepository = userRepository;
        }

        public IResult CreateUser(IUser user)
        {
            var users = _userRepository.GetUsers();
            var fabUser = _userFactory.FabricateUser(user);

            bool duplicated = users.Any(u => u.Name == fabUser.Name || u.Phone == fabUser.Phone || u.Email == fabUser.Email || u.Address == fabUser.Address);

            if (!duplicated)
            {
                return _userRepository.SaveUser(fabUser);
            }

            return new Result()
            {
                IsSuccess = false,
                Errors = "The user is duplicated"
            };
        }

        public string Validate(IUser user)
        {
            string errors = string.Empty;

            if (string.IsNullOrEmpty(user.Name))
                errors = "The name is required.";
            if (string.IsNullOrEmpty(user.Email))
                errors += " The email is required.";
            if (string.IsNullOrEmpty(user.Address))
                errors += " The address is required.";
            if (string.IsNullOrEmpty(user.Phone))
                errors +=" The phone is required.";

            return errors;
        }
    }
}
