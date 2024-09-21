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
        List<Recipe> recipes = new List<Recipe>();
        string[] recipesAsStrings = content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < recipesAsStrings.Length; i++)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            int ingredientCount = recipesAsStrings[i].Length;
            for (int j = 0; j < ingredientCount; j++)
            {
                int recipeId = int.Parse(recipesAsStrings[i][j].ToString());
                ingredients.Add(_ingredientsRegister.GetById(recipeId));
            }
            var recipe = new Recipe(ingredients);
            recipes.Add(recipe);
        }
        return recipes;
    }
}