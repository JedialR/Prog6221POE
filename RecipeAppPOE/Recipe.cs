using System.Collections.Generic;
using System.Linq;

namespace RecipeAppPOE
{
    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }

        public string IngredientsDescription => string.Join(", ", Ingredients.Select(i => i.Name));
        public string StepsDescription => string.Join(", ", Steps);
        public int TotalCalories => Ingredients.Sum(i => i.Calories);

        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
        }

        public void ScaleIngredients(double scaleFactor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Scale(scaleFactor);
            }
        }

        public void ResetIngredients()
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Reset();
            }
        }
    }
}
