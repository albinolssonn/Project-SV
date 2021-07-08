using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

/// <summary>
/// Used to build a network across a grid.
/// </summary>
/// <remarks>
/// <para>The network is "built" by assigning the strongest signal strength to each cell based on the distance and modules in between the cell and the antenna providing the strongest signal.</para>
/// <para>The direction of the network flow is taken into account.</para>
/// </remarks>
public class Network
{
    private Cell[,] gridArray;
    private Cell startCell;
    private readonly List<int> networkColorsOccurences;

    private readonly double distancePenalty;
    private readonly double heightPenalty;
    private readonly double baseSignalStr;


    /// <summary>The different colors which the network flow is displayed with.</summary>
    public static readonly List<Color> networkFlowColors = new List<Color>() { Color.black, Color.blue, new Color(0.5f, 0f, 1f, 1f), new Color(1f, 0.5f, 0f, 1f), Color.magenta, Color.gray, Color.cyan, Color.red, Color.yellow };


    /// <summary>
    /// Used to build a network across a grid.
    /// </summary>
    /// <param name="baseSignalStr">The strength of which an Antenna transmits a signal with</param>
    /// <param name="distancePenalty">The reduction in signal strength for traveling one step in any direction.</param>
    /// <param name="heightPenalty">The reduction in signal strength for traveling through a cell with a higher max height than the cell which the antenna is located in.</param>
    public Network(double baseSignalStr, double distancePenalty, double heightPenalty)
    {
        this.baseSignalStr = baseSignalStr;
        this.distancePenalty = distancePenalty;
        this.heightPenalty = heightPenalty;
        networkColorsOccurences = new List<int>();
        networkColorsOccurences.AddRange(Enumerable.Repeat(0, networkFlowColors.Count));
    }



    /// <summary>
    /// Builds the network based on the grid provided.
    /// </summary>
    /// <param name="gridArray">The grid to build the network across.</param>
    public void BuildNetwork(Cell[,] gridArray)
    {
        this.gridArray = gridArray;


        foreach (Cell cell in gridArray)
        {
            if (cell.GetAntenna() != null)
            {
                BuildNetworkFromCell(cell);
            }
        }
    }



    /// <summary>
    /// Spreads the network from the given Cell with the signal strength baseSignalStr.
    /// </summary>
    /// <param name="cell">The cell to use as an origin for the network</param>
    public void BuildNetworkFromCell(Cell cell)
    {
        List<Direction> directions = new List<Direction>() { new North_NorthEast(cell), new East_NorthEast(cell), new East_SouthEast(cell), new South_SouthEast(cell), new South_SouthWest(cell), new West_SouthWest(cell), new West_NorthWest(cell), new North_NorthWest(cell) };

        startCell = gridArray[cell.GetY(), cell.GetX()];

        if (!(startCell.GetSignalDir() is Origin))
        {
            startCell.SetSignalIfHigher(baseSignalStr, new Origin(cell, NextColorIndex(), networkColorsOccurences), false);
        }

        foreach (Direction direction in directions)
        {
            SetSignalRecursively(direction, startCell, startCell.GetSignalStr() - distancePenalty);
        }
    }



    /// <summary>
    /// This helper-method is called to perform a recursive depth-first search to spread the network accordingly.
    /// </summary>
    /// <param name="direction">In which direction this iteration of the search is searching in. Possible directions extends the Direction class.</param>
    /// <param name="currentCell">The previous cell in the iteration.</param>
    /// <param name="incomingSignalStr">The signal strength passing through the previous cell</param>
    private void SetSignalRecursively(Direction direction, Cell currentCell, double incomingSignalStr)
    {
        List<Cell> neighbours = direction.GetNearbyCells(currentCell, gridArray);

        foreach (Cell nextCell in neighbours)
        {

            nextCell.SetSignalIfHigher(incomingSignalStr, direction, direction.IsDiagonally(nextCell, currentCell));

            SetSignalRecursively(direction, nextCell, GetNewStr(nextCell, incomingSignalStr));

        }
    }


    /// <summary>
    /// This helper-method is called to calculate how much of the signal a cell will block for those behind it.
    /// </summary>
    /// <param name="blockingCell">The cell which the signal is passing through.</param>
    /// <param name="signalStrIn">The signal strength going into blockingCell.</param>
    /// <returns>The signal strength coming out from blockingCell.</returns>
    private double GetNewStr(Cell blockingCell, double signalStrIn)
    {
        double signalStrOut = signalStrIn - distancePenalty;

        if (blockingCell.Equals(startCell))
        {
            return signalStrOut;
        }

        foreach (Module content in blockingCell.GetCellContent())
        {
            signalStrOut -= content.BlockIndex();
        }

        if (startCell.GetHeight() < blockingCell.GetHeight())
        {
            signalStrOut -= heightPenalty;
        }

        return signalStrOut;
    }


