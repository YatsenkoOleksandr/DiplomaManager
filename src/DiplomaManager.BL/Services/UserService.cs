using System;
using System.Collections.Generic;
using System.Linq;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.DAL.Entities.UserEnitites;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Utils;

namespace DiplomaManager.BLL.Services
{
    public class UserService : IUserService
    {
        private IDiplomaManagerUnitOfWork Database { get; }

        public UserService(IDiplomaManagerUnitOfWork uow)
        {
            Database = uow;
        }

        public T GetUser<T>(string login)
            where T : User
        {
            return (T)Database.Users.Get(new FilterExpression<User>(u => u is T && u.Login == login)).SingleOrDefault();
        }

        public string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        public T GetUserFromFullName<T>(ICollection<PeopleName> peopleNames)
            where T : User
        {
            var firstName = peopleNames.SingleOrDefault(pn => pn.NameKind == NameKind.FirstName);
            var lastName = peopleNames.SingleOrDefault(pn => pn.NameKind == NameKind.LastName);
            var patronymic = peopleNames.SingleOrDefault(pn => pn.NameKind == NameKind.Patronymic);

            if (firstName == null || lastName == null)
                throw new InvalidOperationException("Can't get PeopleNames");

            var filters = new List<FilterExpression<User>>
            {
                new FilterExpression<User>(u =>
                    u is T &&
                    u.PeopleNames.Any(pn => pn.Name == firstName.Name || pn.Id == firstName.Id) &&
                    u.PeopleNames.Any(pn => pn.Name == lastName.Name || pn.Id == lastName.Id))
            };
            if (patronymic != null) //If Patronymic is missing
            {
                filters.Add(new FilterExpression<User>(u => u.PeopleNames.Any(pn => pn.Name == patronymic.Name || pn.Id == patronymic.Id)));
            }

            var includeExpressions = new[]
            {
                new IncludeExpression<User>(u => u.PeopleNames)
            };

            var user = Database.Users.Get(filters.ToArray(), includeExpressions).SingleOrDefault();
            return (T)user;
        }
    }
}

