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
        directions = new List<Direction>() { new North(), new NorthEast(), new East(), new SouthEast(), new South(), new SouthWest(), new West(), new NorthWest() };
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
            if (dir.correctDirection(nextCell, currentCell) &&
                nextCell.SetSignalIfHigher(GetNewStr(nextCell, currentCell.GetSignalStr())))
            {
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


public class North : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetY() < currentCell.GetY() && nextCell.GetX() == currentCell.GetX();
    }
}

public class NorthEast : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetX() >= currentCell.GetX() && nextCell.GetY() <= currentCell.GetY();
    }
}

public class East : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetX() > currentCell.GetX() && nextCell.GetY() == currentCell.GetY();
    }
}

public class SouthEast : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetX() >= currentCell.GetX() && nextCell.GetY() >= currentCell.GetY();
    }
}

public class South : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetY() > currentCell.GetY() && nextCell.GetX() == currentCell.GetX();
    }
}

public class SouthWest : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetY() >= currentCell.GetY() && nextCell.GetX() <= currentCell.GetX();
    }
}

public class West : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetX() < currentCell.GetX() && nextCell.GetY() == currentCell.GetY();
    }
}

public class NorthWest : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetY() <= currentCell.GetY() && nextCell.GetX() <= currentCell.GetX();
    }
}
