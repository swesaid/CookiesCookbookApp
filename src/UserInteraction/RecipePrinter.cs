namespace CookiesCookbook.UserInteraction;

public class RecipePrinter : IRecipePrinter
{
    private readonly IUserInteraction _userInteraction;
    public RecipePrinter(IUserInteraction userInteraction)
    {
        _userInteraction = userInteraction;
    }
    public void ShowExistingRecipes(List<Recipe> recipes)
    {
        _userInteraction.ShowMessage("Existing recipes are: ");
        for (int recipe = 0; recipe < recipes.Count(); recipe++)
        {
            _userInteraction.ShowMessage($"\n*****{recipe + 1}*****");
            _userInteraction.ShowMessage(recipes[recipe].ToString());
        }
    }
}