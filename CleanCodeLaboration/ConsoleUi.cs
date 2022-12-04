namespace CleanCodeLaboration;

public class ConsoleUi : IUi
{
    public string GetUserInput()
    {
        return Console.ReadLine() ?? "";
    }

    public void PrintOutput(string output)
    {
        Console.WriteLine(output);
    }
}
