using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sat.Recruitment.Repository
{
    public sealed class FileUserRepository : IUserRepository
    {
        private readonly string _file;
        private List<IUser> _users = new List<IUser>();

        public FileUserRepository()
        {
            _file = Directory.GetCurrentDirectory() + "/Files/Users.txt";
        }

        public IUser FindUserBy(Func<IUser, bool> searchTerms)
        {
            var users = GetUsers();

            return users.FirstOrDefault(searchTerms);
        }

        public IList<IUser> GetUsers()
        {
            using (var reader = ReadUsersFromFile())
            {
                while (reader.Peek() >= 0)
                {
                    var lines = reader.ReadLineAsync().Result.Split(',');

                    var user = new User()
                    {
                        Name = lines[0].ToString(),
                        Email = lines[1].ToString(),
                        Phone = lines[2].ToString(),
                        Address = lines[3].ToString(),
                        UserType = lines[4].ToString(),
                        Money = decimal.Parse(lines[5].ToString()),
                    };

                    _users.Add(user);
                }
            }

            return _users;
        }

        public IResult SaveUser(IUser user)
        {
            IResult result = null;
            try
            {
                using (StreamWriter sw = File.AppendText(_file))
                {
                    sw.WriteLine(user.ToString());
                }

                result = new Result { Errors = "User Created", IsSuccess = true };
            }
            catch (Exception ex)
            {
                result = new Result { Errors = ex.Message, IsSuccess = false };
            }

            return result;
        }

        private StreamReader ReadUsersFromFile()
        {
            FileStream fileStream = new FileStream(_file, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
