﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCodeLaboration.Tests;

[TestClass()]
public class ScoreHandlerTests
{

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

        var actual = ScoreHandler.ParsePlayerData(testData);
        string actualJson = System.Text.Json.JsonSerializer.Serialize(actual);
        string expectedJson = System.Text.Json.JsonSerializer.Serialize(expected);

        Assert.AreEqual(expectedJson, actualJson);
    }

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