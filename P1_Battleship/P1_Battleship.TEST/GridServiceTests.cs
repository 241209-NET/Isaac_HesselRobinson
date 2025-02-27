//reportgenerator -reports:".\JJIMP.Tests\TestResults\51a37d7e-f43b-4c42-b0d8-48b1822a2bbb\coverage.cobertura.xml" -targetdir:"JJIMP.Tests\TestResults\coveragereport" -reporttypes:Html -classfilters:"+JJIMP.API.Service.*"
//dotnet test --collect: "XPlat Code Coverage"

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
    [Fact]
    public void GetGridByIdTestInvalid()
    {
        //Arrange
        int _gridId = 3;
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
        
        //Assert
        Assert.Throws<GridUnknownException>(() => gridService.GetGridById(_gridId));
    }

    [Fact]
    public void GetShipsInGridTest()
    {
        //Arrange
        Mock<IGridRepository> mockGridRepo = new();
        Mock<IShipService> mockShipService = new();
        GridService gridService = new(mockGridRepo.Object, mockShipService.Object);

        Grid newGrid = 
            new Grid{Id = 1,
                columns = ["          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, 2, 3, -1, -1 ]
            };
        List<Ship> expected = [
            new Ship{ Id = 1,
                shipName = "Destroyer",
                size = 2,
                positions = ["A1", "A2"],
                hitPoints = [true,true],
                type = 0
            },
            new Ship{ Id = 2,
                shipName = "Submarine",
                size = 3,
                positions = ["D3", "E3", "F3"],
                hitPoints = [true,true,true],
                type = 0
            },
            new Ship{ Id = 3,
                shipName = "Cruiser",
                size = 3,
                positions = ["J6", "J7", "J8"],
                hitPoints = [true,true,true],
                type = 0
            }
        ];

        mockGridRepo.Setup(repo => repo.GetGridById(1)).Returns(newGrid);
        mockShipService.Setup(service => service.GetShipById(1)).Returns(expected[0]);
        mockShipService.Setup(service => service.GetShipById(2)).Returns(expected[1]);
        mockShipService.Setup(service => service.GetShipById(3)).Returns(expected[2]);
        
        //Act
        var result = gridService.GetShipsInGrid(1).ToList();
        
        //Assert
        Assert.Equal(expected, result);
    }

///////////////////////////////////////////////////////////////////////////////
///POST
///////////////////////////////////////////////////////////////////////////////
    [Fact]
    public void CreateNewGridTestCorrect()
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
    [Fact]
    public void CreateNewGridTestTooSmall()
    {
        //Arrange
        Mock<IGridRepository> mockGridRepo = new();
        Mock<IShipService> mockShipService = new();
        GridService gridService = new(mockGridRepo.Object, mockShipService.Object);

        int _width = 0;
        int _height = 0;
        
        //Assert
        Assert.Throws<GridTooSmallException>(() => gridService.CreateNewGrid(_width, _height));
    }

///////////////////////////////////////////////////////////////////////////////
///PATCH
///////////////////////////////////////////////////////////////////////////////
    [Fact]
    public void AddShipToGridTestValid()
    {
        //Arrange
        Mock<IGridRepository> mockGridRepo = new();
        Mock<IShipService> mockShipService = new();
        GridService gridService = new(mockGridRepo.Object, mockShipService.Object);

        Grid newGrid = 
            new Grid{Id = 1,
                columns = ["          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, 2, -1, -1, -1 ]
            };
        Grid expected = 
            new Grid{Id = 1,
                columns = ["          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, 2, 3, -1, -1 ]
            };
        List<Ship> shiplist = [
            new Ship{ Id = 1,
                shipName = "Destroyer",
                size = 2,
                positions = ["A1", "A2"],
                hitPoints = [true,true],
                type = ShipType.DESTROYER
            },
            new Ship{ Id = 2,
                shipName = "Submarine",
                size = 3,
                positions = ["D3", "E3", "F3"],
                hitPoints = [true,true,true],
                type = ShipType.SUBMARINE
            },
            new Ship{ Id = 3,
                shipName = "Cruiser",
                size = 3,
                positions = ["J6", "J7", "J8"],
                hitPoints = [true,true,true],
                type = ShipType.CRUISER
            }
        ];

        mockGridRepo.Setup(repo => repo.GetGridById(1)).Returns(newGrid);
        mockGridRepo.Setup(repo => repo.AddShipToGrid(1,ShipType.CRUISER,3)).Returns(expected);
        mockShipService.Setup(service => service.GetShipById(1)).Returns(shiplist[0]);
        mockShipService.Setup(service => service.GetShipById(2)).Returns(shiplist[1]);
        mockShipService.Setup(service => service.GetShipById(3)).Returns(shiplist[2]);

        //Act
        var result = gridService.AddShipToGrid(1,3);
        
        //Assert
        Assert.Equal(expected, result);
    }
    [Fact]
    public void AddShipToGridTestGridHasShipTypeException()
    {
        //Arrange
        Mock<IGridRepository> mockGridRepo = new();
        Mock<IShipService> mockShipService = new();
        GridService gridService = new(mockGridRepo.Object, mockShipService.Object);

        Grid newGrid = 
            new Grid{Id = 1,
                columns = ["          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, 2, -1, -1, -1 ]
            };
        Grid expected = 
            new Grid{Id = 1,
                columns = ["          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, 2, 3, -1, -1 ]
            };
        List<Ship> shiplist = [
            new Ship{ Id = 1,
                shipName = "Destroyer",
                size = 2,
                positions = ["A1", "A2"],
                hitPoints = [true,true],
                type = ShipType.DESTROYER
            },
            new Ship{ Id = 2,
                shipName = "Submarine",
                size = 3,
                positions = ["D3", "E3", "F3"],
                hitPoints = [true,true,true],
                type = ShipType.SUBMARINE
            },
            new Ship{ Id = 3,
                shipName = "Submarine",
                size = 3,
                positions = ["J6", "J7", "J8"],
                hitPoints = [true,true,true],
                type = ShipType.SUBMARINE
            }
        ];

        mockGridRepo.Setup(repo => repo.GetGridById(1)).Returns(newGrid);
        mockGridRepo.Setup(repo => repo.AddShipToGrid(1,ShipType.CRUISER,3)).Returns(expected);
        mockShipService.Setup(service => service.GetShipById(1)).Returns(shiplist[0]);
        mockShipService.Setup(service => service.GetShipById(2)).Returns(shiplist[1]);
        mockShipService.Setup(service => service.GetShipById(3)).Returns(shiplist[2]);
        
        //Assert
        Assert.Throws<GridHasShipTypeException>(() => gridService.AddShipToGrid(1,3));
    }
    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    public void AddShipToGridTestGridCoordinateOutOfBoundsException(int _shipIndex)
    {
        //Arrange
        Mock<IGridRepository> mockGridRepo = new();
        Mock<IShipService> mockShipService = new();
        GridService gridService = new(mockGridRepo.Object, mockShipService.Object);

        Grid newGrid = 
            new Grid{Id = 1,
                columns = ["          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, -1, -1, -1, -1 ]
            };
        Grid expected = 
            new Grid{Id = 1,
                columns = ["          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, 2, -1, -1, -1 ]
            };
        List<Ship> shiplist = [
            new Ship{ Id = 1,
                shipName = "Destroyer",
                size = 2,
                positions = ["A1", "A2"],
                hitPoints = [true,true],
                type = ShipType.DESTROYER
            },
            new Ship{ Id = 2,
                shipName = "Submarine",
                size = 3,
                positions = ["C11", "C10", "C9"],
                hitPoints = [true,true,true],
                type = ShipType.SUBMARINE
            },
            new Ship{ Id = 2,
                shipName = "Submarine",
                size = 3,
                positions = ["H9", "H10", "H11"],
                hitPoints = [true,true,true],
                type = ShipType.SUBMARINE
            }
        ];

        mockGridRepo.Setup(repo => repo.GetGridById(1)).Returns(newGrid);
        mockGridRepo.Setup(repo => repo.AddShipToGrid(1,ShipType.CRUISER,3)).Returns(expected);
        mockShipService.Setup(service => service.GetShipById(1)).Returns(shiplist[0]);
        mockShipService.Setup(service => service.GetShipById(2)).Returns(shiplist[1]);
        mockShipService.Setup(service => service.GetShipById(3)).Returns(shiplist[2]);
        
        //Assert
        Assert.Throws<CoordinateOutOfBoundsException>(() => gridService.AddShipToGrid(1,_shipIndex));
    }
        [Fact]
    public void AddShipToGridTestGridHasShipAtPositionException()
    {
        //Arrange
        Mock<IGridRepository> mockGridRepo = new();
        Mock<IShipService> mockShipService = new();
        GridService gridService = new(mockGridRepo.Object, mockShipService.Object);

        Grid newGrid = 
            new Grid{Id = 1,
                columns = ["          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, 2, -1, -1, -1 ]
            };
        Grid expected = 
            new Grid{Id = 1,
                columns = ["          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, 2, 3, -1, -1 ]
            };
        List<Ship> shiplist = [
            new Ship{ Id = 1,
                shipName = "Destroyer",
                size = 2,
                positions = ["A1", "A2"],
                hitPoints = [true,true],
                type = ShipType.DESTROYER
            },
            new Ship{ Id = 2,
                shipName = "Submarine",
                size = 3,
                positions = ["D3", "E3", "F3"],
                hitPoints = [true,true,true],
                type = ShipType.SUBMARINE
            },
            new Ship{ Id = 3,
                shipName = "Cruiser",
                size = 3,
                positions = ["D3", "D4", "D5"],
                hitPoints = [true,true,true],
                type = ShipType.CRUISER
            }
        ];

        mockGridRepo.Setup(repo => repo.GetGridById(1)).Returns(newGrid);
        mockGridRepo.Setup(repo => repo.AddShipToGrid(1,ShipType.CRUISER,3)).Returns(expected);
        mockShipService.Setup(service => service.GetShipById(1)).Returns(shiplist[0]);
        mockShipService.Setup(service => service.GetShipById(2)).Returns(shiplist[1]);
        mockShipService.Setup(service => service.GetShipById(3)).Returns(shiplist[2]);
        
        //Assert
        Assert.Throws<GridHasShipAtPositionException>(() => gridService.AddShipToGrid(1,3));
    }
///////////////////////////////////////////////////////////////////////////////
///DELETE
///////////////////////////////////////////////////////////////////////////////
    [Fact]
    public void DeleteGridTestValid()
    {
        //Arrange
        Mock<IGridRepository> mockGridRepo = new();
        Mock<IShipService> mockShipService = new();
        GridService gridService = new(mockGridRepo.Object, mockShipService.Object);

        Grid expected =
            new Grid{Id = 1,
                columns = ["          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [-1, -1, -1, -1, -1 ]
            };

        mockGridRepo.Setup(repo => repo.GetGridById(1)).Returns(expected);
        mockGridRepo.Setup(repo => repo.DeleteGrid(1)).Returns(expected);
        
        //Act
        var result = gridService.DeleteGrid(1);
        
        //Assert
        Assert.Equal(expected, result);
    }
    [Fact]
    public void DeleteGridTestInvalid()
    {
        //Arrange
        Mock<IGridRepository> mockGridRepo = new();
        Mock<IShipService> mockShipService = new();
        GridService gridService = new(mockGridRepo.Object, mockShipService.Object);
        
        //Assert
        Assert.Throws<GridUnknownException>(() => gridService.DeleteGrid(2));
    }

///////////////////////////////////////////////////////////////////////////////
///UTIL
///////////////////////////////////////////////////////////////////////////////
    [Theory]
    [InlineData("J10", "", -1)]
    [InlineData("A1","A1", 1)]
    public void AnyShipInGridAtPositionTest(string _positionToCheck, string _expectedPosition, int _expectedId)
    {
        //Arrange
        Mock<IGridRepository> mockGridRepo = new();
        Mock<IShipService> mockShipService = new();
        GridService gridService = new(mockGridRepo.Object, mockShipService.Object);

        Grid newGrid = 
            new Grid{Id = 1,
                columns = ["          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, 2, 3, -1, -1 ]
            };
        List<Ship> shiplist = [
            new Ship{ Id = 1,
                shipName = "Destroyer",
                size = 2,
                positions = ["A1", "A2"],
                hitPoints = [true,true],
                type = 0
            },
            new Ship{ Id = 2,
                shipName = "Submarine",
                size = 3,
                positions = ["D3", "E3", "F3"],
                hitPoints = [true,true,true],
                type = 0
            },
            new Ship{ Id = 3,
                shipName = "Cruiser",
                size = 3,
                positions = ["J6", "J7", "J8"],
                hitPoints = [true,true,true],
                type = 0
            }
        ];
        OverlappingShipResult expected = new OverlappingShipResult(_expectedPosition,_expectedId);

        mockGridRepo.Setup(repo => repo.GetGridById(1)).Returns(newGrid);
        mockShipService.Setup(service => service.GetShipById(1)).Returns(shiplist[0]);
        mockShipService.Setup(service => service.GetShipById(2)).Returns(shiplist[1]);
        mockShipService.Setup(service => service.GetShipById(3)).Returns(shiplist[2]);

        //Act
        var result = gridService.AnyShipInGridAtPosition(1,_positionToCheck);
        
        //Assert
        Assert.Throws<GridUnknownException>(() => gridService.DeleteGrid(2));
        Assert.Equal(expected, result);
    }
    [Theory]
    [InlineData(2, "", -1)]
    [InlineData(3,"A1", 1)]
    public void AnyShipInGridOverlapsTest(int _shipToAddId, string _expectedPosition, int _expectedId)
    {
        //Arrange
        Mock<IGridRepository> mockGridRepo = new();
        Mock<IShipService> mockShipService = new();
        GridService gridService = new(mockGridRepo.Object, mockShipService.Object);

        Grid newGrid = 
            new Grid{Id = 1,
                columns = ["          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, -1, -1, -1, -1 ]
            };
        List<Ship> shiplist = [
            new Ship{ Id = 1,
                shipName = "Destroyer",
                size = 2,
                positions = ["A1", "A2"],
                hitPoints = [true,true],
                type = 0
            },
            new Ship{ Id = 2,
                shipName = "Submarine",
                size = 3,
                positions = ["J6", "J7", "J8"],
                hitPoints = [true,true,true],
                type = 0
            },
            new Ship{ Id = 3,
                shipName = "Cruiser",
                size = 3,
                positions = ["A1", "B1", "C1"],
                hitPoints = [true,true,true],
                type = 0
            }
        ];
        OverlappingShipResult expected = new OverlappingShipResult(_expectedPosition,_expectedId);

        mockGridRepo.Setup(repo => repo.GetGridById(1)).Returns(newGrid);
        mockShipService.Setup(service => service.GetShipById(1)).Returns(shiplist[0]);
        mockShipService.Setup(service => service.GetShipById(2)).Returns(shiplist[1]);
        mockShipService.Setup(service => service.GetShipById(3)).Returns(shiplist[2]);

        //Act
        var result = gridService.AnyShipInGridOverlaps(1,_shipToAddId);
        
        //Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1,0)]
    [InlineData(2,1)]
    public void MarkShipIfSunkTest(int _shipId, int _expectedGridId)
    {
        //Arrange
        Mock<IGridRepository> mockGridRepo = new();
        Mock<IShipService> mockShipService = new();
        GridService gridService = new(mockGridRepo.Object, mockShipService.Object);

        Grid newGrid = 
            new Grid{Id = 1,
                columns = ["X         ", "X         ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, 2, 3, -1, -1 ]
            };
        List<Grid> expected = [
            new Grid{Id = 1,
                columns = ["S         ", "S         ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, 2, 3, -1, -1 ]
            },
            new Grid{Id = 1,
                columns = ["X         ", "X         ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, 2, 3, -1, -1 ]
            },
        ];
        List<Ship> shiplist = [
            new Ship{ Id = 1,
                shipName = "Destroyer",
                size = 2,
                positions = ["A1", "A2"],
                hitPoints = [false,false],
                type = 0
            },
            new Ship{ Id = 2,
                shipName = "Submarine",
                size = 3,
                positions = ["D3", "E3", "F3"],
                hitPoints = [true,true,true],
                type = 0
            },
            new Ship{ Id = 3,
                shipName = "Cruiser",
                size = 3,
                positions = ["J6", "J7", "J8"],
                hitPoints = [true,true,true],
                type = 0
            }
        ];

        mockGridRepo.Setup(repo => repo.GetGridById(1)).Returns(newGrid);
        mockGridRepo.Setup(repo => repo.SetCoordinateStatus(1, "A1", SquareStatus.SUNK)).Returns(new Grid{Id = 1,
                columns = ["S         ", "X         ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, 2, 3, -1, -1 ]
            });
        mockGridRepo.Setup(repo => repo.SetCoordinateStatus(1, "A2", SquareStatus.SUNK)).Returns(new Grid{Id = 1,
                columns = ["S         ", "S         ", "          ", "          ", "          ", "          ", "          ", "          ", "          ", "          "],
                width = 10,
                height = 10,
                shipIds = [1, 2, 3, -1, -1 ]
            });
        mockShipService.Setup(service => service.GetShipById(1)).Returns(shiplist[0]);
        mockShipService.Setup(service => service.GetShipById(2)).Returns(shiplist[1]);
        mockShipService.Setup(service => service.GetShipById(3)).Returns(shiplist[2]);

        //Act
        var result = gridService.MarkShipIfSunk(1,_shipId);
        
        //Assert
        Assert.Equivalent(expected[_expectedGridId], result);
    }
}
