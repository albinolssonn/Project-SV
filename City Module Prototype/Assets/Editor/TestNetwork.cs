using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

public class TestNetwork
{
    private readonly double baseStationStr = GridManager.baseSignalStr;
    private readonly double distancePenalty = GridManager.distancePenalty;
    private readonly double heightPenalty = GridManager.heightPenalty;



    [Test]
    public void TwoAntennas_Test()
    {
        Network network = new Network(baseStationStr, distancePenalty, heightPenalty);
        int rows = 5;
        int cols = 6;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[0, 1].AddCellContent(new TallBuilding());
        gridArray[0, 3].AddCellContent(new TallBuilding());

        gridArray[1, 1].AddCellContent(new TallBuilding());
        gridArray[1, 2].AddCellContent(new Antenna());
        gridArray[1, 3].AddCellContent(new TallBuilding());

        gridArray[2, 1].AddCellContent(new TallBuilding());
        gridArray[2, 3].AddCellContent(new TallBuilding());

        gridArray[3, 0].AddCellContent(new House());
        gridArray[3, 0].AddCellContent(new Antenna());
        gridArray[3, 1].AddCellContent(new House());



        network.BuildNetwork(gridArray);


        for (int row = 0; row < rows; row++)
        {
            Debug.Log(gridArray[row, 0].GetSignalStr() + "   " + gridArray[row, 1].GetSignalStr() + "   " + gridArray[row, 2].GetSignalStr() + "   " + gridArray[row, 3].GetSignalStr() + "   " + gridArray[row, 4].GetSignalStr() + "   " + gridArray[row, 5].GetSignalStr());

        }


        Assert.AreEqual(7, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(9, gridArray[0, 2].GetSignalStr());
        Assert.AreEqual(9, gridArray[0, 3].GetSignalStr());
        Assert.AreEqual(4, gridArray[0, 4].GetSignalStr());
        Assert.AreEqual(3, gridArray[0, 5].GetSignalStr());

        Assert.AreEqual(8, gridArray[1, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[1, 1].GetSignalStr());
        Assert.AreEqual(10, gridArray[1, 2].GetSignalStr());
        Assert.AreEqual(9, gridArray[1, 3].GetSignalStr());
        Assert.AreEqual(4, gridArray[1, 4].GetSignalStr());
        Assert.AreEqual(4, gridArray[1, 5].GetSignalStr());

        Assert.AreEqual(9, gridArray[2, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 1].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 2].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 3].GetSignalStr());
        Assert.AreEqual(5, gridArray[2, 4].GetSignalStr());
        Assert.AreEqual(4, gridArray[2, 5].GetSignalStr());

        Assert.AreEqual(10, gridArray[3, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[3, 1].GetSignalStr());
        Assert.AreEqual(8, gridArray[3, 2].GetSignalStr());
        Assert.AreEqual(8, gridArray[3, 3].GetSignalStr());
        Assert.AreEqual(5, gridArray[3, 4].GetSignalStr());
        Assert.AreEqual(4, gridArray[3, 5].GetSignalStr());

        Assert.AreEqual(9, gridArray[4, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[4, 1].GetSignalStr());
        Assert.AreEqual(8, gridArray[4, 2].GetSignalStr());
        Assert.AreEqual(7, gridArray[4, 3].GetSignalStr());
        Assert.AreEqual(7, gridArray[4, 4].GetSignalStr());
        Assert.AreEqual(5, gridArray[4, 5].GetSignalStr());

    }

    [Test]
    public void SmallTown_Test1()
    {
        Network network = new Network(baseStationStr, distancePenalty, heightPenalty);
        int rows = 3;
        int cols = 3;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);



        gridArray[0, 0].AddCellContent(new FireDepartment());

        gridArray[0, 1].AddCellContent(new TallBuilding());

        gridArray[1, 0].AddCellContent(new Antenna());
        gridArray[1, 0].AddCellContent(new House());

        gridArray[1, 1].AddCellContent(new TallBuilding());
        gridArray[1, 1].AddCellContent(new TallBuilding());

        gridArray[1, 2].AddCellContent(new House());

        gridArray[2, 0].AddCellContent(new Park());

        gridArray[2, 1].AddCellContent(new Park());

        gridArray[2, 2].AddCellContent(new Park());

        network.BuildNetwork(gridArray);

        Debug.Log(gridArray[0, 0].GetSignalStr() + "  " + gridArray[0, 1].GetSignalStr() + "  " + gridArray[0, 2].GetSignalStr());
        Debug.Log(gridArray[1, 0].GetSignalStr() + "  " + gridArray[1, 1].GetSignalStr() + "  " + gridArray[1, 2].GetSignalStr());
        Debug.Log(gridArray[2, 0].GetSignalStr() + "  " + gridArray[2, 1].GetSignalStr() + "  " + gridArray[2, 2].GetSignalStr());




        Assert.AreEqual(9, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(4, gridArray[0, 2].GetSignalStr());

        Assert.AreEqual(10, gridArray[1, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[1, 1].GetSignalStr());
        Assert.AreEqual(2, gridArray[1, 2].GetSignalStr());

        Assert.AreEqual(9, gridArray[2, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 1].GetSignalStr());
        Assert.AreEqual(8, gridArray[2, 2].GetSignalStr());
    }

    [Test]
    public void SmallTown_Test2()
    {
        Network network = new Network(baseStationStr, distancePenalty, heightPenalty);
        int rows = 4;
        int cols = 4;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);


        gridArray[0, 0].AddCellContent(new FireDepartment());

        gridArray[0, 1].AddCellContent(new TallBuilding());

        //empty at [0,2]

        gridArray[0, 3].AddCellContent(new House());

        gridArray[1, 0].AddCellContent(new House());

        gridArray[1, 1].AddCellContent(new Antenna());
        gridArray[1, 1].AddCellContent(new TallBuilding());
        gridArray[1, 1].AddCellContent(new TallBuilding());

        gridArray[1, 2].AddCellContent(new House());

        gridArray[1, 3].AddCellContent(new TallBuilding());

        gridArray[2, 0].AddCellContent(new TallBuilding());
        gridArray[2, 0].AddCellContent(new TallBuilding());

        gridArray[2, 1].AddCellContent(new House());
        gridArray[2, 1].AddCellContent(new House());
        gridArray[2, 1].AddCellContent(new House());


        //empty at [2,2]

        gridArray[2, 3].AddCellContent(new House());

        gridArray[3, 0].AddCellContent(new House());

        gridArray[3, 1].AddCellContent(new TallBuilding());
        gridArray[3, 1].AddCellContent(new TallBuilding());

        gridArray[3, 2].AddCellContent(new Park());

        gridArray[3, 3].AddCellContent(new Hospital());




        network.BuildNetwork(gridArray);

        for (int row = 0; row < rows; row++)
        {
            Debug.Log(gridArray[row, 0].GetSignalStr() + "   " + gridArray[row, 1].GetSignalStr() + "   " + gridArray[row, 2].GetSignalStr() + "   " + gridArray[row, 3].GetSignalStr());

        }




        Assert.AreEqual(9, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(9, gridArray[0, 2].GetSignalStr());
        Assert.AreEqual(8, gridArray[0, 3].GetSignalStr());

        Assert.AreEqual(9, gridArray[1, 0].GetSignalStr());
        Assert.AreEqual(10, gridArray[1, 1].GetSignalStr());
        Assert.AreEqual(9, gridArray[1, 2].GetSignalStr());
        Assert.AreEqual(7, gridArray[1, 3].GetSignalStr());


        Assert.AreEqual(9, gridArray[2, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 1].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 2].GetSignalStr());
        Assert.AreEqual(8, gridArray[2, 3].GetSignalStr());

        Assert.AreEqual(5, gridArray[3, 0].GetSignalStr());
        Assert.AreEqual(5, gridArray[3, 1].GetSignalStr());
        Assert.AreEqual(8, gridArray[3, 2].GetSignalStr());
        Assert.AreEqual(8, gridArray[3, 3].GetSignalStr());

    }

    [Test]
    public void Height_Test1()
    {

        Network network = new Network(baseStationStr, distancePenalty, heightPenalty);
        int rows = 3;
        int cols = 3;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[0, 0].AddCellContent(new TallBuilding());

        gridArray[0, 1].AddCellContent(new House());
        gridArray[0, 1].AddCellContent(new House());

        gridArray[0, 2].AddCellContent(new Park());

        gridArray[1, 0].AddCellContent(new House());
        gridArray[1, 0].AddCellContent(new House());
        gridArray[1, 0].AddCellContent(new House());

        gridArray[1, 1].AddCellContent(new FireDepartment());

        gridArray[1, 2].AddCellContent(new House());
        gridArray[1, 2].AddCellContent(new House());

        gridArray[2, 0].AddCellContent(new Park());
        gridArray[2, 0].AddCellContent(new Antenna());

        gridArray[2, 1].AddCellContent(new TallBuilding());

        gridArray[2, 2].AddCellContent(new House());

        network.BuildNetwork(gridArray);

        for (int row = 0; row < rows; row++)
        {
            Debug.Log(gridArray[row, 0].GetSignalStr() + "   " + gridArray[row, 1].GetSignalStr() + "   " + gridArray[row, 2].GetSignalStr());

        }

        Assert.AreEqual(3, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(5, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(5, gridArray[0, 2].GetSignalStr());

        Assert.AreEqual(9, gridArray[1, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[1, 1].GetSignalStr());
        Assert.AreEqual(5, gridArray[1, 2].GetSignalStr());

        Assert.AreEqual(10, gridArray[2, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 1].GetSignalStr());
        Assert.AreEqual(4, gridArray[2, 2].GetSignalStr());
    }

    [Test]
    public void Height_Test2()
    {

        Network network = new Network(baseStationStr, distancePenalty, heightPenalty);
        int rows = 3;
        int cols = 3;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);


        //empty [0,0]

        gridArray[0, 1].AddCellContent(new Park());

        gridArray[0, 2].AddCellContent(new TallBuilding());
        gridArray[0, 2].AddCellContent(new TallBuilding());

        gridArray[1, 0].AddCellContent(new FireDepartment());

        gridArray[1, 1].AddCellContent(new TallBuilding());
        gridArray[1, 1].AddCellContent(new TallBuilding());

        gridArray[1, 2].AddCellContent(new TallBuilding());
        gridArray[1, 2].AddCellContent(new Antenna());

        gridArray[2, 0].AddCellContent(new House());
        gridArray[2, 0].AddCellContent(new House());
        gridArray[2, 0].AddCellContent(new House());

        gridArray[2, 1].AddCellContent(new House());

        gridArray[2, 2].AddCellContent(new Park());


        network.BuildNetwork(gridArray);

        for (int row = 0; row < rows; row++)
        {
            Debug.Log(gridArray[row, 0].GetSignalStr() + "   " + gridArray[row, 1].GetSignalStr() + "   " + gridArray[row, 2].GetSignalStr());
        }

        Assert.AreEqual(8, gridArray[0, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[0, 1].GetSignalStr());
        Assert.AreEqual(9, gridArray[0, 2].GetSignalStr());

        Assert.AreEqual(4, gridArray[1, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[1, 1].GetSignalStr());
        Assert.AreEqual(10, gridArray[1, 2].GetSignalStr());

        Assert.AreEqual(7, gridArray[2, 0].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 1].GetSignalStr());
        Assert.AreEqual(9, gridArray[2, 2].GetSignalStr());


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

