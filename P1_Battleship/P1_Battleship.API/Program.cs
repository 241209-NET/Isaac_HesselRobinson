//dotnet run --project "C:\Users\Isaac\Desktop\Isaac_HesselRobinson\P1_Battleship\P1_Battleship.API"
//Server=localhost;User Id=sa;Password=NotPassword@123;TrustServerCertificate=true;
using Microsoft.EntityFrameworkCore;
using Battleship.API.Data;
using Battleship.API.Repository;
using Battleship.API.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShipContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Battleship")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IShipService,ShipService>();
builder.Services.AddScoped<IShipRepository, ShipRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>{ options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; });

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();