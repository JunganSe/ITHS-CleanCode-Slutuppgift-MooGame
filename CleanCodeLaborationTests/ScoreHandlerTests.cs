using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCodeLaboration.Tests;

[TestClass()]
public class ScoreHandlerTests
{
    [TestMethod()]
    public void ParsePlayerDataTest()
    {
        var scoreHandler = new ScoreHandler("");
        string separator = scoreHandler.Separator;
        var testData = new List<string>()
        {
            $"abc{separator}3",
            $"adasda{separator}3",
            $"abc{separator}8",
            $"adasda{separator}4",
            $"abc{separator}7"
        };
        var expected = new List<PlayerData>();
        expected.Add(new PlayerData("abc", 3));
        expected.Add(new PlayerData("adasda", 3));
        expected[0].AddGameEntry(8);
        expected[1].AddGameEntry(4);
        expected[0].AddGameEntry(7);

        var actual = scoreHandler.ParsePlayerData(testData);
        string actualJson = System.Text.Json.JsonSerializer.Serialize(actual);
        string expectedJson = System.Text.Json.JsonSerializer.Serialize(expected);

        Assert.AreEqual(expectedJson, actualJson);
    }

    [TestMethod()]
    public void StringifyPlayerDataTest()
    {
        var scoreHandler = new ScoreHandler("");
        var testData = new List<PlayerData>();
        testData.Add(new PlayerData("abc", 3));
        testData.Add(new PlayerData("adasda", 3));
        testData[0].AddGameEntry(8);
        testData[1].AddGameEntry(4);
        testData[0].AddGameEntry(7);

        string expected = string.Format(scoreHandler.StringifyFormat, "Player", "Games", "Average")
            + "\n" + string.Format(scoreHandler.StringifyFormat, "abc", "3", "6,00")
            + "\n" + string.Format(scoreHandler.StringifyFormat, "adasda", "2", "3,50");
        string actual = scoreHandler.StringifyPlayerData(testData);

        Assert.AreEqual(expected, actual);
    }
}