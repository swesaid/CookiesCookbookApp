namespace CookiesCookbook.DataAccess;

public interface IRecipesRepository
{
    public string Read(string fileName);
    public string ReadFromTxt(string fileName);
    public string ReadFromJson(string filename);
    public void Write(string idsOfIngredients, string fileContent, string fileName);
    void WriteToTxt(string idsOfIngredients, string fileName);
    void WriteToJson(string idsOfIngredients, string fileContent, string fileName);
}
