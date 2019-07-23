using System;
using System.Threading.Tasks;

namespace Veronica.Backend.Domain.Repositories.Dishes
{
    public interface IVerifyDishExists
    {
        Task<bool> Exists(Guid dishId);
        Task<bool> Exists(Guid userId, Guid dishId);
    }
}