using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidewinder
{
    public void On(ref IGrid grid)
    {
        foreach(var row in grid.GetAllRows())
        {
            List<Cell> run = new List<Cell>();
            foreach(var cell in row)
            {
                run.Add(cell);
                bool atEasternBoundary = (cell.East == null);
                bool atNorthernBoundary = (cell.North == null);

                bool shouldCloseOut = atEasternBoundary || (!atNorthernBoundary && Random.Range(0, 2) == 0);

                if(shouldCloseOut)
                {
                    Cell member = run[Random.Range(0, run.Count)];
                    if(member.North != null)
                        member.Link(member.North);
                    run.Clear();
                }
                else
                    cell.Link(cell.East);
            }
        }
    }
}
