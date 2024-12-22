using Battleship.API.Model;

namespace Battleship.API.Service;

public interface IShipService
{
    Ship CreateNewShip(Ship _newShip);
    IEnumerable<Ship> GetAllShips();
}