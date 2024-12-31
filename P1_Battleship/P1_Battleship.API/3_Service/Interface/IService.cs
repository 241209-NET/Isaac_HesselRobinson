using Battleship.API.Model;

namespace Battleship.API.Service;

public interface IShipService
{
    Ship CreateNewShip(string[] _positions, ShipType _type);
    IEnumerable<Ship> GetAllShips();
    Ship? GetShipById(int _Id);
    Ship? HitShip(int _shipId, string _position);
}

public interface IGridService
{
    IEnumerable<Grid> GetAllGrids();
    Grid? GetGridById(int _gridId);
    IEnumerable<Ship> GetShipsInGrid(int _gridId);
    Grid CreateNewGrid(int _width, int _height);
    Grid CreateNewGrid(Grid _newGrid);
    Grid? DeleteGrid(int _gridId);
    Grid? ShootAtCoordinate(int _gridId, string _coordinate);
    Grid? AddShipToGrid(int _gridId, int _shipId);
    OverlappingShipResult AnyShipInGridAtPosition(int _gridId, string _position);
}