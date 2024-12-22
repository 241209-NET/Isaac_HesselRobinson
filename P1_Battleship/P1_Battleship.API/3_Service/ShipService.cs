using Battleship.API.Model;
using Battleship.API.Repository;

namespace Battleship.API.Service;

public class ShipService : IShipService
{
    private readonly IShipRespository shipRepository;

    public ShipService(IShipRespository _shipRepository) => shipRepository = _shipRepository;

    public Ship CreateNewShip(Ship _newShip)
    {
        return shipRepository.CreateNewShip(_newShip);
    }

    public IEnumerable<Ship> GetAllShips()
    {
        return shipRepository.GetAllShips();
    }
}