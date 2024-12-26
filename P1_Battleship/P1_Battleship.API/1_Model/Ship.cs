namespace Battleship.API.Model;

using System.ComponentModel.DataAnnotations;

public class Ship
{
    [Key]
    public string shipName { get; set; } = "ship";
    public int size { get; set; } = 0;
    public string[] positions { get; set; } = { "0","0","0","0","0" };
    public bool[] hitPoints { get; set; } = { false, false, false, false, false };

   /* public Ship (string[] _positions, string _shipName)
    {
        size = _positions.Length;
        for(int i = 0; i < size; i++)
        {
            positions[i] = _positions[i];
            hitPoints[i] = true;
        }
        shipName = _shipName;
    }*/
}