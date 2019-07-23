using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Veronica.Backend.Domain.Models;
using Veronica.Backend.Domain.Repositories.Users;
using System.Collections.Generic;

namespace Veronica.Backend.Domain.EF.Repositories.Users
{
    public class UserReader : IReadUser, IVerifyUserExists
    {
        private readonly IQueryContext _context;

        public UserReader(IQueryContext context)
        {
            _context = context;
        }

        public async Task<User> ById(Guid id)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<User>> All()
        {
            return await _context.Set<User>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<bool> Exists(Guid userId)
        {
            return await _context.Set<User>().AnyAsync(x => x.Id == userId).ConfigureAwait(false);
        }
    }
}