    /// <summary>
    /// Calculates the next index of the least used network flow color.
    /// </summary>
    /// <returns>The index of the least used network flow color.</returns>
    private int NextColorIndex()
    {
        int nextIndex = 0;
        int minOccureance = int.MaxValue;
        for (int i = 0; i < networkColorsOccurences.Count; i++)
        {
            if (networkColorsOccurences[i] < minOccureance)
            {
                nextIndex = i;
                minOccureance = networkColorsOccurences[i];
                if (minOccureance == 0)
                {
                    networkColorsOccurences[i]++;
                    return i;
                }
            }
        }

        networkColorsOccurences[nextIndex]++;
        return nextIndex;
    }
}









/// <summary>
/// Possible direction of which a signal can traverse through the network is restricted by those implementing this class.
/// </summary>
/// <remarks>
/// The y-axis of the coordinate system is inverted along y-axis. Going north results in decrease in y, going east results in increase in x.
/// </remarks>
public abstract class Direction
{

    public readonly Cell originCell;

    public Direction(Cell originCell)
    {
        this.originCell = originCell;
    }


    /// <summary>
    /// Checks if a signal can traverse from currentCell to nextCell.
    /// </summary>
    /// <param name="nextCell">The cell to travel to.</param>
    /// <param name="currentCell">The cell to travel from</param>
    /// 
    /// <returns>True if the movement was possible given the direction, otherwise false.</returns>
    public abstract bool IsDiagonally(Cell nextCell, Cell currentCell);


    /// <summary>
    /// Gives the direction in degrees. Where 0 is North and 90 is West.
    /// </summary>
    /// <param name="diagonal">Which of the two directions of the object is desired.</param>
    /// <remarks>
    /// Every Direction object contains two directions. Ex. North_NorthEast where 'true' will give northeast and 'false' will give north.
    /// </remarks>
    /// <returns>The direction of the object in degrees.</returns>
    public abstract float GetDirectionInDegrees(bool diagonal);


    /// <summary>
    /// Find all neighbours for the given cell in the direction matching the Direction type.
    /// </summary>
    /// <param name="cell">The cell of which neighbours one is looking for.</param>
    /// <param name="gridArray">The grid where the cells are located in.</param>
    /// <returns>A List containing all the neighbouring Cells for the given cell.</returns>
    public abstract List<Cell> GetNearbyCells(Cell cell, Cell[,] gridArray);


    /// <returns>
    /// The file path for the resource used to visualise the direction flow on the grid.
    /// </returns>
    public virtual string GetResourcePath()
    {
        return "Modules/Arrow";
    }

}


public class North_NorthEast : Direction
{

    public North_NorthEast(Cell cellOrigin) : base(cellOrigin) { }


    public override bool IsDiagonally(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetY() < currentCell.GetY() && nextCell.GetX() > currentCell.GetX();
    }


    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 315;
        }
        return 0;
    }


    public override List<Cell> GetNearbyCells(Cell cell, Cell[,] gridArray)
    {
        int y = cell.GetY();
        int x = cell.GetX();
        int cols = gridArray.GetLength(1);

        List<Cell> neighbours = new List<Cell>();

        if (y > 0)
        {
            neighbours.Add(gridArray[y - 1, x]);
        }

        if (y > 0 && x < cols - 1)
        {
            neighbours.Add(gridArray[y - 1, x + 1]);
        }

        return neighbours;
    }
}

public class East_NorthEast : Direction
{
    public East_NorthEast(Cell cellOrigin) : base(cellOrigin) { }


    public override bool IsDiagonally(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetY() < currentCell.GetY() && nextCell.GetX() > currentCell.GetX();
    }

    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 315;
        }
        return 270;
    }


    public override List<Cell> GetNearbyCells(Cell cell, Cell[,] gridArray)
    {
        int y = cell.GetY();
        int x = cell.GetX();
        int cols = gridArray.GetLength(1);

        List<Cell> neighbours = new List<Cell>();

        if (x < cols - 1)
        {
            neighbours.Add(gridArray[y, x + 1]);
        }

        if (y > 0 && x < cols - 1)
        {
            neighbours.Add(gridArray[y - 1, x + 1]);
        }

        return neighbours;
    }
}

public class East_SouthEast : Direction
{
    public East_SouthEast(Cell cellOrigin) : base(cellOrigin) { }


    public override bool IsDiagonally(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetX() > currentCell.GetX() && nextCell.GetY() > currentCell.GetY();
    }


    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 225;
        }
        return 270;
    }


    public override List<Cell> GetNearbyCells(Cell cell, Cell[,] gridArray)
    {
        int y = cell.GetY();
        int x = cell.GetX();
        int rows = gridArray.GetLength(0);
        int cols = gridArray.GetLength(1);

        List<Cell> neighbours = new List<Cell>();

        if (x < cols - 1)
        {
            neighbours.Add(gridArray[y, x + 1]);
        }

        if (y < rows - 1 && x < cols - 1)
        {
            neighbours.Add(gridArray[y + 1, x + 1]);
        }

        return neighbours;
    }
}

