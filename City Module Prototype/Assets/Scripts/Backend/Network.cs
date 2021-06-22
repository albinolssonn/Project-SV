using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network
{
    private readonly List<Direction> directions; 
    private Cell[,] gridArray;
    private Cell startCell; 
    
    public Network()
    {
        directions = new List<Direction>() { new North_NorthEast(), new East_NorthEast(), new East_SouthEast(), new South_SouthEast(), new South_SouthWest(), new West_SouthWest(), new West_NorthWest(), new North_NorthWest() };
    }


    public void BuildNetwork(Cell[,] gridArray, int startY, int startX)
    {
        this.gridArray = gridArray;
        startCell = gridArray[startY, startX];
        startCell.SetSignalStr(10);



        foreach (Direction direction in directions)
        {
            Traverse(direction, startCell);
        }
    }
   
    private void Traverse(Direction dir, Cell currentCell)
    {
        List<Cell> neighbours = GridUtils.GetNearbyCells(currentCell.GetY(), currentCell.GetX(), gridArray);

        foreach (Cell nextCell in neighbours)
        {
            if (dir.correctDirection(nextCell, currentCell))
            {
                nextCell.SetSignalIfHigher(GetNewStr(nextCell, currentCell.GetSignalStr()));
                Traverse(dir, nextCell);
            }
        }
    }

    private int GetNewStr(Cell cell, int signalStr)
    {
        int newStr = signalStr - 1;

        foreach (Module content in cell.GetCellContent())
        {
            newStr += content.modifier();
        }

        if(startCell.GetMaxHeight() < cell.GetMaxHeight())
        {
            newStr += -2; 
        }

        return newStr;
    }

}






public abstract class Direction
{
    public abstract bool correctDirection(Cell nextCell, Cell currentCell);
}


public class North_NorthEast : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetY() < currentCell.GetY() && nextCell.GetX() >= currentCell.GetX();
    }
}

public class  East_NorthEast : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetY() <= currentCell.GetY() && nextCell.GetX() > currentCell.GetX();
    }
}

public class East_SouthEast : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetX() > currentCell.GetX() && nextCell.GetY() >= currentCell.GetY();
    }
}

public class South_SouthEast : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetX() >= currentCell.GetX() && nextCell.GetY() > currentCell.GetY();
    }
}

public class South_SouthWest : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetY() > currentCell.GetY() && nextCell.GetX() <= currentCell.GetX();
    }
}

public class West_SouthWest : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetX() < currentCell.GetX() && nextCell.GetY() >= currentCell.GetY();
    }
}

public class West_NorthWest : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetX() < currentCell.GetX() && nextCell.GetY() <= currentCell.GetY();
    }
}

public class North_NorthWest : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetY() < currentCell.GetY() && nextCell.GetX() <= currentCell.GetX();
    }
}
