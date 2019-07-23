using System;
using Veronica.Backend.Domain.Models;

namespace Veronica.Backend.Application.Dishes
{
    public class GetDish : IQuery<Dish>
    {
        public GetDish(Guid dishId)
        {
            DishId = dishId;
        }

        public Guid DishId { get; }
    }
}