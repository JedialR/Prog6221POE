   M S B A M L   `   `   `  ����  RecipeAppPOE#    RecipeAppPOE.EditRecipeWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Edit Recipe��5   	   $	��600��6   $	��600��6*   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   �� .+�5      �� $��Recipe Name:��6   �� 5      -   5         ��Name$ RecipeNameTextBox��5      �� 5      $&�� Ingredients (add one at a time):��6   �� 5       �� Orientation$ 
Horizontal=�6   $��0,10,0,0q�62   .+�5      �� -   5        $ IngredientNameTextBox��5      $	��100��69   $��0,0,10,0q�6E   $
��Name��6W   �� 5      -   5        $ IngredientQuantityTextBox��5      $��50��6=   $��0,0,10,0q�6H   $	��Qty��6Z   �� 5      -   5        $ IngredientUnitTextBox��5      $��50��69   $��0,0,10,0q�6D   $
��Unit��6V   �� 5      -   5        $ IngredientCaloriesTextBox��5      $��50��6=   $��0,0,10,0q�6H   $	��Cal��6Z   �� 5      -   5         ��Name$! IngredientFoodGroupComboBox��5      $	��150��6@   $��0,0,10,0q�6L   �� 5      -   5        $��Add Ingredient��5      62   5      &   System.Windows.Controls.DataGrid  5      -   5          Name$ IngredientsDataGrid��5         AutoGenerateColumns .     CanUserAddRows .  $	��150��6g      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Name��6)   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 67   �� *Name+  5      $	 Quantity��6)   
 6;   �� *
Quantity+  5      $
	 Unit��6)   
 67   �� *Unit+  5      $	 Calories��6)   
 6;   �� *
Calories+  5      $	 
Food Group��6)   
 6=   �� *	FoodGroup+4  .System.Windows.Controls.DataGridTemplateColumn  5      $	 Actions��6-      CellTemplate 5       �� 5!      .R�5"   "   �� -	   5        $��Remove��5"   )   6:   5#      5%      5'      �� 5(      $��Steps (one per line):��6   �� 5)      -
   5        $ StepsTextBox��5)       �� AcceptsReturn . $	��100��6A   5*      �� 5,   
   -   5        $��Save Changes��5,      $��2��6(   $��0,10,0,0q�65   $��Center=�6G   6d   5-      5.      