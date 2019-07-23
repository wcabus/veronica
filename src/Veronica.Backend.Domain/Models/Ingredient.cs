using System;

namespace Veronica.Backend.Domain.Models
{
    public class Ingredient
    {
        public Guid Id { get; set; }

        /// <summary>
        /// The density of the ingredient, used when converting from weight to volume or vice-versa.
        /// Only needed for fluid ingredients like water, milk, ...
        /// </summary>
        public decimal? Density { get; set; }
    }
}