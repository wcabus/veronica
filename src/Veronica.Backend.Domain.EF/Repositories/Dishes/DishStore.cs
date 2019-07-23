using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Veronica.Backend.Domain.Models;
using Veronica.Backend.Domain.Repositories.Dishes;

namespace Veronica.Backend.Domain.EF.Repositories.Dishes
{
    public class DishStore : ICreateDish, IRemoveDish
    {
        private readonly VeronicaDbContext _context;
        private readonly ISystemClock _systemClock;

        public DishStore(VeronicaDbContext context, ISystemClock systemClock)
        {
            _context = context;
            _systemClock = systemClock;
        }

        public Dish CreateDish(Guid id, Guid userId, string name, decimal? score, DateTimeOffset? lastInMenu)
        {
            var dish = new Dish(id, userId, name, score, lastInMenu, _systemClock);
            _context.Add(dish);

            return dish;
        }

        public async Task Remove(Guid id)
        {
            // Fetch the dish here because it needs to be tracked by the DbContext (readers do not track entities)
            var dish = await _context.Set<Dish>().FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            _context.Remove(dish);
        }
    }
}