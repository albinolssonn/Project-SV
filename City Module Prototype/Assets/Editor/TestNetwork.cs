using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class TestNetwork
{
    [Test]
    public void BigTown_Test()
    {
        Network searcher = new Network();
        int rows = 5;
        int cols = 5;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        searcher.BuildNetwork(gridArray, 2, 2);


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

        gridArray[1, 0].AddCellContent(new House());
        gridArray[1, 0].AddCellContent(new House());

        gridArray[1, 1].AddCellContent(new House());
        gridArray[1, 1].AddCellContent(new House());

        gridArray[1, 2].AddCellContent(new House());
        gridArray[1, 2].AddCellContent(new House());

        gridArray[1, 3].AddCellContent(new Park());

        gridArray[1, 4].AddCellContent(new House());

        gridArray[2, 0].AddCellContent(new House());
        gridArray[2, 0].AddCellContent(new House());
        gridArray[2, 0].AddCellContent(new House());

        gridArray[2, 1].AddCellContent(new Park());

        gridArray[2, 2].AddCellContent(new Park());
        gridArray[2, 2].AddCellContent(new Antenna());


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

        for (int row = 0; row < rows; row++)
        {
            Debug.Log(gridArray[row, 0].GetSignalStr() + "   " + gridArray[row, 1].GetSignalStr() + "   " + gridArray[row, 2].GetSignalStr() + "   " + gridArray[row, 3].GetSignalStr() + "   " + gridArray[row, 4].GetSignalStr());
            
        }

        Assert.AreEqual(10, gridArray[2, 2].GetSignalStr());

        Assert.AreEqual(4, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(4, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(3, gridArray[0, 2].GetSignalStr());
        Assert.AreEqual(0, gridArray[0, 3].GetSignalStr());
        Assert.AreEqual(5, gridArray[0, 4].GetSignalStr());

        Assert.AreEqual(7, gridArray[1, 0].GetSignalStr());
        Assert.AreEqual(7, gridArray[1, 1].GetSignalStr());
        Assert.AreEqual(6, gridArray[1, 2].GetSignalStr());
        Assert.AreEqual(7, gridArray[1, 3].GetSignalStr());
        Assert.AreEqual(5, gridArray[1, 4].GetSignalStr());

        Assert.AreEqual(9, gridArray[2, 1].GetSignalStr());
        Assert.AreEqual(8, gridArray[2, 2].GetSignalStr());
        Assert.AreEqual(7, gridArray[2, 3].GetSignalStr());
        Assert.AreEqual(4, gridArray[2, 4].GetSignalStr());

        Assert.AreEqual(3, gridArray[3, 0].GetSignalStr());
        Assert.AreEqual(5, gridArray[3, 1].GetSignalStr());
        Assert.AreEqual(5, gridArray[3, 2].GetSignalStr());
        Assert.AreEqual(7, gridArray[3, 3].GetSignalStr());
        Assert.AreEqual(2, gridArray[3, 4].GetSignalStr());

        Assert.AreEqual(0, gridArray[4, 0].GetSignalStr());
        Assert.AreEqual(2, gridArray[4, 1].GetSignalStr());
        Assert.AreEqual(0, gridArray[4, 2].GetSignalStr());
        Assert.AreEqual(0, gridArray[4, 3].GetSignalStr());
        Assert.AreEqual(1, gridArray[4, 4].GetSignalStr());
    }

    [Test]
    public void SmallTown_Test1()
    {
        Network searcher = new Network();
        int rows = 3;
        int cols = 3;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[0, 0].AddCellContent(new FireDepartment());
        gridArray[0, 1].AddCellContent(new TallBuilding());

        gridArray[1, 0].AddCellContent(new House());
        gridArray[1, 0].AddCellContent(new Antenna());
        gridArray[1, 1].AddCellContent(new TallBuilding());
        gridArray[1, 1].AddCellContent(new TallBuilding());
        gridArray[1, 2].AddCellContent(new House());

        gridArray[2, 0].AddCellContent(new Park());
        gridArray[2, 1].AddCellContent(new Park());
        gridArray[2, 2].AddCellContent(new Park());

        searcher.BuildNetwork(gridArray, 1, 0);

        Debug.Log(gridArray[0, 0].GetSignalStr() + "  " + gridArray[0, 1].GetSignalStr() + "  " + gridArray[0, 2].GetSignalStr());
        Debug.Log(gridArray[1, 0].GetSignalStr() + "  " + gridArray[1, 1].GetSignalStr() + "  " + gridArray[1, 2].GetSignalStr());
        Debug.Log(gridArray[2, 0].GetSignalStr() + "  " + gridArray[2, 1].GetSignalStr() + "  " + gridArray[2, 2].GetSignalStr());


        Assert.AreEqual(10, gridArray[1, 0].GetSignalStr());

        Assert.AreEqual(6, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(5, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(4, gridArray[0, 2].GetSignalStr());

        Assert.AreEqual(3, gridArray[1, 1].GetSignalStr());
        Assert.AreEqual(1, gridArray[1, 2].GetSignalStr());

        Assert.AreEqual(9, gridArray[2, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 1].GetSignalStr());
        Assert.AreEqual(8, gridArray[2, 2].GetSignalStr());
    }

    [Test]
    public void SmallTown_Test2()
    {
        Network searcher = new Network();
        int rows = 3;
        int cols = 3;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);


        gridArray[0, 0].AddCellContent(new FireDepartment());
        gridArray[0, 1].AddCellContent(new TallBuilding());

        gridArray[1, 0].AddCellContent(new House());
        gridArray[1, 1].AddCellContent(new Antenna());
        gridArray[1, 1].AddCellContent(new TallBuilding());
        gridArray[1, 1].AddCellContent(new TallBuilding());
        gridArray[1, 2].AddCellContent(new House());

        gridArray[2, 0].AddCellContent(new Park());
        gridArray[2, 1].AddCellContent(new Park());
        gridArray[2, 2].AddCellContent(new Park());

        searcher.BuildNetwork(gridArray, 1, 1);

        Debug.Log(gridArray[0, 0].GetSignalStr() + "  " + gridArray[0, 1].GetSignalStr() + "  " + gridArray[0, 2].GetSignalStr());
        Debug.Log(gridArray[1, 0].GetSignalStr() + "  " + gridArray[1, 1].GetSignalStr() + "  " + gridArray[1, 2].GetSignalStr());
        Debug.Log(gridArray[2, 0].GetSignalStr() + "  " + gridArray[2, 1].GetSignalStr() + "  " + gridArray[2, 2].GetSignalStr());





        Assert.AreEqual(10, gridArray[1, 1].GetSignalStr());

        Assert.AreEqual(8, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(7, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(9, gridArray[0, 2].GetSignalStr());

        Assert.AreEqual(8, gridArray[1, 0].GetSignalStr());
        Assert.AreEqual(8, gridArray[1, 2].GetSignalStr());

        Assert.AreEqual(9, gridArray[2, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 1].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 2].GetSignalStr());
    }

    [Test]
    public void Height_Test1()
    {

        Network searcher = new Network();
        int rows = 2;
        int cols = 2;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[1, 0].AddCellContent(new House());
        gridArray[1, 0].AddCellContent(new Antenna());
        gridArray[1, 1].AddCellContent(new TallBuilding());

        searcher.BuildNetwork(gridArray, 1, 0);

        for (int row = 0; row < rows; row++)
        {
            Debug.Log(gridArray[row, 0].GetSignalStr() + "   " + gridArray[row, 1].GetSignalStr());

        }

        Assert.AreEqual(10, gridArray[1, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(5, gridArray[1, 1].GetSignalStr());

    }

    [Test]
    public void Height_Test2()
    {

        Network searcher = new Network();
        int rows = 2;
        int cols = 2;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[1, 0].AddCellContent(new House());
        gridArray[1, 1].AddCellContent(new Antenna());
        gridArray[1, 1].AddCellContent(new TallBuilding());

        searcher.BuildNetwork(gridArray, 1, 1);

        for (int row = 0; row < rows; row++)
        {
            Debug.Log(gridArray[row, 0].GetSignalStr() + "   " + gridArray[row, 1].GetSignalStr());
        }

        Assert.AreEqual(10, gridArray[1, 1].GetSignalStr());
        Assert.AreEqual(9, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(8, gridArray[1, 0].GetSignalStr());
        

    }

    [Test]
    public void Hallway5x1_Test()
    {
        Network searcher = new Network();
        int rows = 1;
        int cols = 5;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[0, 0].AddCellContent(new Antenna());
        gridArray[0, 0].AddCellContent(new House());

        gridArray[0, 1].AddCellContent(new Park());

        gridArray[0, 2].AddCellContent(new TallBuilding());
        gridArray[0, 2].AddCellContent(new TallBuilding());


        gridArray[0, 4].AddCellContent(new House());



        searcher.BuildNetwork(gridArray, 0, 0);


        Debug.Log(gridArray[0, 0].GetSignalStr() + "   " + gridArray[0, 1].GetSignalStr() + "   " + gridArray[0, 2].GetSignalStr() + "   " + gridArray[0, 3].GetSignalStr() + "   " + gridArray[0,4].GetSignalStr());

        Assert.AreEqual(10, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(2, gridArray[0, 2].GetSignalStr());
        Assert.AreEqual(1, gridArray[0, 3].GetSignalStr());
        Assert.AreEqual(0, gridArray[0, 4].GetSignalStr());
    }

    [Test]
    public void Hallway5x2_Test()
    {
        Network searcher = new Network();
        int rows = 2;
        int cols = 5;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[1, 0].AddCellContent(new Antenna());
        gridArray[1, 0].AddCellContent(new House());

        gridArray[1, 1].AddCellContent(new Park());

        gridArray[1, 2].AddCellContent(new TallBuilding());
        gridArray[1, 2].AddCellContent(new TallBuilding());

        gridArray[1, 4].AddCellContent(new House());


        gridArray[0, 0].AddCellContent(new Park());

        gridArray[0, 1].AddCellContent(new House());

        gridArray[0, 2].AddCellContent(new Park());

        gridArray[0, 3].AddCellContent(new House());

        gridArray[0, 4].AddCellContent(new House());




        searcher.BuildNetwork(gridArray, 1, 0);

        for (int row = 0; row < rows; row++)
        {
            Debug.Log(gridArray[row, 0].GetSignalStr() + "   " + gridArray[row, 1].GetSignalStr() + "   " + gridArray[row, 2].GetSignalStr() + "   " + gridArray[row, 3].GetSignalStr() + "   " + gridArray[row, 4].GetSignalStr());

        }

        Assert.AreEqual(10, gridArray[1, 0].GetSignalStr());

        Assert.AreEqual(9, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(8, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(8, gridArray[0, 2].GetSignalStr());
        Assert.AreEqual(6, gridArray[0, 3].GetSignalStr());
        Assert.AreEqual(4, gridArray[0, 4].GetSignalStr());

        Assert.AreEqual(9, gridArray[1, 1].GetSignalStr());
        Assert.AreEqual(2, gridArray[1, 2].GetSignalStr());
        Assert.AreEqual(1, gridArray[1, 3].GetSignalStr());
        Assert.AreEqual(0, gridArray[1, 4].GetSignalStr());

    }

    [Test]
    public void Hallway5x3_Test()
    {
        Network searcher = new Network();
        int rows = 3;
        int cols = 5;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[0, 0].AddCellContent(new Park());

        gridArray[0, 1].AddCellContent(new House());

        gridArray[0, 2].AddCellContent(new Park());

        gridArray[0, 3].AddCellContent(new House());

        gridArray[0, 4].AddCellContent(new House());


        gridArray[1, 0].AddCellContent(new Antenna());
        gridArray[1, 0].AddCellContent(new House());

        gridArray[1, 1].AddCellContent(new Park());

        gridArray[1, 2].AddCellContent(new TallBuilding());
        gridArray[1, 2].AddCellContent(new TallBuilding());

        gridArray[1, 4].AddCellContent(new House());



        gridArray[2, 0].AddCellContent(new Park());

        gridArray[2, 1].AddCellContent(new Park());

        gridArray[2, 2].AddCellContent(new Park());

        gridArray[2, 3].AddCellContent(new Park());

        gridArray[2, 4].AddCellContent(new Park());




        searcher.BuildNetwork(gridArray, 1, 0);

        for (int row = 0; row < rows; row++)
        {
            Debug.Log(gridArray[row, 0].GetSignalStr() + "   " + gridArray[row, 1].GetSignalStr() + "   " + gridArray[row, 2].GetSignalStr() + "   " + gridArray[row, 3].GetSignalStr() + "   " + gridArray[row, 4].GetSignalStr());

        }

        Assert.AreEqual(10, gridArray[1, 0].GetSignalStr());

        Assert.AreEqual(9, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(8, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(8, gridArray[0, 2].GetSignalStr());
        Assert.AreEqual(6, gridArray[0, 3].GetSignalStr());
        Assert.AreEqual(4, gridArray[0, 4].GetSignalStr());

        Assert.AreEqual(9, gridArray[1, 1].GetSignalStr());
        Assert.AreEqual(2, gridArray[1, 2].GetSignalStr());
        Assert.AreEqual(1, gridArray[1, 3].GetSignalStr());
        Assert.AreEqual(0, gridArray[1, 4].GetSignalStr());

        Assert.AreEqual(9, gridArray[2, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 1].GetSignalStr());
        Assert.AreEqual(8, gridArray[2, 2].GetSignalStr());
        Assert.AreEqual(7, gridArray[2, 3].GetSignalStr());
        Assert.AreEqual(6, gridArray[2, 4].GetSignalStr());

    }


    [Test]
    public void Hallway1x5_Test()
    {
        Network searcher = new Network();
        int rows = 5;
        int cols = 1;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[0, 0].AddCellContent(new Park());

        gridArray[1, 0].AddCellContent(new House());
        gridArray[1, 0].AddCellContent(new Antenna());

        gridArray[2, 0].AddCellContent(new Park());

        gridArray[3, 0].AddCellContent(new TallBuilding());



        gridArray[4, 0].AddCellContent(new Park());


        searcher.BuildNetwork(gridArray, 1, 0);


        for (int row = 0; row < rows; row++)
        {
            Debug.Log(gridArray[row, 0].GetSignalStr());

        }

        Assert.AreEqual(10, gridArray[1, 0].GetSignalStr());

        Assert.AreEqual(9, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 0].GetSignalStr());
        Assert.AreEqual(4, gridArray[3, 0].GetSignalStr());
        Assert.AreEqual(3, gridArray[4, 0].GetSignalStr());
    }

    [Test]
    public void Hallway2x5_Test()
    {
        Network searcher = new Network();
        int rows = 5;
        int cols = 2;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[0, 0].AddCellContent(new Park());

        gridArray[0, 1].AddCellContent(new House());


        gridArray[1, 0].AddCellContent(new House());
        gridArray[1, 0].AddCellContent(new Antenna());

        gridArray[1, 1].AddCellContent(new Park());


        gridArray[2, 0].AddCellContent(new Park());

        gridArray[2, 1].AddCellContent(new Park());


        gridArray[3, 0].AddCellContent(new TallBuilding());

        gridArray[3, 1].AddCellContent(new House());


        gridArray[4, 0].AddCellContent(new Park());

        gridArray[4, 1].AddCellContent(new House());
        gridArray[4, 1].AddCellContent(new House());

        searcher.BuildNetwork(gridArray, 1, 0);


        for (int row = 0; row < rows; row++)
        {
            Debug.Log(gridArray[row, 0].GetSignalStr() + "   " + gridArray[row, 1].GetSignalStr());

        }

        Assert.AreEqual(10, gridArray[1, 0].GetSignalStr());

        Assert.AreEqual(9, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(8, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(9, gridArray[1, 1].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 1].GetSignalStr());
        Assert.AreEqual(4, gridArray[3, 0].GetSignalStr());
        Assert.AreEqual(7, gridArray[3, 1].GetSignalStr());
        Assert.AreEqual(3, gridArray[4, 0].GetSignalStr());
        Assert.AreEqual(4, gridArray[4, 1].GetSignalStr());
    }

    [Test]
    public void Hallway3x5_Test()
    {
        Network searcher = new Network();
        int rows = 5;
        int cols = 3;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[0, 0].AddCellContent(new Park());

        gridArray[0, 1].AddCellContent(new House());

        gridArray[0, 2].AddCellContent(new TallBuilding());


        gridArray[1, 0].AddCellContent(new House());
        gridArray[1, 0].AddCellContent(new Antenna());

        gridArray[1, 1].AddCellContent(new Park());

        gridArray[1, 2].AddCellContent(new House());


        gridArray[2, 0].AddCellContent(new Park());

        gridArray[2, 1].AddCellContent(new Park());

        gridArray[2, 2].AddCellContent(new TallBuilding());
        gridArray[2, 2].AddCellContent(new TallBuilding());


        gridArray[3, 0].AddCellContent(new TallBuilding());

        gridArray[3, 1].AddCellContent(new TallBuilding());

        gridArray[3, 2].AddCellContent(new House());


        gridArray[4, 0].AddCellContent(new Park());

        gridArray[4, 1].AddCellContent(new House());
        gridArray[4, 1].AddCellContent(new House());

        gridArray[4, 2].AddCellContent(new Park());


        searcher.BuildNetwork(gridArray, 1, 0);


        for (int row = 0; row < rows; row++)
        {
            Debug.Log(gridArray[row, 0].GetSignalStr() + "   " + gridArray[row, 1].GetSignalStr() + "   " + gridArray[row, 2].GetSignalStr());

        }

        Assert.AreEqual(10, gridArray[1, 0].GetSignalStr());

        Assert.AreEqual(9, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(8, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(3, gridArray[0, 2].GetSignalStr());

        Assert.AreEqual(9, gridArray[1, 1].GetSignalStr());
        Assert.AreEqual(7, gridArray[1, 2].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 1].GetSignalStr());
        Assert.AreEqual(2, gridArray[2, 2].GetSignalStr());
        Assert.AreEqual(4, gridArray[3, 0].GetSignalStr());
        Assert.AreEqual(4, gridArray[3, 1].GetSignalStr());
        Assert.AreEqual(7, gridArray[3, 2].GetSignalStr());
        Assert.AreEqual(3, gridArray[4, 0].GetSignalStr());
        Assert.AreEqual(1, gridArray[4, 1].GetSignalStr());
        Assert.AreEqual(6, gridArray[4, 2].GetSignalStr());
    }

}

