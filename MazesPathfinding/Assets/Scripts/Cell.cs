using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private int m_row;
    private int m_column;
    private Dictionary<Cell, bool> m_links = new Dictionary<Cell, bool>();
    private Cell m_north = null;
    private Cell m_south = null;
    private Cell m_east = null;
    private Cell m_west = null;

    public Cell(int row, int column)
    {
        m_row = row;
        m_column = column;
    }
    
    public int row
    {
        get { return m_row; } 
        set { m_row = value; }
    }

    public int column
    {
        get { return m_column; }
        set { m_column = value; }
    }

    public Cell north
    {
        get { return m_north; }
        set { m_north = value; }
    }

    public Cell south
    {
        get { return m_south; }
        set { m_south = value; }
    }

    public Cell east
    {
        get { return m_east; }
        set { m_east = value; }
    }

    public Cell west
    {
        get { return m_west; }
        set { m_west = value; }
    }

    public List<Cell> links
    {
        get { return new List<Cell>(m_links.Keys); }
        set { links = value; }
    }

    public bool Linked(Cell cell)
    {
        return m_links.ContainsKey(cell);
    }

    public void Link(Cell cell, bool bidi = true)
    {
        m_links[cell] = true;
        if(bidi)
        {
            cell.Link(this, false);
        }
    }

    public void Unlink(Cell cell, bool bidi = true)
    {
        if(cell.Linked(this))
        {
            m_links.Remove(cell);
            if(bidi)
            {
                cell.Unlink(this, false);
            }
        }
    }

    public List<Cell> Neighbours()
    {
        List<Cell> neigh_list = new List<Cell>();

        if(m_north != null)
        {
            neigh_list.Add(m_north);
        }
        if(m_south != null)
        {
            neigh_list.Add(m_south);
        }
        if(m_east != null)
        {
            neigh_list.Add(m_east);
        }
        if(m_west != null)
        {
            neigh_list.Add(m_west);
        }

        return neigh_list;
    }

    public override bool Equals(object obj)
    {   
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        Cell cell = (Cell)obj;
        if(cell.row == m_row && cell.column == m_column)
        {
            return true;
        }
        return true;
    }
    
    // override object.GetHashCode
    public override int GetHashCode()
    {
        return System.Tuple.Create(m_row, m_column).GetHashCode();
    }

}
