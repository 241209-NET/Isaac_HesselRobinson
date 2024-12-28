namespace Battleship.API.Model;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum SquareStatus
{
    UNKNOWN,
    MISS,
    HIT,
    SUNK
}

//Used to easily convert an input coordinate string to a pair of ints
public struct GridSquare
{
    public int x = -1;
    public int y = -1;

    public GridSquare(string _coordinate)
    {
        _coordinate = _coordinate.ToLower();
        x = _coordinate[0] - 97; //a is 97 on the ASCII table
        y = int.Parse(_coordinate.Substring(1,_coordinate.Length - 1));
    }

    public GridSquare(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}


//🟥🟧🟨🟩🟦🟪🟫⬛⬜
public class Grid
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public string[] columns { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public int[] shipIds { get; set; } = {-1,-1,-1,-1,-1};

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
    /// <summary>
    /// Returns TRUE if the indicated square is present on the grid and FALSE if it is out of bounds
    /// </summary>
    /// <param name="_coordinate"></param>
    /// <returns></returns>
    public bool IsSquareOnGrid(string _coordinate)
    {
        return IsSquareOnGrid(new GridSquare(_coordinate));
    }
    /// <summary>
    /// Returns TRUE if the indicated square is present on the grid and FALSE if it is out of bounds
    /// </summary>
    /// <param name="_coordinate"></param>
    /// <returns></returns>
    public bool IsSquareOnGrid(GridSquare _coordinate)
    {
        if(_coordinate.x < width && _coordinate.y < height)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the indicated square to display the indicated status
    /// </summary>
    /// <param name="_coordinate"></param>
    /// <param name="_status"></param>
    public void SetSquareStatus(string _coordinate, SquareStatus _status)
    {
        SetSquareStatus(new GridSquare(_coordinate), _status);
    }
    /// <summary>
    /// Sets the indicated square to display the indicated status
    /// </summary>
    /// <param name="_coordinate"></param>
    /// <param name="_status"></param>
    public void SetSquareStatus(GridSquare _coordinate, SquareStatus _status)
    {
        char displayChar = ' ';
        switch((int)_status)
        {
            case 3:
                displayChar = 'S';
                break;
            case 2:
                displayChar = 'X';
                break;
            case 1:
                displayChar = '-';
                break;
            default:
                displayChar = ' ';
                break;
        }
        string newColumn = "";
        //Builds the new column, since I cannot directly set a char in columns
        for(int i = 0; i < height; i++)
        {
            if(i == _coordinate.y)
            {
                newColumn += displayChar;
            }
            else
            {
                newColumn += columns[_coordinate.x][i];
            }
        }
        columns[_coordinate.x] = newColumn;
    }
}