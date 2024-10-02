namespace CookiesCookbook.Recipes;

public class RecipeConverter : IRecipeConverter
{
    private readonly IIngredientsRegister _ingredientsRegister;
    public RecipeConverter(IIngredientsRegister ingredientsRegister)
    {
        _ingredientsRegister = ingredientsRegister;
    }

    public List<Recipe> ToListOfRecipes(string content)
    {
        string[] recipesAsStrings = content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        
        return recipesAsStrings.Select(recipe =>
        {
            var ingredients = recipe.Select(recipeId => _ingredientsRegister.GetById(int.Parse(recipeId.ToString())))
                                    .ToList();

            return new Recipe(ingredients);
        
       }).ToList();
    }
}