using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeAppPOE
{
    public partial class AddRecipeWindow : Window
    {
        private List<Recipe> recipes;
        private List<string> foodGroups;
        private List<Ingredient> temporaryIngredients;

        public AddRecipeWindow(List<Recipe> recipes, List<string> foodGroups)
        {
            InitializeComponent();
            this.recipes = recipes;
            this.foodGroups = foodGroups;
            temporaryIngredients = new List<Ingredient>();

            IngredientsDataGrid.ItemsSource = temporaryIngredients;
            IngredientFoodGroupComboBox.ItemsSource = foodGroups; // Set the ComboBox ItemsSource
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ingredient = new Ingredient(
                    IngredientNameTextBox.Text,
                    double.Parse(IngredientQuantityTextBox.Text),
                    IngredientUnitTextBox.Text,
                    int.Parse(IngredientCaloriesTextBox.Text),
                    IngredientFoodGroupComboBox.SelectedItem.ToString());

                temporaryIngredients.Add(ingredient);
                IngredientsDataGrid.Items.Refresh();
                ClearIngredientInputs();
            }
            catch
            {
                MessageBox.Show("Please enter valid ingredient details.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            var recipe = new Recipe
            {
                Name = RecipeNameTextBox.Text,
                Ingredients = new List<Ingredient>(temporaryIngredients),
                Steps = StepsTextBox.Text.Split('\n').ToList()
            };

            recipes.Add(recipe);
            Close();
        }

        private void ClearIngredientInputs()
        {
            IngredientNameTextBox.Clear();
            IngredientQuantityTextBox.Clear();
            IngredientUnitTextBox.Clear();
            IngredientCaloriesTextBox.Clear();
            IngredientFoodGroupComboBox.SelectedIndex = -1; // Clear the ComboBox selection
        }
    }
}

