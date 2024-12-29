namespace Battleship.API.ShipException;

public class ShipUnknownException : Exception
{
    public ShipUnknownException(){}
    public ShipUnknownException(string message) : base(message){}
    public ShipUnknownException(string message, Exception inner) : base(message, inner){}
}
public class ShipNonContiguousException : Exception
{
    public ShipNonContiguousException(){}
    public ShipNonContiguousException(string[] _positions) : base(ConstructMessage(_positions)){}
    public ShipNonContiguousException(string[] _positions, Exception inner) : base(ConstructMessage(_positions), inner){}

    static string ConstructMessage(string[] _positions)
    {
        string message = "Noncontiguous coordinates in ship: ";
        for(int i = 0; i < _positions.Length; i++)
        {
            message += _positions[i] + ", ";
        }
        message += "All coordinates in the ship must be ordered contiguously in a straight line.";
        return message;
    }
}