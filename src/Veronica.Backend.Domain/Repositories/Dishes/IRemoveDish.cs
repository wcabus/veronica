using System;
using System.Threading.Tasks;

namespace Veronica.Backend.Domain.Repositories.Dishes
{
    public interface IRemoveDish
    {
        Task Remove(Guid id);
    }
}