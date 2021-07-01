using System.Collections.Generic;


//This class is used to build the network of the grid of the type Cell[,] handed to the class in the method BuildNetwork().
//The network is "built" by assigning the correct signal strength to each cell depedning on the distance and modules in between the cell and the best antenna.
public class Network
{
    private readonly List<Direction> directions; 
    private Cell[,] gridArray;
    private Cell startCell; 
    
    public Network()
    {
        directions = new List<Direction>() { new North_NorthEast(), new East_NorthEast(), new East_SouthEast(), new South_SouthEast(), new South_SouthWest(), new West_SouthWest(), new West_NorthWest(), new North_NorthWest() };
    }

    /*
     * This method should be called upon once for each cell that contains an Antenna, with startX and startY being the coordinates for said cell.
     * It spreads the network from the cell gridArray[startY, startX] with the signal strength of that cell.
     * 
     * Cell[,] gridArray: This is the grid the method will spread the network across.
     * 
     * int startY: This is the y-coordinate which the method will use as origin for the spread.
     * 
     * int startX: This is the x-coordinate which the method will use as origin for the spread.
     * 
     * Returns: Nothing.
     */
    public void BuildNetwork(Cell[,] gridArray, int startY, int startX, double baseStationStr)
    {
        this.gridArray = gridArray;
        startCell = gridArray[startY, startX];
        startCell.SetSignalIfHigher(baseStationStr, new Origin(), false);



        foreach (Direction direction in directions)
        {
            Traverse(direction, startCell);
        }
    }


    /*
     * This helper-method is called to perform a recursive breadth-first search to spread the network accordingly.
     * 
     * Direction dir: In which direction this iteration of the search is searching in. 
     *                Possible directions are those defined each as a separate class at the bottom of this file.
     * 
     * Cell currentCell: The current cell in the iteration.
     * 
     *
     * Returns: Nothing.
     */
    private void Traverse(Direction direction, Cell currentCell)
    {
        List<Cell> neighbours = GridUtils.GetNearbyCells(currentCell.GetY(), currentCell.GetX(), gridArray);

        foreach (Cell nextCell in neighbours)
        {
            if (direction.CorrectDirection(nextCell, currentCell, out bool diagonal))
            {
                nextCell.SetSignalIfHigher(GetNewStr(currentCell, currentCell.GetSignalStr()), direction, diagonal);
                Traverse(direction, nextCell);
            }
        }
    }


    /*
     * This helper-method is called to calculate how much of the signal 'cell' will block for those behind it.
     * 
     * Cell cell: The cell which is blocking the signal.
     * 
     * double signalStr: The current strength of the signal passing through the cell.
     * 
     * Returns: The resulting signal strenght that passes through the tile 'cell'.
     */
    private double GetNewStr(Cell cell, double signalStr)
    {
        double newStr = signalStr - 1;

        if (cell.Equals(startCell))
        {
            return newStr;
        }

        foreach (Module content in cell.GetCellContent())
        {
            newStr += content.Modifier();
        }

        if(startCell.GetMaxHeight() < cell.GetMaxHeight())
        {
            newStr += -2; 
        }

        return newStr;
    }

}





/*
 * These below are the possible direction of which the signal can traverse through the system.
 * Note: The y-axis of the coordinate system is inverted. Going north results in decrease in y, going east results in increase in x.
 */
public abstract class Direction
{
    public abstract bool CorrectDirection(Cell nextCell, Cell currentCell, out bool diagonal);

    public abstract float GetDirectionArrowRotation(bool diagonal);

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

    public override float GetDirectionArrowRotation(bool diagonal)
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

    public override float GetDirectionArrowRotation(bool diagonal)
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

    public override float GetDirectionArrowRotation(bool diagonal)
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

    public override float GetDirectionArrowRotation(bool diagonal)
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

    public override float GetDirectionArrowRotation(bool diagonal)
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

    public override float GetDirectionArrowRotation(bool diagonal)
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

    public override float GetDirectionArrowRotation(bool diagonal)
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

    public override float GetDirectionArrowRotation(bool diagonal)
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

    public override float GetDirectionArrowRotation(bool diagonal)
    {
        return 0;
    }

    public override string GetResourcePath()
    {
        return "Modules/Dot";
    }

}
