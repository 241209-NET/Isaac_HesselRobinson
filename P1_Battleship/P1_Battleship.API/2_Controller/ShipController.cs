using Microsoft.AspNetCore.Mvc;
using Battleship.API.Model;
using Battleship.API.Service;

namespace Battleship.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class ShipController : ControllerBase
{
    readonly ShipService shipService;

    public ShipController(ShipService _shipService) => shipService = _shipService;


    [HttpGet]
    public IActionResult GetAllShips()
    {
        var result = shipService.GetAllShips();
        return Ok(result);
    }


    [HttpPost]
    public IActionResult CreateNewShip(Ship _newShip)
    {
        var result = shipService.CreateNewShip(_newShip);
        return Ok(result);
    }
}