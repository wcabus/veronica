using Microsoft.EntityFrameworkCore;

namespace Veronica.Backend.Domain.EF
{
    public interface IQueryContext
    {
        DbSet<T> Set<T>() where T : class;
    }
}