using Battleship.API.Model;

namespace Battleship.API.Repository;
public interface IShipRepository
{
    Ship CreateNewShip(Ship _newShip);
    IEnumerable<Ship> GetAllShips();
}
public interface IGridRepository
{
    Grid CreateNewGrid(Grid _newGrid);
    Grid? GetGridById(int _gridId);
    void DeleteGrid(int _gridId);
}