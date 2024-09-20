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
        List<Ingredient> ingredients = new List<Ingredient>()
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
        _userInteraction.ShowAvailableIngredients(ingredients);
    }
}
public interface IUserInteraction
{
    public void ShowMessage(string message);
    public void ShowAvailableIngredients(List<Ingredient> availableIngredients);
}
public class ConsoleUserInteraction : IUserInteraction
{
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
    public void ShowAvailableIngredients(List<Ingredient> availableIngredients)
    {
        ShowMessage("Create a new cookie recipe! Available ingredients are: \n");
        foreach (var ingredient in availableIngredients)
        {
            ShowMessage(ingredient.ToString());
        }
    }
}