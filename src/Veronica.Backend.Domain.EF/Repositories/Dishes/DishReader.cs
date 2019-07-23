using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Veronica.Backend.Domain.Models;
using Veronica.Backend.Domain.Repositories.Dishes;

namespace Veronica.Backend.Domain.EF.Repositories.Dishes
{
    public class DishReader : IReadDish, IVerifyDishExists
    {
        private readonly IQueryContext _context;

        public DishReader(IQueryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dish>> AllForUser(Guid userId)
        {
            return await _context.Set<Dish>().Where(x => x.UserId == userId)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Dish> ById(Guid dishId)
        {
            return await _context.Set<Dish>().FirstOrDefaultAsync(x => x.Id == dishId);
        }

        public async Task<bool> Exists(Guid dishId)
        {
            return await _context.Set<Dish>()
                .AnyAsync(x => x.Id == dishId)
                .ConfigureAwait(false);
        }

        public async Task<bool> Exists(Guid userId, Guid dishId)
        {
            return await _context.Set<Dish>()
                .AnyAsync(x => x.Id == dishId && x.UserId == userId)
                .ConfigureAwait(false);
        }
    }
}