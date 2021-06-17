using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System;


public class TestGridUtils
{
    [Test]
    public void GetNearbyCells_Test()
    {
        int rows = 5;
        int cols = 5;
        //cellToTile = new Dictionary<Cell, GameObject>();
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);
        ArrayList neighbours;
        
        
        neighbours = GridUtils.GetNearbyCells(0, 0, gridArray);
        Assert.AreEqual(neighbours.Count, 3);
        Assert.IsTrue(Search(neighbours, new Cell(1, 0)));
        Assert.IsTrue(Search(neighbours, new Cell(1, 1)));
        Assert.IsTrue(Search(neighbours, new Cell(0, 1)));

        neighbours = GridUtils.GetNearbyCells(4, 4, gridArray);
        Assert.AreEqual(neighbours.Count, 3);
        Assert.IsTrue(Search(neighbours, new Cell(3, 4)));
        Assert.IsTrue(Search(neighbours, new Cell(4, 3)));
        Assert.IsTrue(Search(neighbours, new Cell(3, 3)));

        neighbours = GridUtils.GetNearbyCells(0, 4, gridArray);
        Assert.AreEqual(neighbours.Count, 3);
        Assert.IsTrue(Search(neighbours, new Cell(0, 3)));
        Assert.IsTrue(Search(neighbours, new Cell(1, 4)));
        Assert.IsTrue(Search(neighbours, new Cell(1, 3)));

        neighbours = GridUtils.GetNearbyCells(4, 0, gridArray);
        Assert.AreEqual(neighbours.Count, 3);
        Assert.IsTrue(Search(neighbours, new Cell(3, 0)));
        Assert.IsTrue(Search(neighbours, new Cell(4, 1)));
        Assert.IsTrue(Search(neighbours, new Cell(3, 1)));

        neighbours = GridUtils.GetNearbyCells(2, 2, gridArray);
        Assert.AreEqual(neighbours.Count, 8);
        Assert.IsTrue(Search(neighbours, new Cell(2, 1)));
        Assert.IsTrue(Search(neighbours, new Cell(3, 1)));
        Assert.IsTrue(Search(neighbours, new Cell(3, 2)));
        Assert.IsTrue(Search(neighbours, new Cell(3, 3)));
        Assert.IsTrue(Search(neighbours, new Cell(2, 3)));
        Assert.IsTrue(Search(neighbours, new Cell(1, 3)));
        Assert.IsTrue(Search(neighbours, new Cell(1, 2)));
        Assert.IsTrue(Search(neighbours, new Cell(1, 1)));


    }



    private bool Search(ArrayList list, Cell target)
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
