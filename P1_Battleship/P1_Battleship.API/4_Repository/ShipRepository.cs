using Microsoft.EntityFrameworkCore;
using Battleship.API.Data;
using Battleship.API.Model;

namespace Battleship.API.Repository;

public class ShipRepository : IShipRepository
{
    private readonly ShipContext shipContext;

    public ShipRepository(ShipContext _shipContext) => shipContext = _shipContext;

    public Ship CreateNewShip(string[] _positions, ShipType _type, string _name)
    {
        Ship newShip = new Ship(_positions, _type, _name);
        shipContext.ships.Add(newShip);
        shipContext.SaveChanges();
        return newShip;
    }

    public IEnumerable<Ship> GetAllShips()
    {
        return shipContext.ships.ToList();
    }
    public Ship? GetShipById(int _Id)
    {
        return shipContext.ships.FirstOrDefault(o => o.Id == _Id);
    }

    public Ship? HitShip(int _shipId, string _position)
    {
        Ship? ship = GetShipById(_shipId);
        if(ship != null)
        {
            ship.TakeHit(_position);
            shipContext.SaveChanges();
        }
        return ship;
    }
}