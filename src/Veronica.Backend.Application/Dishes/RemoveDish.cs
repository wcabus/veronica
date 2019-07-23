using System;

namespace Veronica.Backend.Application.Dishes
{
    public class RemoveDish : ICommand
    {
        public RemoveDish(Guid userId, Guid dishId)
        {
            UserId = userId;
            DishId = dishId;
        }

        public Guid UserId { get; }
        public Guid DishId { get; }

        public bool Succeeded { get; internal set; }
    }
}