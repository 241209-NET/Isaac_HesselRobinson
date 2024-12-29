using Battleship.API.Model;
using Battleship.API.Repository;
using Battleship.API.GridException;

namespace Battleship.API.Service;

public class GridService : IGridService
{
    private readonly IGridRepository gridRepository;
    private readonly IShipService shipService;

    public GridService(IGridRepository _gridRepository, IShipService _shipService)
    {
        gridRepository = _gridRepository;
        shipService = _shipService;
    } 


    public Grid CreateNewGrid(int _width, int _height)
    {
        Grid newGrid = new Grid(_width, _height);
        return CreateNewGrid(newGrid);
    }
    public Grid CreateNewGrid(Grid _newGrid)
    {
        return gridRepository.CreateNewGrid(_newGrid);
    }
    public Grid? GetGridById(int _gridId)
    {
        Grid? grid = gridRepository.GetGridById(_gridId);
        if(grid == null)
        {
            throw new GridUnknownException("There is no grid with Id " + _gridId);
        }
        return grid;
    }
    public Grid? DeleteGrid(int _gridId)
    {
        var grid = GetGridById(_gridId);
        if(grid != null)
        {
            gridRepository.DeleteGrid(_gridId);
        }
        return grid;
    }
    
    public Grid? ShootAtCoordinate(int _gridId, string _coordinate)
    {
        var grid = GetGridById(_gridId);
        if(grid != null)
        {
            if(grid.IsSquareOnGrid(_coordinate))
            {
                gridRepository.SetCoordinateStatus(_gridId, _coordinate, SquareStatus.MISS);
            }
        }
        return grid;
    }
    
    public Grid? AddShipToGrid(int _gridId, int _shipId)
    {
        var grid = GetGridById(_gridId);
        var ship = shipService.GetShipById(_shipId);
        if(grid != null && ship != null)
        {
            //Prevents duplicate ships
            if(grid.HasShipOfType(ship.type))
            {
                throw new GridHasShipTypeException(_gridId,ShipService.GetNameOfShipType(ship.type));
            }
            //Prevents a ship that's off the grid
            else if(!grid.IsSquareOnGrid(new GridSquare(ship.positions[0])))
            {
                throw new CoordinateOutOfBoundsException(grid,ship.positions[0]);
            }
            else if(!grid.IsSquareOnGrid(new GridSquare(ship.positions[ship.positions.Length-1])))
            {
                throw new CoordinateOutOfBoundsException(grid,ship.positions[ship.positions.Length-1]);
            }
            //If no exceptions, add the ship
            else
            {
                gridRepository.AddShipToGrid(_gridId,ship.type, ship.Id);
            }
        }
        return grid;
    }
}