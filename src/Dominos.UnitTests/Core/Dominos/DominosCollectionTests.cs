using System;
using System.Collections;
using Dominos.Core.Dominos;
using Dominos.Core.OperationResults;

namespace Dominos.UnitTests.Core.Dominos;

public class DominosCollectionTests
{
    [Test]
    public void Create_Should_ReturnFail_When_InputIsEmpty()
    {
        var result = DominosCollection.Create(string.Empty);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Error, Is.EqualTo(ErrorMessages.EmptyInput));
        Assert.That(result.Value, Is.EqualTo(default));
    }

    [Test]
    [TestCase("1|2,,2|1")]
    public void Create_Should_ReturnFail_When_DominoIsEmpty(string invalidInput)
    {
        var result = DominosCollection.Create(invalidInput);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Error, Is.EqualTo(ErrorMessages.DominoNullOrWhitespace));
        Assert.That(result.Value, Is.EqualTo(default));
    }

    [Test]
    [TestCase("1|2|3")]
    [TestCase("1")]
    [TestCase("a")]
    public void Create_Should_ReturnFail_When_DominoHasInvalidAmoutOfSides(string invalidInput)
    {
        var result = DominosCollection.Create(invalidInput);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Error, Is.EqualTo(ErrorMessages.InvalidDominoSides));
        Assert.That(result.Value, Is.EqualTo(default));
    }

    [Test]
    [TestCase("a|b")]
    public void Create_Should_ReturnFail_When_DominoSidesAreNotANumber(string invalidInput)
    {
        var result = DominosCollection.Create(invalidInput);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Error, Is.EqualTo(ErrorMessages.DominoSidesAreNotANumber));
        Assert.That(result.Value, Is.EqualTo(default));
    }

    [Test]
    [TestCaseSource(nameof(HappyPathCases))]
    public void Create_Should_ReturnProperResult_When_GivenProperInput(string validInput, Domino[] expectedList)
    {
        var result = DominosCollection.Create(validInput);

        Assert.IsTrue(result.IsSuccess);
        Assert.IsNotNull(result.Value);
        Assert.IsEmpty(result.Error);
        Assert.Multiple(() =>
        {
            for (var i = 0; i < result.Value.Dominos.Length; i++)
            {
                Assert.That(result.Value.Dominos[i].LeftSide, Is.EqualTo(expectedList[i].LeftSide));
                Assert.That(result.Value.Dominos[i].RightSide, Is.EqualTo(expectedList[i].RightSide));
            }
        });
    }

    private static object[] HappyPathCases =
    {
        new object[] { "1|2,3|4,5|6", new Domino[] {
                        Domino.Create(1, 2).Value,
                        Domino.Create(3, 4).Value,
                        Domino.Create(5, 6).Value
                    } },
        new object[] { "1|2", new Domino[] { Domino.Create(1, 2).Value } }
    };
}
