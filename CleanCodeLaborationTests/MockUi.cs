using CleanCodeLaboration;

namespace CleanCodeLaborationTests;

internal class MockUi : IUi
{
    private readonly string _fakeUserInput;

    public MockUi(string fakeUserInput)
    {
        _fakeUserInput = fakeUserInput;
    }
    public string GetUserInput()
    {
        return _fakeUserInput;
    }

    public void PrintOutput(string output)
    {
        _ = output;
    }
}
