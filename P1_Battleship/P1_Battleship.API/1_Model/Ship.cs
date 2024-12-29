namespace Battleship.API.Model;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum ShipType
{
    DESTROYER,
    SUBMARINE,
    CRUISER,
    BATTLESHIP,
    CARRIER
}

public class Ship
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string shipName { get; set; } = "ship";
    public int size { get; set; } = 0;
    public string[] positions { get; set; } = { "0","0","0","0","0" };
    public bool[] hitPoints { get; set; } = { true, true, true, true, true };

    public Ship () //never used, but I dislike underlines
    {
        shipName = "Small Ship";
        size = 1;
        positions = new string[size];
        hitPoints = new bool[size];
    }
    public Ship (string[] _positions, string _shipName)
    {
        shipName = _shipName;
        size = _positions.Length;
        positions = new string[size];
        hitPoints = new bool[size];
        for(int i = 0; i < size; i++)
        {
            positions[i] = _positions[i];
            hitPoints[i] = true;
        }
    }

    /// <summary>
    /// Evaluates whether any of the ship's positions have not yet been shot
    /// </summary>
    /// <returns>True if the ship is still alive<br />False if the ship is sunk</returns>
    public bool IsAlive()
    {
        foreach(bool point in hitPoints)
        {
            if(point)
            {
                return true;
            }
        }
        //If no hitpoints remain, you're sunk
        return false;
    }
}