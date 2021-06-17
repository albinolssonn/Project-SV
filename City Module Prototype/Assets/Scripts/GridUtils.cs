using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridUtils
{

    public static Cell[,] BuildArray(int rows, int cols)
    {
        Cell[,] gridArray = new Cell[rows, cols];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                gridArray[row, col] = new Cell(col, row);
            }
        }
        return gridArray;
    }


    public static ArrayList GetNearbyCells(int xIn, int yIn, Cell[,] gridArray)
    {
        Cell cell = gridArray[yIn, xIn];
        int rows = gridArray.GetLength(0);
        int cols = gridArray.GetLength(1);

        ArrayList neighbours = new ArrayList();

        int x = cell.GetX();
        int y = cell.GetY();

        if (x > 0)
        {
            neighbours.Add(gridArray[y, x - 1]);
        }

        if (x < cols - 1)
        {
            neighbours.Add(gridArray[y, x + 1]);
        }

        if (y > 0)
        {
            neighbours.Add(gridArray[y - 1, x]);
        }

        if (y < rows - 1)
        {
            neighbours.Add(gridArray[y + 1, x]);
        }

        if (x > 0 && y > 0)
        {
            neighbours.Add(gridArray[y - 1, x - 1]);
        }

        if (x < cols - 1 && y > 0)
        {
            neighbours.Add(gridArray[y - 1, x + 1]);
        }

        if (x < cols - 1 && y < rows - 1)
        {
            neighbours.Add(gridArray[y + 1, x + 1]);
        }

        if (x > 0 && y < rows - 1)
        {
            neighbours.Add(gridArray[y + 1, x - 1]);
        }

        return neighbours;
    }



}
