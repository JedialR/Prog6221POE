RecipeAppPOE

Overview:

This WPF-based tool called the Recipe Application is intended to assist users in organizing, sizing, and filtering their recipe collections. Users can filter recipes by food group or ingredient, scale ingredient proportions, and contribute new recipes.

Features:

- Add multiple new recipes: Users can add new recipes, by adding a name, ingredients, and steps.
- Scale Recipes: Users can select users to select recipes that they want to scale the quantities of by any factor.
- Filter: Users can filter recipes based on names, food groups, and maximum calories
- Edit Recipes: Users can update existing recipes

Project Recipe:
RecipeAppPOE/
MainWindow.xaml - The main window of the application.
MainWindow.xaml.cs - The code-behind file for the main window.
ScaleRecipeWindow.xaml - The window for scaling recipes.
ScaleRecipeWindow.xaml.cs - The code-behind file for the scale recipe window.
Ingredient.cs - The class representing an ingredient.
Recipe.cs - The class representing a recipe.
AddRecipeWindow.xaml - The window for adding and editing recipes.
AddRecipeWindow.xaml.cs - The code-behind file for the add recipe window.

Prerequisites
.NET 8.0

git clone https://github.com/JedialR/Prog6221POE.git

cd prog6221-part-2-JedialR


Setup:

1. Clone Repository 
https://github.com/JedialR/Prog6221POE.git

2. Open the Project in Visual Studio

Open Visual Studio.
Select File > Open > Project/Solution.
Navigate to the cloned repository folder and select the RecipeAppPOE.sln file.

3. Build Solution:
Build the solution by pressing Ctrl+Shift+B or selecting Build > Build Solution.

4. Run the Appilcation 
Press the green play button

Usage:
Adding Recipes:
-Click on File > Add New Recipe.
-Enter the recipe name, ingredients, and steps.
-Click OK to save the recipe.
Scaling Recipes:
-Select a recipe from the DataGrid.
-Click the Scale button.
-Enter the scale factor.
-The ingredient quantities and calories will be updated according to the scale factor.
Filtering Recipes:
-Use the filter options to specify ingredient name, food group, or maximum calories.
-Click the Filter button to apply the filters.
-Click the Reset button to clear all filters.
Editing Recipes:
-Select a recipe from the DataGrid.
-Click the Edit button.
-Update the recipe details.
-Click OK to save the changes.

Code Snippets:

Ingredient Class:
public class Ingredient
{
    public string Name { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
    public int Calories { get; set; }
    public string FoodGroup { get; set; }

    private double originalQuantity;
    private int originalCalories;

    public Ingredient(string name, double quantity, string unit, int calories, string foodGroup)
    {
        Name = name;
        Quantity = quantity;
        Unit = unit;
        Calories = calories;
        FoodGroup = foodGroup;

        originalQuantity = quantity;
        originalCalories = calories;
    }

    public void Scale(double factor)
    {
        Quantity = originalQuantity * factor;
        Calories = (int)(originalCalories * factor);
    }

    public void Reset()
    {
        Quantity = originalQuantity;
        Calories = originalCalories;
    }
}

Recipe Class:
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

Main Window Xaml:
<Window x:Class="RecipeAppPOE.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Recipe Application" Height="450" Width="800">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>

        <Menu Grid.Row="0">
            <MenuItem Header="File">
                <MenuItem Header="Add New Recipe" Click="AddNewRecipe_Click"/>
                <MenuItem Header="Exit" Click="Exit_Click"/>
            </MenuItem>
        </Menu>

