using System;

namespace Veronica.Backend.Domain.Models
{
    public class Dish
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal? Score { get; set; }
        public DateTimeOffset Added { get; set; }
        public DateTimeOffset? LastInMenu { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Dish()
        {
            
        }

        public Dish(Guid id, Guid userId, string name, decimal? score, DateTimeOffset? lastInMenu, ISystemClock systemClock)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Score = score;
            LastInMenu = lastInMenu;

            Added = systemClock.UtcNow;
        }
    }
}