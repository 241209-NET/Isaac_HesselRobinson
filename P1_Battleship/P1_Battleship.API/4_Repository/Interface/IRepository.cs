using Battleship.API.Model;

namespace Battleship.API.Repository;
public interface IShipRepository
{
    Ship CreateNewShip(string[] _positions, ShipType _type, string _name);
    IEnumerable<Ship> GetAllShips();
    Ship? GetShipById(int _Id);
    Ship? HitShip(int _shipId, string _position);
}
public interface IGridRepository
{
    Grid CreateNewGrid(Grid _newGrid);
    Grid? GetGridById(int _gridId);
    IEnumerable<Grid> GetAllGrids();
    Grid? DeleteGrid(int _gridId);
    Grid? SetCoordinateStatus(int _gridId, string _coordinate, SquareStatus _status);
    Grid? AddShipToGrid(int _gridId, ShipType _type, int _ShipId);
}