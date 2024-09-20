namespace CookiesCookbook.App;
public class CookiesCookbookApp
{
    private readonly IUserInteraction _userInteraction;
    public CookiesCookbookApp(IUserInteraction userInteraction)
    {
        _userInteraction = userInteraction;
    }
    public void Run()
    {
        _userInteraction.ShowAvailableIngredients();

        List<Ingredient> chosenIngredients = _userInteraction.ReadIngredientsFromUser();

        if (chosenIngredients.Count > 0)
        {
            _userInteraction.ShowMessage("Recipe added: \n");
            Recipe recipes = new Recipe(chosenIngredients);
            _userInteraction.ShowMessage(recipes.ToString());
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

    public List<Ingredient> ReadIngredientsFromUser();
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
        ShowMessage("Create a new cookie recipe! Available ingredients are: \n");
        foreach (var ingredient in _ingredientsRegister.AvailableIngredients)
        {
            ShowMessage(ingredient.ToString());
        }
    }

    public List<Ingredient> ReadIngredientsFromUser()
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
                chosenIngredients.Add(_ingredientsRegister.GetById(id));
        
        } while (true);
        
        return chosenIngredients;
    }
}