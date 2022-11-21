using Sat.Recruitment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Entities
{
    public sealed class UserFactory : IUserFactory
    {
        public IUser FabricateUser(IUser user)
        {
            decimal percentage = CalculatePercentage(user.Money, user.UserType);

            decimal gif = user.Money * percentage;
            user.Money += gif;

            return user;
        }

        private decimal CalculatePercentage(decimal money, string userType)
        {
            decimal percentage = 0m;
            switch (userType)
            {
                case "Normal":
                    if (money > 100)
                    {
                        percentage = 0.12m;
                    }
                    else if (money > 10)
                    {
                        percentage = 0.8m;
                    }
                    break;
                case "SuperUser":
                    if (money > 100)
                    {
                        percentage = 0.20m;
                    }
                    break;
                case "Premium":
                    if (money > 100)
                    {
                        percentage = 2m;
                    }
                    break;
                default:
                    break;
            }

            return percentage;
        }

        private string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            /// I bet this above equals this below.
            //user.Email = user.Email.Replace(".", "").Replace("+", "").Trim();

            return string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
