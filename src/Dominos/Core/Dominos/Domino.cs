using Dominos.Core.OperationResults;

namespace Dominos.Core.Dominos;

public class Domino
{

    public int LeftSide { get; private set; }
    public int RightSide { get; private set; }

    private Domino(int leftSide, int rightSide)
    {
        LeftSide = leftSide;
        RightSide = rightSide;
    }

    public static OperationResult<Domino> Create(int leftSide, int rightSide)
    {
        if (leftSide <= 0 || leftSide >= 7)
            return OperationResult<Domino>.Fail(ErrorMessages.InvalidLeftSide);

        if (rightSide <= 0 || rightSide >= 7)
            return OperationResult<Domino>.Fail(ErrorMessages.InvalidRightSide);

        return OperationResult<Domino>.Success(new Domino(leftSide, rightSide));
    }

    public void Flip()
    {
        var buffer = LeftSide;
        LeftSide = RightSide;
        RightSide = buffer;
    }
}
