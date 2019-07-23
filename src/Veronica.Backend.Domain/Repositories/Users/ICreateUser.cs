using System;
using Veronica.Backend.Domain.Models;

namespace Veronica.Backend.Domain.Repositories.Users
{
    public interface ICreateUser
    {
        User CreateUser(Guid id, string name, string email);
    }
}