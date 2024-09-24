namespace CookiesCookbook.App;

public class CookiesCookbookApp
{
    private readonly IUserInteraction _userInteraction;
    private readonly IRecipesRepository _recipesRepository;
    private readonly IRecipeConverter _recipeConverter;
    private readonly IRecipePrinter _recipePrinter;

    public CookiesCookbookApp(IUserInteraction userInteraction, IRecipesRepository recipesRepository, IRecipeConverter recipeConverter, IRecipePrinter recipePrinter)
    {
        _userInteraction = userInteraction;
        _recipesRepository = recipesRepository;
        _recipeConverter = recipeConverter;
        _recipePrinter = recipePrinter;
    }

    public void Run()
    {
        string fileName = _userInteraction.ReadFileNameFromUser("Enter the name of your cookie book: ");
        string fileContent = _recipesRepository.Read(fileName);
        
        if (fileContent is not null)
        {
            List<Recipe> recipes = _recipeConverter.ToListOfRecipes(fileContent);
            _recipePrinter.ShowExistingRecipes(recipes);
        }
        else
        {
            _userInteraction.ShowMessage("File either does not exist or is empty.\n");
            fileName = _userInteraction.PromptToCreateNewFile("Enter the name of the cookie book that you want to create: ");
        }

        string idsOfIngredients = "";
        _userInteraction.ShowAvailableIngredients();

        List<Ingredient> chosenIngredients = _userInteraction.ReadIngredientsFromUser(ref idsOfIngredients);

        if (chosenIngredients.Count > 0)
        {
            _userInteraction.ShowMessage("Recipe added: \n");
            Recipe recipes = new Recipe(chosenIngredients);
            _userInteraction.ShowMessage(recipes.ToString());
            _recipesRepository.Write(idsOfIngredients, fileContent, fileName);
        }
        else
            _userInteraction.ShowMessage("No ingredients have been selected. Recipe will not be saved.");

        _userInteraction.ShowMessage("\nPress any key to close.");
        Console.ReadKey();
    }
}