namespace CookiesCookbook.UserInteraction;

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
        FileFormat format = ReadFileFormatFromUser();
        fileName += format == FileFormat.Json ? ".json" : ".txt";

        return fileName;
    }

    public string PromptToCreateNewFile(string message)
    {
        while (true)
        {
            string fileName = ReadFileNameFromUser(message);
            if (File.Exists(fileName))
            {
                ShowMessage("Such file exists please, enter another name.\n");
                continue;
            }
            ShowMessage("File sucessfully created!");
            return fileName;
        }
    }

    public FileFormat ReadFileFormatFromUser()
    {
        do
        {
            ShowMessage("Choose file format: \n\n1.JSON\n2.Txt");
            if (!int.TryParse(Console.ReadLine(), out int number))
            {
                ShowMessage("Invalid input\n");
                continue;
            }
            switch (number)
            {
                case 1:
                    return FileFormat.Json;
                case 2:
                    return FileFormat.Txt;
                default:
                    ShowMessage("Invalid choice");
                    break;
            }
        } while (true);
    }
}
