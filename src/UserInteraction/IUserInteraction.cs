namespace CookiesCookbook.UserInteraction;

public interface IUserInteraction
{
    public void ShowMessage(string message);
    public void ShowAvailableIngredients();
    public List<Ingredient> ReadIngredientsFromUser(ref string idsOfIngredients);
    string ReadFileNameFromUser(string message);
    void ShowMessageWithoutNewLine(string message);
    string PromptToCreateNewFile(string message);
    public FileFormat ReadFileFormatFromUser();
}
