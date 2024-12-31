using Moq;
using Battleship.API.Model;
using Battleship.API.Repository;
using Battleship.API.Service;
using Battleship.API.GridException;

namespace Battleship.TEST;

public class GridServiceTests
{
///////////////////////////////////////////////////////////////////////////////
///GET
///////////////////////////////////////////////////////////////////////////////
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

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GetGridByIdTestValid(int _gridId)
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

        mockGridRepo.Setup(repo => repo.GetGridById(_gridId)).Returns(gridList[_gridId]);
        
        //Act
        var result = gridService.GetGridById(_gridId);
        
        //Assert
        Assert.Equal(gridList[_gridId], result);
    }
    [Theory]
    [InlineData(-2)]
    [InlineData(5)]
    public void GetGridByIdTestInvalid(int _gridId)
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

        mockGridRepo.Setup(repo => repo.GetGridById(_gridId));
        
        //Act
        //var result = gridService.GetGridById(_gridId);
        
        //Assert
        Assert.Throws<GridUnknownException>(() => gridService.GetGridById(_gridId));
    }
///////////////////////////////////////////////////////////////////////////////
///POST
///////////////////////////////////////////////////////////////////////////////
    [Fact]
    public void CreateNewGridCorrect()
    {
        //Arrange
        Mock<IGridRepository> mockGridRepo = new();
        Mock<IShipService> mockShipService = new();
        GridService gridService = new(mockGridRepo.Object, mockShipService.Object);

        int _width = 10;
        int _height = 10;
        Grid expected = new Grid{Id = 0,
                columns = ["          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [-1, -1, -1, -1, -1 ]
        };

        mockGridRepo.Setup(repo => repo.CreateNewGrid(It.IsAny<Grid>())).Returns(new Grid(_width,_height));
        
        //Act
        Grid result = gridService.CreateNewGrid(_width, _height);
        
        //Assert
        Assert.Equivalent(expected, result);
    }
}
