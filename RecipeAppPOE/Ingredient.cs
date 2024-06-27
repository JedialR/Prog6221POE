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
