namespace Battleship.API.GridException;


public class UnknownGridException : Exception
{
    public UnknownGridException(){}
    public UnknownGridException(string message) : base(message){}
    public UnknownGridException(string message, Exception inner) : base(message, inner){}
}

public class CoordinateOutOfBoundsException : Exception
{
    public CoordinateOutOfBoundsException(){}
    public CoordinateOutOfBoundsException(string message) : base(message){}
    public CoordinateOutOfBoundsException(string message, Exception inner) : base(message, inner){}
}
public class CoordinateMalformedException : Exception
{
    public CoordinateMalformedException(){}
    public CoordinateMalformedException(string _malformedCoordinate) : base(ConstructMessage(_malformedCoordinate)){}
    public CoordinateMalformedException(string _malformedCoordinate, Exception inner) : base(ConstructMessage(_malformedCoordinate), inner){}

    static string ConstructMessage(string _malformedCoordinate)
    {
        return "Malformed coordinate: \"" + _malformedCoordinate + "\". Coordinates must be a single letter character followed by a number greater than 0.";
    }
}