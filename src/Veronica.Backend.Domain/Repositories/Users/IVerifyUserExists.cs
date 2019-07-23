using System;
using System.Threading.Tasks;

namespace Veronica.Backend.Domain.Repositories.Users
{
    public interface IVerifyUserExists
    {
        Task<bool> Exists(Guid userId);
    }
}