using Microsoft.VisualStudio.TestTools.UnitTesting;
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
}