using System;
using System.Collections.Generic;
using Veronica.Backend.Domain.Models;

namespace Veronica.Backend.Application.Dishes
{
    public class GetDishesForUser : IQuery<IEnumerable<Dish>>
    {
        public GetDishesForUser(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}