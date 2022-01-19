using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : IGrid
{
    protected Cell[,] grid;

    public int Rows { get; set; } = 0;

    public int Columns { get; set; } = 0;

    public virtual int Size { get { return Rows * Columns; } }
    
    public Distances distances { get; set; } = null;

    public MyGrid(int _rows, int _columns, bool useBaseInitializer = true)
    {
        if (useBaseInitializer)
        { 
            Initialize(_rows, _columns);
        }
    }

    protected virtual void Initialize(int _rows, int _columns)
    {
        Rows = _rows;
        Columns = _columns;

        PrepareGrid();
        ConfigureCells();
    }

    protected virtual void PrepareGrid()
    {
        grid = new Cell[Rows, Columns];
        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Rows; j++)
            {
                grid[i, j] = new Cell(i, j);
            }
        }
    }

    protected virtual void ConfigureCells()
    {
        foreach (var cell in grid)
        {

            cell.North = this[cell.row - 1, cell.column];
            cell.South = this[cell.row + 1, cell.column];
            cell.West = this[cell.row, cell.column - 1];
            cell.East = this[cell.row, cell.column + 1];
        }
    }

    protected IEnumerable<Cell> GetRow(int row)
    {
        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Rows; j++)
            {
                if (i == row)
                    yield return this[i, j];
            }
        }
    }

    public List<List<Cell>> GetAllRows()
    {
        var results = new List<List<Cell>>();
        for (var i = 0; i < Rows; i++)
        {
            var innerList = new List<Cell>();
            for (var j = 0; j < Rows; j++)
            {
                innerList.Add(this[i, j]);
            }
            results.Add(innerList);
        }
        return results;
    }

    public IEnumerable<Cell> GetAllCells()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Columns; j++)
            {
                yield return this[i, j];
            }
        }
    }

    public virtual Cell GetRandomCell
    {
        get
        {
            var i = Random.Range(0, Rows - 1);
            var j = Random.Range(0, Columns - 1);
            return this[i, j];
        }
    }

    #region To Display / Debug Grid
    public virtual string ContentsOf(Cell cell)
    {
        return "   ";
    }

    public virtual string ToString(bool displayGridCoordinates)
    {
        return string.Empty;
    }

    public override string ToString()
    {
        return ToString(false);
    }

    public string ToDebug()
    {
        var output = string.Empty;

        return output;
    }

    #endregion

    #region Helper Methods  

    /// <summary>IEnumerator for 2-D cell array.</summary>
    public IEnumerator<Cell> GetEnumerator()
    {
        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Rows; j++)
            {
                yield return grid[i, j];
            }
        }
    }

    /// <summary>2-D Array Accessor method.</summary>
    public Cell this[int row, int column]
    {
        get
        {
            if (row < 0 || row > Rows - 1) return null;
            if (column < 0 || column > Columns - 1) return null;
            return grid[row, column];
        }
        set
        {
            grid[row, column] = value;
        }
    }

    #endregion
}
