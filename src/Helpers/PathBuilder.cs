namespace CookiesCookbook.Helpers;

public class PathBuilder : IPathBuilder
{
    private string _dataFolderName;

    public PathBuilder(string dataFolderName)
    {
        _dataFolderName = dataFolderName;
    }

    private string GetProjectRootDirectory()
    {
        //Will take to the project root directory and return the full path
        var projectRootDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
        return projectRootDirectory;
    }

    public string BuildFilePath(string fileName)
    {
        string dataFolderPath = Path.Combine(GetProjectRootDirectory(), _dataFolderName);
        if (!Directory.Exists(dataFolderPath))
        {
            Directory.CreateDirectory(dataFolderPath);
        }
        return Path.Combine(dataFolderPath, fileName);
    }
}
