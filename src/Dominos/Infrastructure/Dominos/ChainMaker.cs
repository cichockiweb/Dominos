using Dominos.Core.Dominos;
using Dominos.Core.OperationResults;

namespace Dominos.Infrastructure.Dominos;

public class ChainMaker
{
    public OperationResult<Domino[]> Chain(DominosCollection dominosCollection)
    {
        if (dominosCollection == null || !dominosCollection.Dominos.Any())
            return OperationResult<Domino[]>.Fail(ErrorMessages.NoDominosToOrder);

        var dominos = dominosCollection.Dominos.ToList();

        var dominosChain = new List<Domino>{
            dominos.First()
        };
        dominos.Remove(dominos.First());

        while (dominos.Count > 0)
        {
            var next = Next(dominosChain.Last(), dominos.ToArray());
            if (next.IsSuccess)
            {
                dominosChain.Add(next.Value);
                dominos.Remove(next.Value);
                continue;
            }
            return OperationResult<Domino[]>.Fail(next.Error);
        }

        if (dominosChain.First().LeftSide != dominosChain.Last().RightSide)
            return OperationResult<Domino[]>.Fail(ErrorMessages.ChainIsNotValid);

        return OperationResult<Domino[]>.Success(dominosChain.ToArray());
    }

    private OperationResult<Domino> Next(Domino previous, Domino[] dominos)
    {
        for (var i = 0; i < dominos.Length; i++)
        {
            var current = dominos[i];
            if (IsValidDominoOrder(previous, current))
                return OperationResult<Domino>.Success(current);

            current.Flip();

            if (IsValidDominoOrder(previous, current))
                return OperationResult<Domino>.Success(current);
        }
        return OperationResult<Domino>.Fail(ErrorMessages.NoMatchingDominosLeft);
    }

    private bool IsValidDominoOrder(Domino leftDomino, Domino rightDomino) =>
        leftDomino.RightSide == rightDomino.LeftSide;
}
