using System;
using System.Collections.Generic;

namespace Veronica.Backend.Domain.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Language { get; set; }
        
        /// <summary>
        /// The default amount of people this recipe serves.
        /// </summary>
        public int Serves { get; set; }
        public ICollection<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();

        public Guid CreatorId { get; set; }
        public User Creator { get; set; }

        public Guid BaseRecipeId { get; set; }
        public Recipe BaseRecipe { get; set; }
    }
}