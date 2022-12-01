using CleanCodeLaboration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CleanCodeLaboration.Tests;

[TestClass()]
public class MooGameTests
{
    [TestMethod()]
    public void GenerateTargetDigitsTest()
    {
        string output = MooGame.GenerateTargetDigits();

        Assert.IsTrue(output.Length == 4);
        StringAssert.Matches(output, new Regex("^(?:([0-9])(?!.*\\1))*$")); // Only unique digits.
    }

    [TestMethod()]
    [DataRow("1234", "1234", "BBBB,")]
    [DataRow("1243", "1234", "BB,CC")]
    [DataRow("1256", "1234", "BB,")]
    [DataRow("1123", "1234", "B,CCC")]
    [DataRow("5678", "1234", ",")]
    public void GenerateClueTest(string guess, string target, string expected)
    {
        string actual = MooGame.GenerateClue(target, guess);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void ParsePlayerDataTest()
    {
        string separator = "#&#"; // NOTE: Make MooGame._separator public to avoid hard coding here?
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

        var actual = MooGame.ParsePlayerData(testData);
        string actualJson = System.Text.Json.JsonSerializer.Serialize(actual);
        string expectedJson = System.Text.Json.JsonSerializer.Serialize(expected);

        Assert.AreEqual(expectedJson, actualJson);
    }
}