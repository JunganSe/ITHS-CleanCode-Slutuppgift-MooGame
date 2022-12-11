namespace CleanCodeLaboration;

public static class FileHandler
{
    public static List<string> ReadTextFile(string path)
    {
        return File.ReadAllLines(path)?.ToList() ?? new();
    }

    public static void AppendTextFile(string filePath, string content)
    {
        var streamWriter = new StreamWriter(filePath, append: true);
        streamWriter.WriteLine(content);
        streamWriter.Close();
    }
}
