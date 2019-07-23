using System;
using System.Collections.Generic;

namespace Veronica.Backend.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }

        public DateTimeOffset RegistrationDate { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();

        public User()
        {
            
        }

        public User(Guid id, string email, string name, ISystemClock systemClock)
        {
            Id = id;
            Email = email;
            Name = name;

            RegistrationDate = systemClock.UtcNow;
        }
    }
}
