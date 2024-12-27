namespace Battleship.API.Model;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//🟥🟧🟨🟩🟦🟪🟫⬛⬜
public class Grid
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public string[] columns { get; set; }
    public int width { get; set; }
    public int height { get; set; }

    public Grid() //never used, but I don't like underlines
    {
        width = 1;
        height = 1;
        columns = new string[width];
        for(int i = 0; i < width; i++)
        {
            columns[i] = "";
            for(int j = 0; j < height; j++)
            {
                columns[i] += ' ';
            }
        }
    }
    public Grid(int _width, int _height)
    {
        width = _width;
        height = _height;
        columns = new string[width];
        for(int i = 0; i < width; i++)
        {
            columns[i] = "";
            for(int j = 0; j < height; j++)
            {
                columns[i] += ' ';
            }
        }
    }
}