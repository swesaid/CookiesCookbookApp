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

        string idsOfIngredients = "";
        _userInteraction.ShowAvailableIngredients();

        List<Ingredient> chosenIngredients = _userInteraction.ReadIngredientsFromUser(ref idsOfIngredients);

        if (chosenIngredients.Count > 0)
        {
            _userInteraction.ShowMessage("Recipe added: \n");
            Recipe recipes = new Recipe(chosenIngredients);
            _userInteraction.ShowMessage(recipes.ToString());
            _recipesRepository.WriteToTxt(idsOfIngredients, fileName);
        }
        else
            _userInteraction.ShowMessage("No ingredients have been selected. Recipe will not be saved.");

        _userInteraction.ShowMessage("\nPress any key to close.");
        Console.ReadKey();
    }
}
public interface IUserInteraction
{
    public void ShowMessage(string message);
    public void ShowAvailableIngredients();
    public List<Ingredient> ReadIngredientsFromUser(ref string idsOfIngredients);
    string ReadFileNameFromUser(string message);
    void ShowMessageWithoutNewLine(string message);
}
public class ConsoleUserInteraction : IUserInteraction
{
    private readonly IIngredientsRegister _ingredientsRegister;
    public ConsoleUserInteraction(IIngredientsRegister ingredientsRegister)
    {
        _ingredientsRegister = ingredientsRegister;
    }
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }   
    public void ShowAvailableIngredients()
    {
        ShowMessage("\nCreate a new cookie recipe! Available ingredients are: \n");
        foreach (var ingredient in _ingredientsRegister.AvailableIngredients)
        {
            ShowMessage(ingredient.ToString());
        }
    }

    public List<Ingredient> ReadIngredientsFromUser(ref string idsOfIngredients)
    {
        List<Ingredient> chosenIngredients = new List<Ingredient>();
        int id = 0;
        bool shouldNotStop;
        
        do
        {
            ShowMessage("\nAdd an ingredient by its ID or type anything else if finished.");
            shouldNotStop = int.TryParse(Console.ReadLine(), out id);
            if (!shouldNotStop)
                break;
            if (id < 1 || id > _ingredientsRegister.AvailableIngredients.Count)
            {
                ShowMessage("Wrong input. Please provide a valid ingredient id.");
                continue;
            }
            else
            {
                idsOfIngredients += id; //at the end of the code pass it to the write to file function as a string and use it.
                chosenIngredients.Add(_ingredientsRegister.GetById(id));
            }
        
        } while (true);
        
        return chosenIngredients;
    }

    public void ShowMessageWithoutNewLine(string message)
    {
        Console.Write(message);
    }

    public string ReadFileNameFromUser(string message)
    {
        ShowMessageWithoutNewLine(message);
        string fileName = Console.ReadLine();
        return fileName;
    }
}

public interface IRecipesRepository
{
    public string Read(string fileName);
    public string ReadFromTxt(string fileName);
    public string ReadFromJson(string filename);
    void WriteToTxt(string idsOfIngredients, string fileName);
}

public class RecipesFileRepository : IRecipesRepository
{
    public string Read(string filename)
    {
        if (File.Exists(filename))
            return (filename.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            ? ReadFromTxt(filename)
            : ReadFromJson(filename);
        return null;
    }
    public string ReadFromTxt(string filename)
    {
        string content = File.ReadAllText(filename);
        string[] tmp = content.Split(',', StringSplitOptions.None);
        content = string.Join("", tmp);

        return content;
    }

    public string ReadFromJson(string filename)
    {
        string[] recipes = JsonSerializer.Deserialize<string[]>(File.ReadAllText(filename));
        string recipesAsString = "";
        for (int i = 0; i < recipes.Length; i++)
        {
            recipesAsString += string.Join("", recipes[i].Split(',', StringSplitOptions.None)) + Environment.NewLine;
        }
        return recipesAsString;
    }

    public void WriteToTxt(string idsOfIngredients, string fileName)
    {
        //from "135" -> to ["1", "3", "5"] then to -> "1, 3, 5"
        using (StreamWriter file = new StreamWriter(fileName, append: true))
        {
            char[] integers = idsOfIngredients.ToCharArray();
            idsOfIngredients = string.Join(",", integers);
            file.WriteLine(idsOfIngredients);
        }
    }

}