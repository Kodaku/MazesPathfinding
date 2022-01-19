using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject cell;
    public GameObject cellWallEast;
    public GameObject cellWallSouth;
    public GameObject cellWallNorth;
    public GameObject cellWallWest;
    public GameObject distance;
    // Start is called before the first frame update
    void Start()
    {
        IGrid grid = new MyGrid(25,25);
        Sidewinder sidewinder = new Sidewinder();
        sidewinder.On(ref grid);
        Cell start = grid[0, 0];
        Distances distances = start.Distances;
        MaxResult maxResult = distances.Max;
        Cell newStart = maxResult.Cell;
        int distance = maxResult.Distance;
        Distances newDistances = newStart.Distances;
        MaxResult maxGoal = newDistances.Max;
        Cell goal = maxGoal.Cell;
        distance = maxGoal.Distance;
        grid.distances = newDistances.PathToGoal(goal);
        BuildMaze(grid);
    }

    private void BuildMaze(IGrid grid)
    {
        foreach(var row in grid.GetAllRows())
        {
            foreach (var currentCell in row)
            {
                if(currentCell.IsLinked(currentCell.East))
                {
                    Instantiate(cell, new Vector3(currentCell.row, 0.0f, currentCell.column), Quaternion.identity);
                }
                else
                {
                    Instantiate(cellWallEast, new Vector3(currentCell.row, 0.0f, currentCell.column), Quaternion.Euler(0.0f, 90.0f, 0.0f));
                }

                var southCell = currentCell.South ?? currentCell;
                var eastCell = currentCell.East ?? currentCell;
                var northCell = currentCell.North ?? currentCell;
                var westCell = currentCell.West ?? currentCell;

                if (southCell.IsLinked(southCell.South) && eastCell.IsLinked(eastCell.East) && northCell.IsLinked(northCell.North) && westCell.IsLinked(westCell.West))
                {
                    Instantiate(cell, new Vector3(currentCell.row, 0.0f, currentCell.column), Quaternion.identity);
                }
                if(currentCell.IsLinked(currentCell.South))
                {
                    Instantiate(cell, new Vector3(currentCell.row, 0.0f, currentCell.column), Quaternion.identity);
                }
                else
                {
                    Instantiate(cellWallSouth, new Vector3(currentCell.row, 0.0f, currentCell.column), Quaternion.Euler(0.0f, 90.0f, 0.0f));
                }
                var corner = currentCell.IsLinked(currentCell.South) && currentCell.IsLinked(currentCell.East) && currentCell.IsLinked(currentCell.North) && currentCell.IsLinked(currentCell.West)
                            ? " "
                            : "+";
                if(currentCell.row == 0)
                {
                    Instantiate(cellWallSouth, new Vector3(currentCell.row, 0.0f, currentCell.column), Quaternion.Euler(0.0f, -90.0f, 0.0f));
                }
                if(currentCell.column == 0)
                {
                    Instantiate(cellWallEast, new Vector3(currentCell.row, 0.0f, currentCell.column), Quaternion.Euler(0.0f, -90.0f, 0.0f));
                }
            }
        }
        foreach(Cell cell in grid.distances.Cells())
        {
            Instantiate(distance, new Vector3(cell.row, 0.5f, cell.column), Quaternion.identity);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
