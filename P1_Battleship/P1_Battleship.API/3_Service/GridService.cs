using Battleship.API.Model;
using Battleship.API.Repository;
using Battleship.API.GridException;

namespace Battleship.API.Service;

public struct OverlappingShipResult
{
    public string position = "";
    public int shipId = -1;
    public OverlappingShipResult() {}
    public OverlappingShipResult(string _position, int _Id) 
    {
        position = _position;
        shipId = _Id;
    }
}

public class GridService : IGridService
{
    private readonly IGridRepository gridRepository;
    private readonly IShipService shipService;

    public GridService(IGridRepository _gridRepository, IShipService _shipService)
    {
        gridRepository = _gridRepository;
        shipService = _shipService;
    }
    
///////////////////////////////////////////////////////////////////////////////
///GET
///////////////////////////////////////////////////////////////////////////////
    public Grid? GetGridById(int _gridId)
    {
        Grid? grid = gridRepository.GetGridById(_gridId);
        if(grid == null)
        {
            throw new GridUnknownException(_gridId);
        }
        return grid;
    }
    public IEnumerable<Grid> GetAllGrids()
    {
        return gridRepository.GetAllGrids();
    }
    public IEnumerable<Ship> GetShipsInGrid(int _gridId)
    {
        Grid? grid = GetGridById(_gridId);
        List<Ship> ships = new List<Ship>();
        if(grid != null)
        {
            foreach(int shipId in grid.shipIds)
            {
                if(shipId > -1)
                {
                    Ship? ship = shipService.GetShipById(shipId);
                    if(ship != null)
                    {
                        ships.Add(ship);
                    }
                }
            }
        }
        return ships;
    }

///////////////////////////////////////////////////////////////////////////////
///POST
///////////////////////////////////////////////////////////////////////////////
    public Grid CreateNewGrid(int _width, int _height)
    {
        Grid newGrid = new Grid(_width, _height);
        return CreateNewGrid(newGrid);
    }
    public Grid CreateNewGrid(Grid _newGrid)
    {
        return gridRepository.CreateNewGrid(_newGrid);
    }

///////////////////////////////////////////////////////////////////////////////
///DELETE
///////////////////////////////////////////////////////////////////////////////
    public Grid? DeleteGrid(int _gridId)
    {
        var grid = GetGridById(_gridId);
        if(grid != null)
        {
            return gridRepository.DeleteGrid(_gridId);
        }
        return grid;
    }
    
///////////////////////////////////////////////////////////////////////////////
///PATCH
///////////////////////////////////////////////////////////////////////////////
    public Grid? ShootAtCoordinate(int _gridId, string _coordinate)
    {
        var grid = GetGridById(_gridId);
        if(grid != null)
        {
            //Prevents a shot that would be off the grid
            if(!grid.IsSquareOnGrid(_coordinate))
            {
                throw new CoordinateOutOfBoundsException(grid, _coordinate);
            }
            //Checks if it's a hit; if so, hits the ship
            else
            {
                OverlappingShipResult result = AnyShipInGridAtPosition(_gridId,_coordinate);
                //Hit
                if(result.position == _coordinate)
                {
                    gridRepository.SetCoordinateStatus(_gridId, _coordinate, SquareStatus.HIT);
                    shipService.HitShip(result.shipId,_coordinate);
                    MarkShipIfSunk(_gridId, result.shipId);
                }
                //Miss
                else
                {
                    gridRepository.SetCoordinateStatus(_gridId, _coordinate, SquareStatus.MISS);
                }
            }
        }
        return grid;
    }

    /// <summary>
    /// Adds an existing ship to this grid
    /// </summary>
    /// <param name="_gridId"></param>
    /// <param name="_shipId"></param>
    /// <returns></returns>
    /// <exception cref="GridHasShipTypeException"></exception>
    /// <exception cref="CoordinateOutOfBoundsException"></exception>
    /// <exception cref="GridHasShipAtPositionException"></exception>
    public Grid? AddShipToGrid(int _gridId, int _shipId)
    {
        var grid = GetGridById(_gridId);
        var ship = shipService.GetShipById(_shipId);
        if(grid != null && ship != null)
        {
            OverlappingShipResult overlapping = AnyShipInGridOverlaps(_gridId,_shipId);
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
            //Prevents a ship that would be on top of an existing ship
            else if(overlapping.position != "")
            {
                throw new GridHasShipAtPositionException(_gridId,overlapping.position, overlapping.shipId);
            }
            //If no exceptions, add the ship
            else
            {
                gridRepository.AddShipToGrid(_gridId,ship.type, ship.Id);
            }
        }
        return grid;
    }
    
///////////////////////////////////////////////////////////////////////////////
///UTIL
///////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Checks if the indicated ship is sunk; if so, marks all of its positions accordingly
    /// </summary>
    /// <param name="_gridId"></param>
    /// <param name="_shipId"></param>
    public Grid? MarkShipIfSunk(int _gridId, int _shipId)
    {
        Ship? ship = shipService.GetShipById(_shipId);
        Grid? returnForTesting = gridRepository.GetGridById(_gridId);
        if(ship != null && !ship.IsAlive())
        {
            foreach(string position in ship.positions)
            {
                returnForTesting = gridRepository.SetCoordinateStatus(_gridId, position, SquareStatus.SUNK);
            }
        }
        return returnForTesting;
    }

    /// <summary>
    /// Returns whether any ship in the indicated grid already occupies any of the indicated ship's coordinates. If no overlap, the return will have "" as position
    /// </summary>
    /// <param name="_gridId"></param>
    /// <param name="_shipId"></param>
    /// <returns></returns>
    public OverlappingShipResult AnyShipInGridOverlaps(int _gridId, int _shipId)
    {
        var grid = GetGridById(_gridId);
        var ship = shipService.GetShipById(_shipId);
        List<Ship> shipsInGrid = (List<Ship>)GetShipsInGrid(_gridId);
        if(grid != null && ship != null)
        {
            foreach(string newShipPosition in ship.positions)
            {
                foreach(Ship existingShip in shipsInGrid)
                {
                    foreach(string existingShipPosition in existingShip.positions)
                    {
                        if(newShipPosition == existingShipPosition)
                        {
                            return new OverlappingShipResult(newShipPosition, existingShip.Id);
                        }
                    }
                }
            }
        }
        return new OverlappingShipResult();
    }

    /// <summary>
    /// Returns any ship that is already at the indicated position. If no overlap, the return will have "" as position
    /// </summary>
    /// <param name="_gridId"></param>
    /// <param name="_position"></param>
    /// <returns></returns>
    public OverlappingShipResult AnyShipInGridAtPosition(int _gridId, string _position)
    {
        var grid = GetGridById(_gridId);
        List<Ship> shipsInGrid = (List<Ship>)GetShipsInGrid(_gridId);
        if(grid != null)
        {
            foreach(Ship existingShip in shipsInGrid)
            {
                foreach(string existingShipPosition in existingShip.positions)
                {
                    if(_position == existingShipPosition)
                    {
                        return new OverlappingShipResult(_position, existingShip.Id);
                    }
                }
            }
        }
        return new OverlappingShipResult();
    }
}