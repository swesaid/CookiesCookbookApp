namespace CookiesCookbook.Models.Ingredients;

public class IngredientsRegister : IIngredientsRegister
{
    private List<Ingredient> _ingredients = new List<Ingredient>()
    {
        new WheatFlour(),
        new CoconutFlour(),
        new Butter(),
        new Chocolate(),
        new Sugar(),
        new Cardamom(),
        new Cinnamon(),
        new CocoaPowder()
    };
    public IReadOnlyList<Ingredient> AvailableIngredients => _ingredients.AsReadOnly();

    public Ingredient GetById(int id)
    { 
        return AvailableIngredients.FirstOrDefault(ingredient => id == ingredient.ID);
    }
}