        <StackPanel Grid.Row="1" Orientation="Horizontal" Margin="10">
            <StackPanel Orientation="Vertical" Margin="0,0,10,0">
                <TextBlock Text="Filter by ingredient or food group"/>
                <TextBox x:Name="FilterTextBox" Width="200"/>
            </StackPanel>
            <StackPanel Orientation="Vertical" Margin="0,0,10,0">
                <TextBlock Text="Select food group"/>
                <ComboBox x:Name="FoodGroupComboBox" Width="150" ItemsSource="{Binding FoodGroups}" SelectedItem="{Binding SelectedFoodGroup}" Loaded="FoodGroupComboBox_Loaded" SelectionChanged="FoodGroupComboBox_SelectionChanged"/>
            </StackPanel>
            <StackPanel Orientation="Vertical" Margin="0,0,10,0">
                <TextBlock Text="Max calories"/>
                <TextBox x:Name="MaxCaloriesTextBox" Width="100"/>
            </StackPanel>
            <Button Content="Filter" Click="FilterRecipes_Click"/>
            <Button Content="Reset" Click="ResetFilters_Click" Margin="10,0,0,0"/>
        </StackPanel>

        <DataGrid x:Name="RecipeDataGrid" Grid.Row="2" AutoGenerateColumns="False" CanUserAddRows="False" ItemsSource="{Binding Recipes}" IsReadOnly="True">
            <DataGrid.Columns>
                <DataGridTextColumn Header="Name" Binding="{Binding Name}"/>
                <DataGridTextColumn Header="Ingredients" Binding="{Binding IngredientsDescription}"/>
                <DataGridTextColumn Header="Steps" Binding="{Binding StepsDescription}"/>
                <DataGridTextColumn Header="Total Calories" Binding="{Binding TotalCalories}"/>
                <DataGridTemplateColumn Header="Actions">
                    <DataGridTemplateColumn.CellTemplate>
                        <DataTemplate>
                            <StackPanel Orientation="Horizontal">
                                <Button Content="Scale" Click="ScaleRecipe_Click"/>
                                <Button Content="Edit" Click="EditRecipe_Click"/>
                            </StackPanel>
                        </DataTemplate>
                    </DataGridTemplateColumn.CellTemplate>
                </DataGridTemplateColumn>
            </DataGrid.Columns>
        </DataGrid>
    </Grid>
</Window>


MainWindow.xaml.cs:
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

            DataContext = this; // Set the data context for data binding
        }

        private void AddNewRecipe_Click(object sender, RoutedEventArgs e)
        {
            var addRecipeWindow = new AddRecipeWindow();
            if (addRecipeWindow.ShowDialog() == true)
            {
                Recipes.Add(addRecipeWindow.NewRecipe);
                RecipeDataGrid.Items.Refresh();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void FoodGroupComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            FoodGroupComboBox.ItemsSource = FoodGroups;
        }

        private void FoodGroupComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            FilterRecipes();
        }

        private void FilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            FilterRecipes();
        }

        private void ResetFilters_Click(object sender, RoutedEventArgs e)
        {
            FilterTextBox.Text = string.Empty;
            FoodGroupComboBox.SelectedItem = null;
            MaxCaloriesTextBox.Text = string.Empty;
            RecipeDataGrid.ItemsSource = Recipes;
        }

        private void FilterRecipes()
        {
            var filteredRecipes = Recipes.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(FilterTextBox.Text))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.Name.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase)));
            }

            if (!string.IsNullOrWhiteSpace(SelectedFoodGroup))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.FoodGroup.Equals(SelectedFoodGroup, StringComparison.OrdinalIgnoreCase)));
            }

            if (int.TryParse(MaxCaloriesTextBox.Text, out int maxCalories))
            {
                filteredRecipes = filteredRecipes.Where(r => r.TotalCalories <= maxCalories);
            }

            RecipeDataGrid.ItemsSource = filteredRecipes.ToList();
        }

        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeDataGrid.SelectedItem is Recipe selectedRecipe)
            {
                var scaleRecipeWindow = new ScaleRecipeWindow(selectedRecipe);
                scaleRecipeWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a recipe to scale.", "No Recipe Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeDataGrid.SelectedItem is Recipe selectedRecipe)
            {
                var editRecipeWindow = new AddRecipeWindow(selectedRecipe);
                if (editRecipeWindow.ShowDialog() == true)
                {
                    RecipeDataGrid.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please select a recipe to edit.", "No Recipe Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}




