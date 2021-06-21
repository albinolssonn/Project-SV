using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS
{
    private readonly List<Direction> directions; 
    private Cell[,] gridArray;
    
    public BFS()
    {
        directions = new List<Direction>() { new North(), new NorthEast(), new East(), new SouthEast(), new South(), new SouthWest(), new West(), new NorthWest() };
    }


    public void BFSearch(Cell[,] gridArray, int startX, int startY)
    {
        this.gridArray = gridArray;
        Cell startCell = gridArray[startX, startY];
        int startingStrength = startCell.GetSignalStr();

        HashSet<Cell> neighbours = new HashSet<Cell>(GridUtils.GetNearbyCells(startCell.GetX(), startCell.GetY(), gridArray));

        foreach (Direction direction in directions)
        {
            Traverse(direction, startCell);
        }
    }
   
    private void Traverse(Direction dir, Cell currentCell)
    {
        List<Cell> neighbours = GridUtils.GetNearbyCells(currentCell.GetX(), currentCell.GetY(), gridArray);

        foreach (Cell nextCell in neighbours)
        {
            if (dir.correctDirection(nextCell, currentCell))
            {
                nextCell.SetSignalIfHigher(GetNewStr(nextCell, currentCell.GetSignalStr()));
                Traverse(dir, nextCell);
            }
        }
    }

    public int GetNewStr(Cell cell, int signalStr)
    {
        int newStr = signalStr;

        foreach (Module content in cell.GetCellContent())
        {
            newStr += content.modifier();
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
        return nextCell.GetY() < currentCell.GetY();
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
        return nextCell.GetX() > currentCell.GetX();
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
        return nextCell.GetY() >= currentCell.GetY();
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
        return nextCell.GetX() < currentCell.GetX();
    }
}

public class NorthWest : Direction
{
    public override bool correctDirection(Cell nextCell, Cell currentCell)
    {
        return nextCell.GetY() <= currentCell.GetY() && nextCell.GetX() <= currentCell.GetX();
    }
}
