using Moq;
using Battleship.API.Model;
using Battleship.API.Repository;
using Battleship.API.Service;

namespace Battleship.TEST;

public class UnitTest1
{
    [Fact]
    public void GetAllGridsTest()
    {
        //Arrange
        Mock<IGridRepository> mockGridRepo = new();
        Mock<IShipService> mockShipService = new();
        GridService gridService = new(mockGridRepo.Object, mockShipService.Object);

        List<Grid> gridList = [
            new Grid{Id = 1,
                columns = ["          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [-1, -1, -1, -1, -1 ]
            },
            new Grid{Id = 2,
                columns = ["  X       ", "   X      ", "     ---  ", "          ", "          "],
                width = 5,
                height = 10,
                shipIds = [2, -1, -1, -1, 3 ]
            }
        ];

        mockGridRepo.Setup(repo => repo.GetAllGrids()).Returns(gridList);
        
        //Act
        var result = gridService.GetAllGrids().ToList();
        
        //Assert
        Assert.Equal(gridList, result);
    }
}
