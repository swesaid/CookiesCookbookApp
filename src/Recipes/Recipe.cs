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
        var ingredients = _chosenIngredients.Select(ingredient => $"{ingredient.Name}. {ingredient.PreparationInstructions()}").ToList();
        return string.Join(Environment.NewLine, ingredients);
    }
}