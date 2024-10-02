namespace CookiesCookbook.DataAccess;

public class RecipesFileRepository : IRecipesRepository
{
    public string Read(string filename)
    {
        if (File.Exists(filename))
            return filename.EndsWith(".txt", StringComparison.OrdinalIgnoreCase)
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

    public void Write(string idsOfIngredients, string fileContent, string fileName)
    {
        if (fileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            WriteToTxt(idsOfIngredients, fileName);
        else
            WriteToJson(idsOfIngredients, fileContent, fileName);
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

    public void WriteToJson(string IdsOfIngredients, string fileContent, string fileName)
    {
        fileContent += IdsOfIngredients + Environment.NewLine;
        string[] recipes = fileContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
       
        recipes = recipes.Select(recipe => string.Join(",", recipe.ToCharArray()))
                         .ToArray();

        string jsonContent = JsonSerializer.Serialize(recipes);
        File.WriteAllText(fileName, jsonContent);
    }

}