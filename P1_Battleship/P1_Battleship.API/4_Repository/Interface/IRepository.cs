using Battleship.API.Model;

namespace Battleship.API.Repository;
public interface IShipRepository
{
    Ship CreateNewShip(Ship _newShip);
    IEnumerable<Ship> GetAllShips();
}