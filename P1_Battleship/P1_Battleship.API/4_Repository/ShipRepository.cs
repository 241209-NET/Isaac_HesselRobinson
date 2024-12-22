using Battleship.API.Data;
using Battleship.API.Model;

namespace Battleship.API.Repository;

public class ShipRepository : IShipRepository
{
    private readonly ShipContext shipContext;

    public ShipRepository(ShipContext _shipContext) => shipContext = _shipContext;

    public Ship CreateNewShip(Ship _newShip)
    {
        shipContext.ships.Add(_newShip);
        return _newShip;
    }

    public IEnumerable<Ship> GetAllShips()
    {
        return shipContext.ships.ToList();
    }
}