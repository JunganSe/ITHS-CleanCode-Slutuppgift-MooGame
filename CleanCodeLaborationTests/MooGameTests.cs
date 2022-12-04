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
    [DataRow("1234", "1243", "BB,CC")]
    [DataRow("1234", "1256", "BB,")]
    [DataRow("1234", "1123", "B,CCC")]
    [DataRow("1234", "5678", ",")]
    public void GenerateClueTest(string target, string guess, string expected)
    {
        string actual = new MooGame().GenerateClue(target, guess);
        Assert.AreEqual(expected, actual);
    }
}