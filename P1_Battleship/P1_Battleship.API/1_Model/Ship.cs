namespace Battleship.API.Model;

public class Ship
{
    public string shipName { get; private set; } = "ship";
    public int size { get; private set; } = 0;
    public string[] positions { get; private set; } = { "0","0","0","0","0" };
    public bool[] hitPoints { get; private set; } = { false, false, false, false, false };

    public Ship (string[] _positions, string _shipName)
    {
        size = _positions.Length;
        for(int i = 0; i < size; i++)
        {
            positions[i] = _positions[i];
            hitPoints[i] = true;
        }
        shipName = _shipName;
    }
}