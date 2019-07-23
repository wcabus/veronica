using System;
using Veronica.Backend.Domain.Models;

namespace Veronica.Backend.Domain.Repositories.Dishes
{
    public interface ICreateDish
    {
        Dish CreateDish(Guid id, Guid userId, string name, decimal? score, DateTimeOffset? lastInMenu);
    }
}