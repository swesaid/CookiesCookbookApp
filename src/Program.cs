public class Program
{ 
    public static void Main(string[] args)
    {
        string dataFolderName = "data";
        IPathBuilder pathBuilder = new PathBuilder(dataFolderName);

        IIngredientsRegister ingredientsRegister = new IngredientsRegister();
        IUserInteraction userInteraction = new ConsoleUserInteraction(ingredientsRegister, pathBuilder);
        IRecipesRepository recipesRepository = new RecipesFileRepository();
        IRecipeConverter recipeConverter = new RecipeConverter(ingredientsRegister);
        IRecipePrinter recipePrinter = new RecipePrinter(userInteraction);

        CookiesCookbookApp App = new CookiesCookbookApp(userInteraction, recipesRepository, recipeConverter, recipePrinter);
        App.Run();
    }
}
