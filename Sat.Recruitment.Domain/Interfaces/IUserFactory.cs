using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Interfaces
{
    public interface IUserFactory
    {
        IUser FabricateUser(IUser user);
    }
}
