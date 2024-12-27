using Microsoft.EntityFrameworkCore;
using Battleship.API.Model;

namespace Battleship.API.Data;

public partial class GridContext : DbContext
{
    public GridContext(){}
    public GridContext(DbContextOptions<GridContext> options) : base(options){}

    public virtual required DbSet<Grid> grids {get; set;}
}
