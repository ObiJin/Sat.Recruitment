using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Interfaces
{
    public interface IUserService
    {
        string Validate(IUser user);

        IResult CreateUser(IUser user);
    }
}
