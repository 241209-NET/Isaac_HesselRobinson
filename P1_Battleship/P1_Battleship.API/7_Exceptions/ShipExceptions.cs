namespace Battleship.API.ShipException;

public class ShipUnknownException : Exception
{
    public ShipUnknownException(){}
    public ShipUnknownException(int _shipId) : base(ConstructMessage(_shipId)){}
    public ShipUnknownException(int _shipId, Exception inner) : base(ConstructMessage(_shipId), inner){}
    static string ConstructMessage(int _shipId)
    {
        return "Unknown Ship: " + _shipId + ". There is no ship with that Id.";
    }
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