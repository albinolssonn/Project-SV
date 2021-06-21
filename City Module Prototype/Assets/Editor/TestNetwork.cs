using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class TestNetwork
{
    [Test]
    public void BFSeach_Test()
    {
        Network searcher = new Network();
        int rows = 5;
        int cols = 5;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[0, 0].AddCellContent(new House());
        gridArray[0, 0].AddCellContent(new House());

        gridArray[0, 1].AddCellContent(new House());
        gridArray[0, 1].AddCellContent(new House());

        gridArray[0, 2].AddCellContent(new House());
        gridArray[0, 2].AddCellContent(new House());
        gridArray[0, 2].AddCellContent(new House());

        gridArray[0, 3].AddCellContent(new TallBuilding());
        gridArray[0, 3].AddCellContent(new TallBuilding());

        gridArray[0, 4].AddCellContent(new House());
        gridArray[0, 4].AddCellContent(new House());

        gridArray[1, 0].AddCellContent(new House());
        gridArray[1, 0].AddCellContent(new House());

        gridArray[1, 1].AddCellContent(new House());
        gridArray[1, 1].AddCellContent(new House());

        gridArray[1, 2].AddCellContent(new House());
        gridArray[1, 2].AddCellContent(new House());

        gridArray[1, 3].AddCellContent(new Park());
        gridArray[1, 3].AddCellContent(new Park());

        gridArray[1, 4].AddCellContent(new House());
        gridArray[1, 4].AddCellContent(new House());

        gridArray[2, 0].AddCellContent(new TallBuilding());
        gridArray[2, 0].AddCellContent(new TallBuilding());
        gridArray[2, 0].AddCellContent(new TallBuilding());

        gridArray[2, 1].AddCellContent(new Park());

        gridArray[2, 2].AddCellContent(new Park());

        gridArray[2, 3].AddCellContent(new Park());

        gridArray[2, 4].AddCellContent(new House());
        gridArray[2, 4].AddCellContent(new House());

        gridArray[3, 0].AddCellContent(new TallBuilding());
        gridArray[3, 0].AddCellContent(new TallBuilding());

        gridArray[3, 1].AddCellContent(new Hospital());

        gridArray[3, 2].AddCellContent(new PoliceStation());

        gridArray[3, 4].AddCellContent(new House());
        gridArray[3, 4].AddCellContent(new House());
        gridArray[3, 4].AddCellContent(new House());
        gridArray[3, 4].AddCellContent(new House());

        gridArray[4, 0].AddCellContent(new TallBuilding());

        gridArray[4, 2].AddCellContent(new FireDepartment());

        gridArray[4, 3].AddCellContent(new TallBuilding());
        gridArray[4, 3].AddCellContent(new TallBuilding());

        gridArray[4, 4].AddCellContent(new House());
        gridArray[4, 4].AddCellContent(new House());
        gridArray[4, 4].AddCellContent(new House());
        gridArray[4, 4].AddCellContent(new House());
        gridArray[4, 4].AddCellContent(new House());



        //TODO: Finish this test when the logic for the network traversal is done.
        Assert.IsTrue(false);
    }

    [Test]
    public void Height_Test()
    {

        Network searcher = new Network();
        int rows = 2;
        int cols = 2;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[1, 0].AddCellContent(new House());
        gridArray[1, 0].AddCellContent(new Antenna());
        gridArray[1, 1].AddCellContent(new TallBuilding());

        searcher.BuildNetwork(gridArray, 1, 0);

        Assert.AreEqual(9, gridArray[1, 0].GetSignalStr());
        Assert.AreEqual(8, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(8, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(4, gridArray[1, 1].GetSignalStr());

    }
}
