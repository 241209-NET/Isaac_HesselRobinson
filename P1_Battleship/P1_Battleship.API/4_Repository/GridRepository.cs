using Battleship.API.Data;
using Battleship.API.Model;

namespace Battleship.API.Repository;

public class GridRepository : IGridRepository
{
    private readonly GridContext gridContext;

    public GridRepository(GridContext _gridContext) => gridContext = _gridContext;

    public Grid CreateNewGrid(Grid _newGrid)
    {
        gridContext.grids.Add(_newGrid);
        gridContext.SaveChanges();
        return _newGrid;
    }
    public Grid? GetGridById(int _gridId)
    {
        return gridContext.grids.Find(_gridId);
    }
    public void DeleteGrid(int _gridId)
    {
        var grid = GetGridById(_gridId);
        gridContext.grids.Remove(grid!);
        gridContext.SaveChanges();
    }
    
    public Grid? SetCoordinateStatus(int _gridId, string _coordinate, SquareStatus _status)
    {
        var grid = GetGridById(_gridId);
        if(grid != null)
        {
            grid.SetSquareStatus(_coordinate, _status);
        }
        return grid;
    }
    public Grid? AddShipToGrid(int _gridId, ShipType _type, int _ShipId)
    {
        var grid = GetGridById(_gridId);
        if(grid != null)
        {
            grid.AddShip(_type, _ShipId);
        }
        return grid;
    }
}