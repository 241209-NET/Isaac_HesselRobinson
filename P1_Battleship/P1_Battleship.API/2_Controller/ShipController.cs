using Microsoft.AspNetCore.Mvc;
using Battleship.API.Model;
using Battleship.API.Service;

namespace Battleship.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class ShipController : ControllerBase
{
    readonly IShipService shipService;

    public ShipController(IShipService _shipService) => shipService = _shipService;


    [HttpGet]
    public IActionResult GetAllShips()
    {
        var result = shipService.GetAllShips();
        return Ok(result);
    }
    [HttpGet("{Id}")]
    public IActionResult GetShipById(int Id)
    {
        var result = shipService.GetShipById(Id);
        return Ok(result);
    }


    [HttpPost("Carrier/{_firstPosition}/{_secondPosition}/{_thirdPosition}/{_fourthPosition}/{_fifthPosition}")]
    public IActionResult CreateCarrier(string _firstPosition, string _secondPosition, string _thirdPosition, string _fourthPosition, string _fifthPosition)
    {
        string[] positions = {_firstPosition,_secondPosition,_thirdPosition,_fourthPosition,_fifthPosition};
        var result = shipService.CreateNewShip(positions, ShipType.CARRIER);
        return Ok(result);
    }
    [HttpPost("Battleship/{_firstPosition}/{_secondPosition}/{_thirdPosition}/{_fourthPosition}")]
    public IActionResult CreateBattleship(string _firstPosition, string _secondPosition, string _thirdPosition, string _fourthPosition)
    {
        string[] positions = {_firstPosition,_secondPosition,_thirdPosition,_fourthPosition};
        var result = shipService.CreateNewShip(positions, ShipType.BATTLESHIP);
        return Ok(result);
    }
    [HttpPost("Cruiser/{_firstPosition}/{_secondPosition}/{_thirdPosition}")]
    public IActionResult CreateCruiser(string _firstPosition, string _secondPosition, string _thirdPosition)
    {
        string[] positions = {_firstPosition,_secondPosition,_thirdPosition};
        var result = shipService.CreateNewShip(positions, ShipType.CRUISER);
        return Ok(result);
    }
    [HttpPost("Submarine/{_firstPosition}/{_secondPosition}/{_thirdPosition}")]
    public IActionResult CreateSubmarine(string _firstPosition, string _secondPosition, string _thirdPosition)
    {
        string[] positions = {_firstPosition,_secondPosition,_thirdPosition};
        var result = shipService.CreateNewShip(positions, ShipType.SUBMARINE);
        return Ok(result);
    }
    [HttpPost("Destroyer/{_firstPosition}/{_secondPosition}")]
    public IActionResult CreateDestroyer(string _firstPosition, string _secondPosition)
    {
        string[] positions = {_firstPosition,_secondPosition};
        var result = shipService.CreateNewShip(positions, ShipType.DESTROYER);
        return Ok(result);
    }
}