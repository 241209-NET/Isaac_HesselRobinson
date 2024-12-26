namespace Battleship.API.Model;
//🟥 🟧 🟨 🟩 🟦 🟪 🟫 ⬛ ⬜
public class Grid
{
    public static int gridCount { get; set; } = 0;
    public required int ID { get; set; }
    public string[,] squares { get; set; }

    public Grid(int _width, int _height)
    {
        squares = new string[_width,_height];
        for(int i = 0; i < _width; i++)
        {
            for(int j = 0; j < _height; j++)
            {
                squares[i,j] = "🟦";
            }
        }
    }
}