using System;

namespace Veronica.Backend.ApiModels.V1.Dishes
{
    public class Dish
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal? Score { get; set; }
        public DateTimeOffset Added { get; set; }
        public DateTimeOffset? LastInMenu { get; set; }
    }
}