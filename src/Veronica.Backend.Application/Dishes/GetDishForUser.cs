using System;
using Veronica.Backend.Domain.Models;

namespace Veronica.Backend.Application.Dishes
{
    public class GetDishForUser : IQuery<Dish>
    {
        public GetDishForUser(Guid userId, Guid dishId)
        {
            UserId = userId;
            DishId = dishId;
        }

        public Guid UserId { get; }
        public Guid DishId { get; }
    }
}