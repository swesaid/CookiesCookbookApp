namespace CookiesCookbook.Recipes;

public interface IRecipeConverter
{
    List<Recipe> ToListOfRecipes(string content);
}