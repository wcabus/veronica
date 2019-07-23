using System;

namespace Veronica.Backend.Domain.Models
{
    public class RecipeIngredient
    {
        public Guid RecipeId { get; set; }
        public Guid IngredientId { get; set; }

        public decimal Quantity { get; set; }
        public Unit Unit { get; set; }
    }
}