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

    [HttpPost]
    public IActionResult CreateNewGrid(int _width, int _height)
    {
        var result = gridService.CreateNewGrid(_width, _height);
        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetGridById(int _gridId)
    {
        var result = gridService.GetGridById(_gridId);
        if(result is null) return NotFound();
        return Ok(result);
    }

    [HttpDelete]
    public IActionResult DeleteGrid(int _gridId)
    {
        var result = gridService.DeleteGrid(_gridId);
        if(result is null) return NotFound();
        return Ok(result);
    }
}