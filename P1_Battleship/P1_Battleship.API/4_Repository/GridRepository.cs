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
        return _newGrid;
    }

}