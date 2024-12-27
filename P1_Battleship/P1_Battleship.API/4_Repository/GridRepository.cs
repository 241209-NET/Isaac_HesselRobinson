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

}