using System.Collections.Generic;
using NUnit.Framework;


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
        int rows = 5;
        int cols = 5;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);
        List<Cell> neighbours;
        
        
        neighbours = GridUtils.GetNearbyCells(gridArray[0, 0], gridArray);
        Assert.AreEqual(3, neighbours.Count);
        Assert.IsTrue(Search(neighbours, new Cell(0, 1)));
        Assert.IsTrue(Search(neighbours, new Cell(1, 0)));
        Assert.IsTrue(Search(neighbours, new Cell(1, 1)));

        neighbours = GridUtils.GetNearbyCells(gridArray[4, 4], gridArray);
        Assert.AreEqual(3, neighbours.Count);
        Assert.IsTrue(Search(neighbours, new Cell(3, 3)));
        Assert.IsTrue(Search(neighbours, new Cell(3, 4)));
        Assert.IsTrue(Search(neighbours, new Cell(4, 3)));

        neighbours = GridUtils.GetNearbyCells(gridArray[0, 4], gridArray);
        Assert.AreEqual(3, neighbours.Count);
        Assert.IsTrue(Search(neighbours, new Cell(0, 3)));
        Assert.IsTrue(Search(neighbours, new Cell(1, 3)));
        Assert.IsTrue(Search(neighbours, new Cell(1, 4)));

        neighbours = GridUtils.GetNearbyCells(gridArray[4, 0], gridArray);
        Assert.AreEqual(3, neighbours.Count);
        Assert.IsTrue(Search(neighbours, new Cell(3, 0)));
        Assert.IsTrue(Search(neighbours, new Cell(3, 1)));
        Assert.IsTrue(Search(neighbours, new Cell(4, 1)));

        neighbours = GridUtils.GetNearbyCells(gridArray[2, 2], gridArray);
        Assert.AreEqual(8, neighbours.Count);
        Assert.IsTrue(Search(neighbours, new Cell(1, 1)));
        Assert.IsTrue(Search(neighbours, new Cell(1, 2)));
        Assert.IsTrue(Search(neighbours, new Cell(1, 3)));
        Assert.IsTrue(Search(neighbours, new Cell(2, 1)));
        Assert.IsTrue(Search(neighbours, new Cell(2, 3)));
        Assert.IsTrue(Search(neighbours, new Cell(3, 1)));
        Assert.IsTrue(Search(neighbours, new Cell(3, 2)));
        Assert.IsTrue(Search(neighbours, new Cell(3, 3)));


    }



    private bool Search(List<Cell> list, Cell target)
    {
        bool found = false;
        foreach (Cell elem in list)
        {
            if(elem.GetX() == target.GetX() &&
                elem.GetY() == target.GetY())
            {
                found = true;
                break;
            }
        }
        return found;
    }


}
