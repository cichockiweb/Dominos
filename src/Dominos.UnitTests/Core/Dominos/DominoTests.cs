using AutoFixture;
using Dominos.Core.Dominos;
using Dominos.Core.OperationResults;

namespace Dominos.UnitTests.Core.Dominos;

public class DominoTests
{
    [Test]
    [TestCase(0)]
    [TestCase(7)]
    public void Create_Should_ReturnFail_When_LeftSideIsNotValid(int leftSide)
    {
        var result = Domino.Create(leftSide,1);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Error, Is.EqualTo(ErrorMessages.InvalidLeftSide));
        Assert.That(result.Value, Is.EqualTo(default));
    }

    [Test]
    [TestCase(0)]
    [TestCase(7)]
    public void Create_Should_ReturnFail_When_RightSideIsNotValid(int rightSide)
    {
        var result = Domino.Create(1,rightSide);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Error, Is.EqualTo(ErrorMessages.InvalidRightSide));
        Assert.That(result.Value, Is.EqualTo(default));
    }

    [Test]
    [TestCase(1)]
    [TestCase(6)]
    [TestCase(3)]
    public void Create_Should_ReturnObject_When_BothSidesAreValid(int sideValue)
    {
        var result = Domino.Create(sideValue,sideValue);

        Assert.IsTrue(result.IsSuccess);
        Assert.IsEmpty(result.Error);
        Assert.That(result.Value.LeftSide, Is.EqualTo(sideValue));
        Assert.That(result.Value.RightSide, Is.EqualTo(sideValue));
    }

    [Test]
    public void Flip_Should_SwitchNumbers()
    {
        var leftSide = 1;
        var rightSide = 2;

        var result = Domino.Create(leftSide,rightSide);

        Assert.IsTrue(result.IsSuccess);
        Assert.IsEmpty(result.Error);
        result.Value.Flip();
        Assert.That(result.Value.LeftSide, Is.EqualTo(rightSide));
        Assert.That(result.Value.RightSide, Is.EqualTo(leftSide));
    }
}
