using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeAppPOE
{
    public partial class MainWindow : Window
    {
        public List<Recipe> Recipes { get; set; }
        public List<string> FoodGroups { get; set; }
        public string SelectedFoodGroup { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Recipes = new List<Recipe>();
            FoodGroups = new List<string> { "Carbohydrates", "Protein", "Dairy", "Fruit and vegetables", "Fats and sugars" };

            DataContext = this; // Setting DataContext

            RecipeDataGrid.ItemsSource = Recipes;
            FoodGroupComboBox.ItemsSource = FoodGroups;
        }

        private void AddNewRecipe_Click(object sender, RoutedEventArgs e)
        {
            var addRecipeWindow = new AddRecipeWindow(Recipes, FoodGroups);
            addRecipeWindow.ShowDialog();
            RecipeDataGrid.Items.Refresh();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void FilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filterText = FilterTextBox.Text.ToLower();
                string selectedFoodGroup = FoodGroupComboBox.SelectedItem?.ToString();
                int maxCalories;
                int.TryParse(MaxCaloriesTextBox.Text, out maxCalories);

                var filteredRecipes = Recipes.Where(r =>
                    (string.IsNullOrEmpty(filterText) || r.Ingredients.Any(i => i.Name.ToLower().Contains(filterText) || i.FoodGroup.ToLower().Contains(filterText))) &&
                    (string.IsNullOrEmpty(selectedFoodGroup) || r.Ingredients.Any(i => i.FoodGroup == selectedFoodGroup)) &&
                    (maxCalories == 0 || r.TotalCalories <= maxCalories)).ToList();

                RecipeDataGrid.ItemsSource = filteredRecipes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering recipes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetFilters_Click(object sender, RoutedEventArgs e)
        {
            FilterTextBox.Clear();
            FoodGroupComboBox.SelectedIndex = -1;
            MaxCaloriesTextBox.Clear();
            RecipeDataGrid.ItemsSource = Recipes;
        }

        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeDataGrid.SelectedItem != null)
            {
                var selectedRecipe = RecipeDataGrid.SelectedItem as Recipe;
                var scaleRecipeWindow = new ScaleRecipeWindow(selectedRecipe);
                scaleRecipeWindow.ShowDialog();
                RecipeDataGrid.Items.Refresh();
            }
        }

        private void EditRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeDataGrid.SelectedItem != null)
            {
                var selectedRecipe = RecipeDataGrid.SelectedItem as Recipe;
                var editRecipeWindow = new EditRecipeWindow(selectedRecipe, FoodGroups);
                editRecipeWindow.ShowDialog();
                RecipeDataGrid.Items.Refresh();
            }
        }

        private void FoodGroupComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (FoodGroupComboBox.SelectedItem == null)
            {
                FoodGroupComboBox.Text = "Select food group";
            }
        }

        private void FoodGroupComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Handle selection changed event here
        }
    }
}
