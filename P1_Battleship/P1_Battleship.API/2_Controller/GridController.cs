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
        try{
            var result = gridService.GetAllGrids();
            return Ok(result);
        }
        catch(Exception e)
        {
            return Problem(e.Message);
        }
    }
    [HttpGet("{gridId}")]
    public IActionResult GetGridById(int gridId)
    {
        try{
            var result = gridService.GetGridById(gridId);
            if(result is null) return NotFound();
            return Ok(result);
        }
        catch(Exception e)
        {
            return Problem(e.Message);
        }
    }
    [HttpGet("ShipsInGrid/{gridId}")]
    public IActionResult GetShipsInGrid(int gridId)
    {
        try{
            var result = gridService.GetShipsInGrid(gridId);
            return Ok(result);
        }
        catch(Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost("{width}/{height}")]
    public IActionResult CreateNewGrid(int width, int height)
    {
        try{
            var result = gridService.CreateNewGrid(width, height);
            return Ok(result);
        }
        catch(Exception e)
        {
            return Problem(e.Message);
        }
    }
    
    [HttpPatch("AddShip/{gridId}/{shipId}")]
    public IActionResult AddShipToGrid(int gridId, int shipId)
    {
        try{
            var result = gridService.AddShipToGrid(gridId, shipId);
            return Ok(result);
        }
        catch(Exception e)
        {
            return Problem(e.Message);
        }
    }
    [HttpPatch("ShootAt/{gridId}/{coordinate}")]
    public IActionResult ShootAtCoordinate(int gridId, string coordinate)
    {
        try{
            var result = gridService.ShootAtCoordinate(gridId, coordinate);
            return Ok(result);
        }
        catch(Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpDelete("{Id}")]
    public IActionResult DeleteGrid(int Id)
    {
        try{
            var result = gridService.DeleteGrid(Id);
            if(result is null) return NotFound();
            return Ok(result);
        }
        catch(Exception e)
        {
            return Problem(e.Message);
        }
    }

}