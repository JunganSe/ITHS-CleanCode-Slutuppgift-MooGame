using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCodeLaboration.Tests;

[TestClass()]
public class ScoreHandlerTests
{
    private ScoreHandler? _scoreHandler;

    [TestInitialize()]
    public void Initialize()
    {
        _scoreHandler = new ScoreHandler("");
    }

    [TestMethod()]
    public void ParsePlayerDataTest()
    {
        var expectedData = new List<PlayerData>();
        expectedData.Add(new PlayerData("abc", 3));
        expectedData.Add(new PlayerData("adasda", 3));
        expectedData[0].AddGameEntry(8);
        expectedData[1].AddGameEntry(4);
        expectedData[0].AddGameEntry(7);
        string expectedJson = System.Text.Json.JsonSerializer.Serialize(expectedData);

        string separator = _scoreHandler!.Separator;
        var testData = new List<string>()
        {
            $"abc{separator}3",
            $"adasda{separator}3",
            $"abc{separator}8",
            $"adasda{separator}4",
            $"abc{separator}7"
        };
        var actualData = _scoreHandler.ParsePlayerData(testData);
        string actualJson = System.Text.Json.JsonSerializer.Serialize(actualData);

        Assert.AreEqual(expectedJson, actualJson);
    }

    [TestMethod()]
    public void StringifyPlayerDataTest()
    {
        string expected = string.Format(_scoreHandler!.StringifyFormat, "Player", "Games", "Average")
            + "\n" + string.Format(_scoreHandler.StringifyFormat, "abc", "3", "6,00")
            + "\n" + string.Format(_scoreHandler.StringifyFormat, "adasda", "2", "3,50");

        var testData = new List<PlayerData>();
        testData.Add(new PlayerData("abc", 3));
        testData.Add(new PlayerData("adasda", 3));
        testData[0].AddGameEntry(8);
        testData[1].AddGameEntry(4);
        testData[0].AddGameEntry(7);
        string actual = _scoreHandler.StringifyPlayerData(testData);

        Assert.AreEqual(expected, actual);
    }
}