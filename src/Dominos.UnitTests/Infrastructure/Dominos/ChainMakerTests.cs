using System;
using System.Runtime.Intrinsics.Arm;
using Dominos.Core.Dominos;
using Dominos.Core.OperationResults;
using Dominos.Infrastructure.Dominos;

namespace Dominos.UnitTests.Infrastructure.Dominos;

public class ChainMakerTests
{
    private ChainMaker _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new ChainMaker();
    }

    [Test]
    public void Chain_Should_ReturnFail_When_GivenNullArray()
    {
        var result = _sut.Chain(null);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Error, Is.EqualTo(ErrorMessages.NoDominosToOrder));
        Assert.IsNull(result.Value);
    }

    [Test]
    [TestCase("5|6")]
    [TestCase("4|1,1|2,2|3")]
    public void Chain_Should_ReturnFail_When_SequenceThatCannotBeLooped(string invalidSequence)
    {
        var result = _sut.Chain(DominosCollection.Create(invalidSequence).Value);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Error, Is.EqualTo(ErrorMessages.ChainIsNotValid));
        Assert.IsNull(result.Value);
    }

    [Test]
    [TestCase("1|2,3|4")]
    [TestCase("1|2,2|3,4|5")]
    public void Chain_Should_ReturnFail_When_SequenceCannotBeBuild(string invalidSequence)
    {
        var result = _sut.Chain(DominosCollection.Create(invalidSequence).Value);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Error, Is.EqualTo(ErrorMessages.NoMatchingDominosLeft));
        Assert.IsNull(result.Value);
    }

    [Test]
    [TestCaseSource(nameof(HappyPathCases))]
    public void Chain_Should_ReturnProperChain_When_GivenProperDominos(DominosCollection dominosCollection, Domino[] expectedList)
    {
        var result = _sut.Chain(dominosCollection);

        Assert.IsTrue(result.IsSuccess);
        Assert.IsEmpty(result.Error);
        Assert.Multiple(() =>
        {
            for (var i = 0; i < result.Value.Length; i++)
            {
                Assert.That(result.Value[i].LeftSide, Is.EqualTo(expectedList[i].LeftSide));
                Assert.That(result.Value[i].RightSide, Is.EqualTo(expectedList[i].RightSide));
            }
        });
    }

    private static object[] HappyPathCases =
    {
        new object[] {
            DominosCollection.Create("1|1").Value,
            new[]{ Domino.Create(1,1).Value }
        },
        new object[] {
            DominosCollection.Create("1|2,2|3,3|1").Value,
            new[]{ 
                Domino.Create(1,2).Value,
                Domino.Create(2,3).Value,
                Domino.Create(3,1).Value
            }
        }
    };
}
