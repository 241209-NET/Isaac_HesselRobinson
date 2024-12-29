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


    [HttpPost("Carrier")]
    public IActionResult CreateCarrier(string _firstPosition, string _secondPosition, string _thirdPosition, string _fourthPosition, string _fifthPosition)
    {
        string[] positions = {_firstPosition,_secondPosition,_thirdPosition,_fourthPosition,_fifthPosition};
        var result = shipService.CreateNewShip(positions, ShipType.CARRIER);
        return Ok(result);
    }

    [HttpPost("Destroyer")]
    public IActionResult CreateDestroyer(string _firstPosition, string _secondPosition)
    {
        string[] positions = {_firstPosition,_secondPosition};
        var result = shipService.CreateNewShip(positions, ShipType.DESTROYER);
        return Ok(result);
    }
}