public class South_SouthEast : Direction
{
    public South_SouthEast(Cell cellOrigin) : base(cellOrigin) { }


    public override bool IsDiagonally(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetX() > currentCell.GetX() && nextCell.GetY() > currentCell.GetY();
    }


    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 225;
        }
        return 180;
    }


    public override List<Cell> GetNearbyCells(Cell cell, Cell[,] gridArray)
    {
        int y = cell.GetY();
        int x = cell.GetX();
        int rows = gridArray.GetLength(0);
        int cols = gridArray.GetLength(1);

        List<Cell> neighbours = new List<Cell>();

        if (y < rows - 1)
        {
            neighbours.Add(gridArray[y + 1, x]);
        }


        if (y < rows - 1 && x < cols - 1)
        {
            neighbours.Add(gridArray[y + 1, x + 1]);
        }

        return neighbours;
    }
}

public class South_SouthWest : Direction
{
    public South_SouthWest(Cell cellOrigin) : base(cellOrigin) { }


    public override bool IsDiagonally(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetY() > currentCell.GetY() && nextCell.GetX() < currentCell.GetX();
    }


    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 135;
        }
        return 180;
    }


    public override List<Cell> GetNearbyCells(Cell cell, Cell[,] gridArray)
    {
        int y = cell.GetY();
        int x = cell.GetX();
        int rows = gridArray.GetLength(0);

        List<Cell> neighbours = new List<Cell>();

        if (y < rows - 1)
        {
            neighbours.Add(gridArray[y + 1, x]);
        }

        if (y < rows - 1 && x > 0)
        {
            neighbours.Add(gridArray[y + 1, x - 1]);
        }

        return neighbours;
    }
}

public class West_SouthWest : Direction
{
    public West_SouthWest(Cell cellOrigin) : base(cellOrigin) { }


    public override bool IsDiagonally(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetX() < currentCell.GetX() && nextCell.GetY() > currentCell.GetY();
    }


    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 135;
        }
        return 90;
    }


    public override List<Cell> GetNearbyCells(Cell cell, Cell[,] gridArray)
    {
        int y = cell.GetY();
        int x = cell.GetX();
        int rows = gridArray.GetLength(0);

        List<Cell> neighbours = new List<Cell>();

        if (x > 0)
        {
            neighbours.Add(gridArray[y, x - 1]);
        }

        if (y < rows - 1 && x > 0)
        {
            neighbours.Add(gridArray[y + 1, x - 1]);
        }

        return neighbours;
    }
}

public class West_NorthWest : Direction
{
    public West_NorthWest(Cell cellOrigin) : base(cellOrigin) { }


    public override bool IsDiagonally(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetX() < currentCell.GetX() && nextCell.GetY() < currentCell.GetY();
    }


    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 45;
        }
        return 90;
    }


    public override List<Cell> GetNearbyCells(Cell cell, Cell[,] gridArray)
    {
        int y = cell.GetY();
        int x = cell.GetX();

        List<Cell> neighbours = new List<Cell>();

        if (x > 0)
        {
            neighbours.Add(gridArray[y, x - 1]);
        }

        if (y > 0 && x > 0)
        {
            neighbours.Add(gridArray[y - 1, x - 1]);
        }

        return neighbours;
    }
}

public class North_NorthWest : Direction
{
    public North_NorthWest(Cell cellOrigin) : base(cellOrigin) { }


    public override bool IsDiagonally(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetY() < currentCell.GetY() && nextCell.GetX() < currentCell.GetX();
    }


    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 45;
        }
        return 0;
    }


    public override List<Cell> GetNearbyCells(Cell cell, Cell[,] gridArray)
    {
        int y = cell.GetY();
        int x = cell.GetX();

        List<Cell> neighbours = new List<Cell>();

        if (y > 0)
        {
            neighbours.Add(gridArray[y - 1, x]);
        }

        if (y > 0 && x > 0)
        {
            neighbours.Add(gridArray[y - 1, x - 1]);
        }

        return neighbours;
    }
}

public class Origin : Direction
{
    public readonly int networkFlowColorIndex;
    private readonly List<int> networkColorsOccurences;

    public Origin(Cell cellOrigin, int networkFlowColorIndex, List<int> networkColorsOccurences) : base(cellOrigin)
    {
        this.networkFlowColorIndex = networkFlowColorIndex;
        this.networkColorsOccurences = networkColorsOccurences;
    }

    public void OriginRemoved()
    {
        networkColorsOccurences[networkFlowColorIndex]--;
    }



    public override bool IsDiagonally(Cell nextCell, Cell currentCell)
    {
        throw new NotImplementedException();
    }


    public override float GetDirectionInDegrees(bool diagonal)
    {
        return 0;
    }


    public override string GetResourcePath()
    {
        return "Modules/Dot";
    }

    public override List<Cell> GetNearbyCells(Cell cell, Cell[,] gridArray)
    {
        throw new NotImplementedException();
    }
}