namespace CookiesCookbook.Recipes;

public class Recipe
{
    private List<Ingredient> _chosenIngredients;

    public Recipe(List<Ingredient> chosenIngredients)
    {
        _chosenIngredients = chosenIngredients;
    }

    public IReadOnlyList<Ingredient> ChosenIngredients => _chosenIngredients.AsReadOnly();

    public override string ToString()
    {
        List<string> recipes = new List<string>();
        foreach (var ingredient in _chosenIngredients)
        {
            string recipe = $"{ingredient.Name}. {ingredient.PreparationInstructions()}";
            recipes.Add(recipe);
        }
        return string.Join(Environment.NewLine, recipes);
    }
}