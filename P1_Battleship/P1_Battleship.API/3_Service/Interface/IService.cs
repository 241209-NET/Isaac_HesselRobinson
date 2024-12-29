using Battleship.API.Model;

namespace Battleship.API.Service;

public interface IShipService
{
    Ship CreateNewShip(string[] _positions, ShipType _type);
    IEnumerable<Ship> GetAllShips();
}

public interface IGridService
{
    Grid CreateNewGrid(int _width, int _height);
    Grid CreateNewGrid(Grid _newGrid);
    Grid? GetGridById(int _gridId);
    Grid? DeleteGrid(int _gridId);
    Grid? ShootAtCoordinate(int _gridId, string _coordinate);
}