using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Veronica.Backend.Domain.Models;

namespace Veronica.Backend.Domain.Repositories.Dishes
{
    public interface IReadDish
    {
        Task<Dish> ById(Guid dishId);
        Task<IEnumerable<Dish>> AllForUser(Guid userId);
    }
}