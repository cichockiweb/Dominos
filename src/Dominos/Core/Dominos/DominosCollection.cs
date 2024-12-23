using Dominos.Core.OperationResults;

namespace Dominos.Core.Dominos;

public class DominosCollection
{
    public Domino[] Dominos { get; private set; }

    private DominosCollection(Domino[] dominos)
    {
        Dominos = dominos;
    }

    public static OperationResult<DominosCollection> Create(string dominosInput)
    {
        if (string.IsNullOrWhiteSpace(dominosInput))
            return OperationResult<DominosCollection>.Fail(ErrorMessages.EmptyInput);

        var dominoStringItems = dominosInput.Split(',');

        var dominos = CreateDominos(dominoStringItems);
        if (!dominos.IsSuccess)
            return OperationResult<DominosCollection>.Fail(dominos.Error);

        return OperationResult<DominosCollection>.Success(new DominosCollection(dominos.Value));
    }

    private static OperationResult<Domino[]> CreateDominos(string[] dominoStringItems)
    {
        var dominos = new List<Domino>();
        foreach (var dominoItem in dominoStringItems)
        {
            if (string.IsNullOrWhiteSpace(dominoItem))
            {
                return OperationResult<Domino[]>.Fail(ErrorMessages.DominoNullOrWhitespace);
            }

            var dominoValues = dominoItem.Split('|');

            if (dominoValues.Length != 2)
            {
                return OperationResult<Domino[]>.Fail(ErrorMessages.InvalidDominoSides);
            }

            if (int.TryParse(dominoValues[0], out var leftSide) && int.TryParse(dominoValues[1], out var rightSide))
            {
                var domino = Domino.Create(leftSide, rightSide);
                if (domino.IsSuccess)
                {
                    dominos.Add(domino.Value);
                    continue;
                }
                return OperationResult<Domino[]>.Fail(domino.Error);
            }
            else
            {
                return OperationResult<Domino[]>.Fail(ErrorMessages.DominoSidesAreNotANumber);
            }
        }
        return OperationResult<Domino[]>.Success(dominos.ToArray());
    }
}
