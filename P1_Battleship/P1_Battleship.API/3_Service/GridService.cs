using Battleship.API.Model;
using Battleship.API.Repository;

namespace Battleship.API.Service;

public class GridService : IGridService
{
    private readonly IGridRepository shipRepository;

    public GridService(IGridRepository _shipRepository) => shipRepository = _shipRepository;

    public Grid CreateNewGrid(int _width, int _height)
    {
        Grid newGrid = new Grid(_width, _height) { ID = Grid.gridCount++ };
        return CreateNewGrid(newGrid);
    }
    public Grid CreateNewGrid(Grid _newGrid)
    {
        return shipRepository.CreateNewGrid(_newGrid);
    }
}