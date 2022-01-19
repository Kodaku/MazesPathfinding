using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree
{
    public void On(ref IGrid grid)
    {
        foreach (var cell in grid.GetAllCells())
        {
            var neighbors = new List<Cell>();
            if (cell.North != null) neighbors.Add(cell.North);
            if (cell.East != null) neighbors.Add(cell.East);

            int index = Random.Range(0, neighbors.Count);
            if (neighbors.Count > 0)
            {
                Cell neighbor = neighbors[index];
                cell.Link(neighbor);
            }
        }
    }
}
