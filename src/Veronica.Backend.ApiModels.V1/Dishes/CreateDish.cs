using System;

namespace Veronica.Backend.ApiModels.V1.Dishes
{
    public class CreateDish
    {
        public string Name { get; set; }
        public decimal? Score { get; set; }
        public DateTimeOffset? LastInMenu { get; set; }
    }
}