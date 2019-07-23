using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Veronica.Backend.Domain.Models;

namespace Veronica.Backend.Domain.Repositories.Users
{
    public interface IReadUser
    {
        Task<User> ById(Guid id);
        Task<IEnumerable<User>> All();
    }
}