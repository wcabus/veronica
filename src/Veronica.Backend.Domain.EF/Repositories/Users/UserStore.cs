using System;
using Veronica.Backend.Domain.Models;
using Veronica.Backend.Domain.Repositories.Users;

namespace Veronica.Backend.Domain.EF.Repositories.Users
{
    public class UserStore : ICreateUser
    {
        private readonly VeronicaDbContext _context;
        private readonly ISystemClock _systemClock;

        public UserStore(VeronicaDbContext context, ISystemClock systemClock)
        {
            _context = context;
            _systemClock = systemClock;
        }

        public User CreateUser(Guid id, string name, string email)
        {
            var user = new User(id, email, name, _systemClock);
            _context.Add(user);

            return user;
        }
    }
}