using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCodeLaboration.Tests;

[TestClass()]
public class ScoreHandlerTests
{
    [TestMethod()]
    public void StringifyPlayerDataTest()
    {
        // NOTE: Test breaks if variable "format" is changed in testee.

        var testData = new List<PlayerData>();
        testData.Add(new PlayerData("abc", 3));
        testData.Add(new PlayerData("adasda", 3));
        testData[0].AddGameEntry(8);
        testData[1].AddGameEntry(4);
        testData[0].AddGameEntry(7);

        string expected = "Player   Games  Average\nabc          3     6,00\nadasda       2     3,50";
        string actual = ScoreHandler.StringifyPlayerData(testData);
        Assert.AreEqual(expected, actual);
    }
}