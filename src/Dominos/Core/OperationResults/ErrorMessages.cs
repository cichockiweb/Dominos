namespace Dominos.Core.OperationResults;

public class ErrorMessages
{
    public const string InvalidLeftSide = "Left side of domino must be between 1 and 6";
    public const string InvalidRightSide = "Right side of domino must be between 1 and 6";
    public const string NoDominosToOrder = "No dominos to order";
    public const string NoMatchingDominosLeft = "There are more dominos, but non of them matches";
    public const string ChainIsNotValid = "Left side of first domino does not match right side of last domino";
    public const string EmptyInput = "Input is empty";
    public const string DominoSidesAreNotANumber = "One or both domino sides are not a number";
    public const string DominoNullOrWhitespace = "One of dominos is null or whitespace";
    public const string InvalidDominoSides = "Invalid domino with more or less than 2 sides";
}
