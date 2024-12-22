using Microsoft.EntityFrameworkCore;
using Battleship.API.Model;

namespace Battleship.API.Data;

public partial class ShipContext : DbContext
{
    public ShipContext(){}
    public ShipContext(DbContextOptions<ShipContext> options) : base(options){}

    public virtual DbSet<Ship> ships {get; set;}
}
