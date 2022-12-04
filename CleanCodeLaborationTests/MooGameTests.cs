using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace CleanCodeLaboration.Tests;

[TestClass()]
public class MooGameTests
{
    [TestMethod()]
    public void GenerateTargetDigitsTest()
    {
        string output = new MooGame().GenerateTargetDigits();

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
        string actual = new MooGame().GenerateClue(target, guess);
        Assert.AreEqual(expected, actual);
    }
}