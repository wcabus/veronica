using System.Threading.Tasks;

namespace Veronica.Backend.Domain
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}