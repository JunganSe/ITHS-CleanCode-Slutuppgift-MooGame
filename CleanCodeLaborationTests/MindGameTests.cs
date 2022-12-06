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
}