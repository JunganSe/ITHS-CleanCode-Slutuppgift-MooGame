using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCodeLaboration.Tests;

[TestClass()]
public class ScoreHandlerTests
{
    [TestMethod()]
    public void ParsePlayerDataTest()
    {
        var scoreHandler = new ScoreHandler("");

        var expectedData = new List<PlayerData>();
        expectedData.Add(new PlayerData("abc", 3));
        expectedData.Add(new PlayerData("adasda", 3));
        expectedData[0].AddGameEntry(8);
        expectedData[1].AddGameEntry(4);
        expectedData[0].AddGameEntry(7);
        string expectedJson = System.Text.Json.JsonSerializer.Serialize(expectedData);

        string separator = scoreHandler.Separator;
        var testData = new List<string>()
        {
            $"abc{separator}3",
            $"adasda{separator}3",
            $"abc{separator}8",
            $"adasda{separator}4",
            $"abc{separator}7"
        };
        var actualData = scoreHandler.ParsePlayerData(testData);
        string actualJson = System.Text.Json.JsonSerializer.Serialize(actualData);

        Assert.AreEqual(expectedJson, actualJson);
    }

    [TestMethod()]
    public void StringifyPlayerDataTest()
    {
        var scoreHandler = new ScoreHandler("");

        string expected = string.Format(scoreHandler.StringifyFormat, "Player", "Games", "Average")
            + "\n" + string.Format(scoreHandler.StringifyFormat, "abc", "3", "6,00")
            + "\n" + string.Format(scoreHandler.StringifyFormat, "adasda", "2", "3,50");

        var testData = new List<PlayerData>();
        testData.Add(new PlayerData("abc", 3));
        testData.Add(new PlayerData("adasda", 3));
        testData[0].AddGameEntry(8);
        testData[1].AddGameEntry(4);
        testData[0].AddGameEntry(7);
        string actual = scoreHandler.StringifyPlayerData(testData);

        Assert.AreEqual(expected, actual);
    }
}