using System.Collections.Generic;


/// <summary>
/// Used to build a network across a grid.
/// </summary>
/// <remarks>
/// <para>The network is "built" by assigning the strongest signal strength to each cell based on the distance and modules in between the cell and the antenna providing the strongest signal.</para>
/// <para>The direction of the network flow is taken into account.</para>
/// </remarks>
public class Network
{
    private readonly List<Direction> directions; 
    private Cell[,] gridArray;
    private Cell startCell;

    private readonly double distancePenalty;
    private readonly double heightPenalty;
    private readonly double baseSignalStr;

    /// <summary>
    /// Used to build a network across a grid.
    /// </summary>
    /// <param name="baseSignalStr">The strength of which an Antenna transmits a signal with</param>
    /// <param name="distancePenalty">The reduction in signal strength for traveling one step in any direction.</param>
    /// <param name="heightPenalty">The reduction in signal strength for traveling through a cell with a higher max height than the cell which the antenna is located in.</param>
    public Network(double baseSignalStr, double distancePenalty, double heightPenalty)
    {
        directions = new List<Direction>() { new North_NorthEast(), new East_NorthEast(), new East_SouthEast(), new South_SouthEast(), new South_SouthWest(), new West_SouthWest(), new West_NorthWest(), new North_NorthWest() };
        this.baseSignalStr = baseSignalStr;
        this.distancePenalty = distancePenalty;
        this.heightPenalty = heightPenalty;
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
            if (cell.HasAntenna())
            {
                BuildNetworkFromCell(cell);
            }
        }
    }



    /// <summary>
    /// Should be called upon once for each cell that contains an Antenna.
    /// Spreads the network from the given Cell with the signal strength baseSignalStr.
    /// </summary>
    /// <param name="cell">The cell to use as an origin for the network</param>
    private void BuildNetworkFromCell(Cell cell)
    {
        startCell = gridArray[cell.GetY(), cell.GetX()];
        startCell.SetSignalIfHigher(baseSignalStr, new Origin(), false);

        foreach (Direction direction in directions)
        {
            SetSignalRecursively(direction, startCell, startCell.GetSignalStr() - distancePenalty);
        }
    }



    /// <summary>
    /// This helper-method is called to perform a recursive depth-first search to spread the network accordingly.
    /// </summary>
    /// <param name="direction">In which direction this iteration of the search is searching in. Possible directions extends the Direction class.</param>
    /// <param name="prevCell">The previous cell in the iteration.</param>
    /// <param name="incomingSignalStr">The signal strength passing through the previous cell</param>
    private void SetSignalRecursively(Direction direction, Cell prevCell, double incomingSignalStr)
    {
        List<Cell> neighbours = GridUtils.GetNearbyCells(prevCell.GetY(), prevCell.GetX(), gridArray);

        foreach (Cell nextCell in neighbours)
        {
            if (direction.CorrectDirection(nextCell, prevCell, out bool diagonal))
            {
                nextCell.SetSignalIfHigher(incomingSignalStr, direction, diagonal);

                SetSignalRecursively(direction, nextCell, GetNewStr(nextCell, incomingSignalStr));
            }
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
        double signalStrOut = signalStrIn  - distancePenalty;

        if (blockingCell.Equals(startCell))
        {
            return signalStrOut;
        }

        foreach (Module content in blockingCell.GetCellContent())
        {
            signalStrOut -= content.blockIndex();
        }

        if(startCell.GetMaxHeight() < blockingCell.GetMaxHeight())
        {
            signalStrOut -= heightPenalty; 
        }

        return signalStrOut;
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
    /// <summary>
    /// Checks if a signal can traverse from currentCell to nextCell.
    /// </summary>
    /// <param name="nextCell">The cell to travel to.</param>
    /// <param name="currentCell">The cell to travel from</param>
    /// <param name="diagonal">Output for it the resulting movement is done diagonally or not</param>
    /// <returns>True if the movement was possible given the direction, otherwise false.</returns>
    public abstract bool CorrectDirection(Cell nextCell, Cell currentCell, out bool diagonal);

    /// <summary>
    /// Gives the direction in degrees. Where 0 is North and 90 is West.
    /// </summary>
    /// <param name="diagonal">Which of the two directions of the object is desired.</param>
    /// <remarks>
    /// Every Direction object contains two directions. Ex. North_NorthEast where 'true' will give northeast and 'false' will give north.
    /// </remarks>
    /// <returns>The direction of the object in degrees.</returns>
    public abstract float GetDirectionInDegrees(bool diagonal);


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
    public override bool CorrectDirection(Cell nextCell, Cell currentCell, out bool diagonal)
    {
        diagonal = nextCell.GetY() < currentCell.GetY() && nextCell.GetX() > currentCell.GetX();
        return nextCell.GetY() < currentCell.GetY() && nextCell.GetX() >= currentCell.GetX();
    }

    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 315;
        }
        return 0;
    }

}

