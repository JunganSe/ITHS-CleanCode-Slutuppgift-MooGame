using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace CleanCodeLaboration.Tests;

[TestClass()]
public class MindGameTests
{
    [TestMethod()]
    public void GenerateTargetDigitsTest()
    {
        string output = new MindGame().GenerateTargetDigits();

        Assert.IsTrue(output.Length == 4);
        StringAssert.Matches(output, new Regex("^[1-6]*$")); // Only digits 1-6.
    }

    [TestMethod()]
    [DataRow("1234", "1234", "BBBB")]
    [DataRow("1234", "1243", "BBCC")]
    [DataRow("1234", "1256", "BB..")]
    [DataRow("1234", "1523", "B.CC")]
    [DataRow("1234", "5678", "....")]
    public void GenerateClueTest(string target, string guess, string expected)
    {
        // NOTE: Test breaks if _correctLetter, _closeLetter or _wrongLetter is changed in MindGame.
        string actual = new MindGame().GenerateClue(target, guess);
        Assert.AreEqual(expected, actual);
    }
}