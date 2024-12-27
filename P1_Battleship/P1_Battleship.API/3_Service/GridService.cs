using Battleship.API.Model;
using Battleship.API.Repository;

namespace Battleship.API.Service;

public class GridService : IGridService
{
    private readonly IGridRepository gridRepository;

    public GridService(IGridRepository _gridRepository) => gridRepository = _gridRepository;

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
        return gridRepository.GetGridById(_gridId);
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
}