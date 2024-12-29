using Microsoft.AspNetCore.Mvc;
using Battleship.API.Model;
using Battleship.API.Service;

namespace Battleship.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class GridController : ControllerBase
{
    readonly IGridService gridService;

    public GridController(IGridService _gridService) => gridService = _gridService;

    [HttpGet]
    public IActionResult GetAllGrids()
    {
        var result = gridService.GetAllGrids();
        return Ok(result);
    }
    [HttpGet("{gridId}")]
    public IActionResult GetGridById(int gridId)
    {
        var result = gridService.GetGridById(gridId);
        if(result is null) return NotFound();
        return Ok(result);
    }
    [HttpGet("ShipsInGrid/{gridId}")]
    public IActionResult GetShipsInGrid(int gridId)
    {
        var result = gridService.GetShipsInGrid(gridId);
        return Ok(result);
    }

    [HttpPost("{width}/{height}")]
    public IActionResult CreateNewGrid(int width, int height)
    {
        var result = gridService.CreateNewGrid(width, height);
        return Ok(result);
    }
    
    [HttpPatch("AddShip/{gridId}/{shipId}")]
    public IActionResult AddShipToGrid(int gridId, int shipId)
    {
        var result = gridService.AddShipToGrid(gridId, shipId);
        return Ok(result);
    }
    [HttpPatch("ShootAt/{gridId}/{coordinate}")]
    public IActionResult ShootAtCoordinate(int gridId, string coordinate)
    {
        var result = gridService.ShootAtCoordinate(gridId, coordinate);
        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public IActionResult DeleteGrid(int Id)
    {
        var result = gridService.DeleteGrid(Id);
        if(result is null) return NotFound();
        return Ok(result);
    }

}