public class  East_NorthEast : Direction
{
    public override bool CorrectDirection(Cell nextCell, Cell currentCell, out bool diagonal)
    {
        diagonal = nextCell.GetY() < currentCell.GetY() && nextCell.GetX() > currentCell.GetX();
        return nextCell.GetY() <= currentCell.GetY() && nextCell.GetX() > currentCell.GetX();
    }

    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 315;
        }
        return 270;
    }
}

public class East_SouthEast : Direction
{
    public override bool CorrectDirection(Cell nextCell, Cell currentCell, out bool diagonal)
    {
        diagonal = nextCell.GetX() > currentCell.GetX() && nextCell.GetY() > currentCell.GetY();
        return nextCell.GetX() > currentCell.GetX() && nextCell.GetY() >= currentCell.GetY();
    }

    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 225;
        }
        return 270;
    }
}

public class South_SouthEast : Direction
{
    public override bool CorrectDirection(Cell nextCell, Cell currentCell, out bool diagonal)
    {
        diagonal = nextCell.GetX() > currentCell.GetX() && nextCell.GetY() > currentCell.GetY();
        return nextCell.GetX() >= currentCell.GetX() && nextCell.GetY() > currentCell.GetY();
    }

    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 225;
        }
        return 180;
    }
}

public class South_SouthWest : Direction
{
    public override bool CorrectDirection(Cell nextCell, Cell currentCell, out bool diagonal)
    {
        diagonal = nextCell.GetY() > currentCell.GetY() && nextCell.GetX() < currentCell.GetX();
        return nextCell.GetY() > currentCell.GetY() && nextCell.GetX() <= currentCell.GetX();
    }

    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 135;
        }
        return 180;
    }
}

public class West_SouthWest : Direction
{
    public override bool CorrectDirection(Cell nextCell, Cell currentCell, out bool diagonal)
    {
        diagonal = nextCell.GetX() < currentCell.GetX() && nextCell.GetY() > currentCell.GetY();
        return nextCell.GetX() < currentCell.GetX() && nextCell.GetY() >= currentCell.GetY();
    }

    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 135;
        }
        return 90;
    }
}

public class West_NorthWest : Direction
{
    public override bool CorrectDirection(Cell nextCell, Cell currentCell, out bool diagonal)
    {
        diagonal = nextCell.GetX() < currentCell.GetX() && nextCell.GetY() < currentCell.GetY();
        return nextCell.GetX() < currentCell.GetX() && nextCell.GetY() <= currentCell.GetY();
    }

    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 45;
        }
        return 90;
    }
}

public class North_NorthWest : Direction
{
    public override bool CorrectDirection(Cell nextCell, Cell currentCell, out bool diagonal)
    {
        diagonal = nextCell.GetY() < currentCell.GetY() && nextCell.GetX() < currentCell.GetX();
        return nextCell.GetY() < currentCell.GetY() && nextCell.GetX() <= currentCell.GetX();
    }

    public override float GetDirectionInDegrees(bool diagonal)
    {
        if (diagonal)
        {
            return 45;
        }
        return 0;
    }
}

public class Origin : Direction
{
    public override bool CorrectDirection(Cell nextCell, Cell currentCell, out bool diagonal)
    {
        throw new System.NotImplementedException();
    }

    public override float GetDirectionInDegrees(bool diagonal)
    {
        return 0;
    }

    public override string GetResourcePath()
    {
        return "Modules/Dot";
    }

}