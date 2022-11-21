using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Interfaces
{
    public interface IUserRepository
    {
        IList<IUser> GetUsers();

        IUser FindUserBy(Func<IUser, bool> searchTerms);

        IResult SaveUser(IUser user);
    }
}
