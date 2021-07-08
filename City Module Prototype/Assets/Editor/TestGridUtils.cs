using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class TestGridUtils
{

    [Test]
    public void BuildArray_Test()
    {
        var grid = GridUtils.BuildArray(10, 5);
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        Assert.AreEqual(10, rows);
        Assert.AreEqual(5, cols);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Assert.IsTrue(grid[row, col].GetType() == typeof(Cell));
            }
        }
    }

    [Test]
    public void GetNearbyCells_Test()
    {
        int rows = 3;
        int cols = 3;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);
        List<Cell> neighbours;
        Cell originCell = gridArray[1, 1];
        Direction direction;

        direction = new North_NorthEast(originCell);
        neighbours = direction.GetNearbyCells(originCell, gridArray);
        Assert.AreEqual(2, neighbours.Count);
        Assert.IsTrue(Search(neighbours, new Cell(0, 1)));
        Assert.IsTrue(Search(neighbours, new Cell(0, 2)));


        direction = new East_NorthEast(originCell);
        neighbours = direction.GetNearbyCells(originCell, gridArray);
        Assert.AreEqual(2, neighbours.Count);
        Assert.IsTrue(Search(neighbours, new Cell(0, 2)));
        Assert.IsTrue(Search(neighbours, new Cell(1, 2)));


        direction = new East_SouthEast(originCell);
        neighbours = direction.GetNearbyCells(originCell, gridArray);
        Assert.AreEqual(2, neighbours.Count);
        Assert.IsTrue(Search(neighbours, new Cell(1, 2)));
        Assert.IsTrue(Search(neighbours, new Cell(2, 2)));


        direction = new South_SouthEast(originCell);
        neighbours = direction.GetNearbyCells(originCell, gridArray);
        Assert.AreEqual(2, neighbours.Count);
        Assert.IsTrue(Search(neighbours, new Cell(2, 1)));
        Assert.IsTrue(Search(neighbours, new Cell(2, 2)));


        direction = new South_SouthWest(originCell);
        neighbours = direction.GetNearbyCells(originCell, gridArray);
        Assert.AreEqual(2, neighbours.Count);
        Assert.IsTrue(Search(neighbours, new Cell(2, 0)));
        Assert.IsTrue(Search(neighbours, new Cell(2, 1)));


        direction = new West_SouthWest(originCell);
        neighbours = direction.GetNearbyCells(originCell, gridArray);
        Assert.AreEqual(2, neighbours.Count);
        Assert.IsTrue(Search(neighbours, new Cell(1, 0)));
        Assert.IsTrue(Search(neighbours, new Cell(2, 0)));


        direction = new West_NorthWest(originCell);
        neighbours = direction.GetNearbyCells(originCell, gridArray);
        Assert.AreEqual(2, neighbours.Count);
        Assert.IsTrue(Search(neighbours, new Cell(0, 0)));
        Assert.IsTrue(Search(neighbours, new Cell(1, 0)));


        originCell = gridArray[1, 2];
        direction = new North_NorthEast(originCell);
        neighbours = direction.GetNearbyCells(originCell, gridArray);
        Assert.AreEqual(1, neighbours.Count);
        Assert.IsTrue(Search(neighbours, new Cell(0, 2)));


        originCell = gridArray[2, 0];
        direction = new South_SouthWest(originCell);
        neighbours = direction.GetNearbyCells(originCell, gridArray);
        Assert.AreEqual(0, neighbours.Count);

    }



    private bool Search(List<Cell> list, Cell target)
    {
        bool found = false;
        foreach (Cell elem in list)
        {
            if (elem.GetX() == target.GetX() &&
                elem.GetY() == target.GetY())
            {
                found = true;
                break;
            }
        }
        return found;
    }


}
