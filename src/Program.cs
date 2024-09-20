public class Program
{ 
    public static void Main(string[] args)
    {
        IUserInteraction userInteraction = new ConsoleUserInteraction();
        CookiesCookbookApp App = new CookiesCookbookApp(userInteraction);
        App.Run();
    }

}
