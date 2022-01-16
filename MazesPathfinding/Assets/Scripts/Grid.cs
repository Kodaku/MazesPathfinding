using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int m_rows;
    private int m_columns;
    private List<List<Cell>> m_grid = new List<List<Cell>>();

    public Grid(int rows, int columns)
    {
        m_rows = rows;
        m_columns = columns;
        PrepareGrid();
    }

    public int rows
    {
        get { return m_rows; }
        set { m_rows = value; }
    }

    public int columns
    {
        get { return m_columns; }
        set { m_columns = value; }
    }

    public List<List<Cell>> grid
    {
        get { return m_grid; }
        set { m_grid = value; }
    }

    private void PrepareGrid()
    {
        for(int i = 0; i < m_rows; i++)
        {
            List<Cell> grid_row = new List<Cell>();
            for(int j = 0; j < m_columns; j++)
            {
                Cell cell = new Cell(i, j);
                grid_row.Add(cell);
            }
            m_grid.Add(grid_row);
        }
    }

    private void ConfigureCells()
    {
        for(int i = 0; i < m_grid.Count; i++)
        {
            List<Cell> grid_row = m_grid[i];
            List<Cell> northCells = null;
            List<Cell> southCells = null;
            if((i - 1) >= 0)
            {
                northCells = m_grid[i - 1];
            }
            if((i + 1) < m_rows)
            {
                southCells = m_grid[i + 1];
            }
            for(int j = 0; j < grid_row.Count; j++)
            {
                Cell cell = grid_row[j];
                int row = cell.row;
                int column = cell.column;
                cell.north = northCells != null ? northCells[j] : null;
                cell.south = southCells != null ? southCells[j] : null;
                if((j + 1) < grid_row.Count)
                    cell.east = grid_row[j + 1];
                if((j - 1) >= 0)
                    cell.west = grid_row[j - 1];
            }
        }
    }
}
