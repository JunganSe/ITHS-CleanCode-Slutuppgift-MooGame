using CleanCodeLaborationTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCodeLaboration.Tests;

[TestClass()]
public class GameControllerTests
{
    [TestMethod()]
    [DataRow("Bob")]
    [DataRow("Jane")]
    [DataRow("Sam himself")]
    [DataRow("_xXx_FrAgL0Rd4000_xXx_")]
    public void AskPlayerNameTest(string expected)
    {
        var gameController = new GameController(new MooGame(), new MockUi(expected), new ScoreHandler(""));
        string actual = gameController.AskPlayerName();

        Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DataRow(true, "y")]
    [DataRow(true, "Yes")]
    [DataRow(true, "ya!")]
    [DataRow(true, "   ...I dunno")]
    [DataRow(true, "of course!")]
    [DataRow(true, "pancakes!")]
    [DataRow(true, "")]
    [DataRow(true, null)]
    [DataRow(false, "n")]
    [DataRow(false, "Nevah!")]
    [DataRow(false, "nnno way")]
    public void AskTest(bool expected, string answer)
    {
        var gameController = new GameController(new MooGame(), new MockUi(answer), new ScoreHandler(""));
        bool actual = gameController.Ask("");

        Assert.AreEqual(expected, actual);
    }
}