namespace CookiesCookbook.Models.Ingredients;

public abstract class Spices : Ingredient
{
    public override string PreparationInstructions() => $"Take a half a teaspoon. {base.PreparationInstructions()}";
}