using CleanCodeLaboration;

namespace CleanCodeLaborationTests;

internal class MockUi : IUi
{
    private readonly string _answer;

    public MockUi(string answer)
    {
        _answer = answer;
    }
    public string GetUserInput()
    {
        return _answer;
    }

    public void PrintOutput(string output)
    {
        _ = output;
    }
}
