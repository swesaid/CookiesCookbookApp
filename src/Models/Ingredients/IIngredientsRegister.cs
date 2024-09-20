namespace CookiesCookbook.Models.Ingredients;
public interface IIngredientsRegister
{
    IReadOnlyList<Ingredient> AvailableIngredients { get; }
    Ingredient GetById(int id);
}