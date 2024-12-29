using Battleship.API.Model;
using Battleship.API.Repository;
using Battleship.API.ShipException;

namespace Battleship.API.Service;

public class ShipService : IShipService
{
    string[] shipTypeNames = {"Destroyer", "Submarine", "Cruiser", "Battleship", "Carrier"};
    private readonly IShipRepository shipRepository;

    public ShipService(IShipRepository _shipRepository) => shipRepository = _shipRepository;

    public Ship CreateNewShip(string[] _positions, ShipType _type)
    {
        //Converts positions to grid squares
        GridSquare[] squares = new GridSquare[_positions.Length];
        for(int i = 0; i < _positions.Length; i++)
        {
            squares[i] = new GridSquare(_positions[i]);
        }
        //Ensures that they are all in a line
        Direction direction = squares[0].BorderDirection(squares[1]);
        if(direction == Direction.NONCONTIGUOUS)
        {
            throw new ShipNonContiguousException(_positions);
        }
        for(int i = 1; i < squares.Length-1; i++)
        {
            if(squares[i].BorderDirection(squares[i+1]) != direction)
            {
            throw new ShipNonContiguousException(_positions);
            }
        }
        //If everything went well, make the ship!
        return shipRepository.CreateNewShip(_positions, shipTypeNames[(int)_type]);
    }

    public IEnumerable<Ship> GetAllShips()
    {
        return shipRepository.GetAllShips();
    }
    public Ship? GetShipById(int _Id)
    {
        Ship? ship = shipRepository.GetShipById(_Id);
        if(ship == null)
        {
            throw new ShipUnknownException("No ship with Id " + _Id);
        }
        return ship;
    }
}