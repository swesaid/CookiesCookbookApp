public class Program
{ 
    public static void Main(string[] args)
    {
        IIngredientsRegister ingredientsRegister = new IngredientsRegister();
        IUserInteraction userInteraction = new ConsoleUserInteraction(ingredientsRegister);
        CookiesCookbookApp App = new CookiesCookbookApp(userInteraction);
        App.Run();
    }

}
