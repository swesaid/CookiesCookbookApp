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
    }
}
public interface IUserInteraction
{
    public void ShowMessage(string message);
    public void ShowAvailableIngredients();
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
}