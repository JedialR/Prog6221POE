using System.Collections.Generic;
using System.Windows;

namespace RecipeAppPOE
{
    public partial class ScaleRecipeWindow : Window
    {
        private Recipe recipe;
        private List<Ingredient> originalIngredients;

        public ScaleRecipeWindow(Recipe recipe)
        {
            InitializeComponent();

            this.recipe = recipe;
            originalIngredients = new List<Ingredient>();

            foreach (var ingredient in recipe.Ingredients)
            {
                originalIngredients.Add(new Ingredient(ingredient.Name, ingredient.Quantity, ingredient.Unit, ingredient.Calories, ingredient.FoodGroup));
            }

            IngredientsListBox.ItemsSource = recipe.Ingredients;
        }

        private void Scale_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ScaleFactorTextBox.Text, out double scaleFactor))
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    ingredient.Scale(scaleFactor);
                }

                IngredientsListBox.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please enter a valid scale factor.", "Invalid Scale Factor", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.Reset();
            }

            IngredientsListBox.Items.Refresh();
        }
    }
}
