namespace Battleship.API.Model;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Battleship.API.GridException;

public enum SquareStatus
{
    UNKNOWN,
    MISS,
    HIT,
    SUNK
}

public enum Direction
{
    NONCONTIGUOUS = -1,
    UP,
    RIGHT,
    DOWN,
    LEFT
}

//Used to easily convert an input coordinate string to a pair of ints
public struct GridSquare
{
    public int x = -1;
    public int y = -1;

    public GridSquare(string _coordinate)
    {
        //Must subtract 1 extra from each because the display starts at A/1 but the code starts at 0/0
        x = _coordinate.ToLower()[0] - 97; //a is 97 on the ASCII table
        int.TryParse(_coordinate.Substring(1,_coordinate.Length - 1), out y);
        y -= 1;
        if(x < 0 || y < 0)
        {
            throw new CoordinateMalformedException(_coordinate);
        }
    }

    /// <summary>
    /// Returns which side of THIS borders _other. If they do not border, return NONCONTIGUOUS (-1)
    /// </summary>
    /// <param name="_other"></param>
    /// <returns></returns>
    public Direction BorderDirection(GridSquare _other)
    {
        if(x == _other.x && y == _other.y - 1)
            return Direction.UP;
        if(y == _other.y && x == _other.x - 1)
            return Direction.RIGHT;
        if(x == _other.x && y == _other.y + 1)
            return Direction.DOWN;
        if(y == _other.y && x == _other.x + 1)
            return Direction.LEFT;
        return Direction.NONCONTIGUOUS;
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
    /// Returns true if the grid already has a ship of the designated type
    /// </summary>
    /// <param name="_type"></param>
    /// <returns></returns>
    public bool HasShipOfType(ShipType _type)
    {
        return shipIds[(int)_type] > -1;
    }

    /// <summary>
    /// Adds a reference to the specified ship
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_Id"></param>
    /// <param name="_override">If true, will replace an existing ship. If false, will fail if such a ship already exists</param>
    /// <returns>TRUE if ship added, FALSE if failed to add</returns>
    public bool AddShip(ShipType _type, int _Id, bool _override = false)
    {
        if(!HasShipOfType(_type) || _override)
        {
            shipIds[(int)_type] = _Id;
            return true;
        }
        return false;
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