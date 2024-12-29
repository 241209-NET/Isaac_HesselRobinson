using Battleship.API.Model;

namespace Battleship.API.Repository;
public interface IShipRepository
{
    Ship CreateNewShip(string[] _positions, string _name);
    IEnumerable<Ship> GetAllShips();
}
public interface IGridRepository
{
    Grid CreateNewGrid(Grid _newGrid);
    Grid? GetGridById(int _gridId);
    void DeleteGrid(int _gridId);
    Grid? SetCoordinateStatus(int _gridId, string _coordinate, SquareStatus _status);
    Grid? AddShipToGrid(int _gridId, ShipType _type, int _ShipId);
}