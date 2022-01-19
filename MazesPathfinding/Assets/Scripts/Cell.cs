using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Cell : IEquatable<Cell>
{
    public int row { get; set; } = -1;
    public int column { get; set; } = -1;
    protected Dictionary<Cell, bool> links;
    public Cell North { get; set; }
    public Cell South { get; set; }
    public Cell East { get; set; }
    public Cell West { get; set; }

    public Cell(int _row, int _column)
    {
        Initialize(_row, _column);
    }

    /// <summary>All cells linked to this cell.</summary>
    public List<Cell> Links
    {
        get
        {
            return links.Keys.ToList();
        }
    }

    protected void Initialize(int _row, int _column)
    {
        row = _row;
        column = _column;
        links = new Dictionary<Cell, bool>(); 
    }

    public bool IsLinked(Cell cell)
    {
        if (cell == null) return false;
        return links.ContainsKey(cell);
    }

    public Cell Link(Cell cell, bool bidirectional = true)
    {
        if (!links.ContainsKey(cell))
            links.Add(cell, true);

        if (bidirectional)
            cell.Link(this, false);

        return this;
    }

    public Cell UnLink(Cell cell, bool bidirectional = true)
    {
        if (links.ContainsKey(cell))
            links.Remove(cell);
                        
        if (bidirectional)
            cell.UnLink(this, false);

        return this;
    }

    public virtual Distances Distances
    {
        get
        {
            Distances distances = new Distances(this);
            List<Cell> frontier = new List<Cell>();
            frontier.Add(this);
            
            while(frontier.Any())
            {
                List<Cell> newFrontier = new List<Cell>();

                foreach(var cell in frontier)
                {
                    foreach(var linkedCell in cell.Links)
                    {
                        if(!distances.ContainsKey(linkedCell))
                        {
                            distances[linkedCell] = distances[cell] + 1;
                            newFrontier.Add(linkedCell);
                        }
                    }
                }
                frontier = newFrontier;
            }
            return distances;
        }
    }

    public List<Cell> Neighbors
    {
        get
        {
            List<Cell> list = new List<Cell>();

            if (North != null)
                list.Add(North);
            if (South != null)
                list.Add(South);
            if (East != null)
                list.Add(East);
            if (West != null)
                list.Add(West);

            return list;
        }
    }

    #region ToString() implementation

    public override string ToString()
    {
        return $"({this.row},{this.column})";
    }

    public string ToStringDistance()
    {
        return $"({this.row},{this.column})";
    }

    #endregion

    #region IEquatable and other such things...
    public bool Equals(Cell other)
    {
        if (other == null) return false;
        return (row == other.row && column == other.column);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Cell);
    }

    public override int GetHashCode()
    {
        return $"{row}_{column}".GetHashCode();
    }

    #endregion

}
