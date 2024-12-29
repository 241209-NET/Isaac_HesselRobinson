using Battleship.API.Model;

namespace Battleship.API.GridException;


public class GridUnknownException : Exception
{
    public GridUnknownException(){}
    public GridUnknownException(int _gridId) : base(ConstructMessage(_gridId)){}
    public GridUnknownException(int _gridId, Exception inner) : base(ConstructMessage(_gridId), inner){}
    static string ConstructMessage(int _gridId)
    {
        return "Unknown Grid: " + _gridId + ". There is no grid with that Id.";
    }
}
public class GridHasShipTypeException : Exception
{
    public GridHasShipTypeException(){}
    public GridHasShipTypeException(int _gridId, string _shipType) : base(ConstructMessage(_gridId,_shipType)){}
    public GridHasShipTypeException(int _gridId, string _shipType, Exception inner) : base(ConstructMessage(_gridId,_shipType), inner){}

    static string ConstructMessage(int _gridId, string _shipType)
    {
        return "Duplicate ship type: Grid " + _gridId + " already has a " + _shipType;
    }
}
public class GridHasShipAtPositionException : Exception
{
    public GridHasShipAtPositionException(){}
    public GridHasShipAtPositionException(int _gridId, string _position, int _shipId) : base(ConstructMessage(_gridId,_position,_shipId)){}
    public GridHasShipAtPositionException(int _gridId, string _position, int _shipId, Exception inner) : base(ConstructMessage(_gridId,_position,_shipId), inner){}

    static string ConstructMessage(int _gridId, string _position, int _shipId)
    {
        return "Overlapping ships: Grid " + _gridId + " already has ship with Id " + _shipId + " at position " + _position;;
    }
}

public class CoordinateOutOfBoundsException : Exception
{
    public CoordinateOutOfBoundsException(){}
    public CoordinateOutOfBoundsException(Grid _grid, string _coordinate) : base(ConstructMessage(_grid,_coordinate)){}
    public CoordinateOutOfBoundsException(Grid _grid, string _coordinate, Exception inner) : base(ConstructMessage(_grid,_coordinate), inner){}
    static string ConstructMessage(Grid _grid, string _coordinate)
    {
        return "Coordinate out of bounds: \"" + _coordinate + "\" is outside bounds of grid " + _grid.Id + ". Maximum coordinate is " + ((char)(_grid.width+Grid.ASCII_A)) + _grid.height + ".";
    }
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