using System.Collections.Generic;

/// <summary>
/// Contains utitilies used by the gridManager.
/// The reason these are in a class of their own is to be able to run automatic tests on them without having to start the entire program.
/// </summary>
public static class GridUtils
{

    /// <summary>
    /// Fills a nested array of (rows x cols) with instances of Cell.
    /// </summary>
    /// <param name="rows">Number of rows for resulting grid.</param>
    /// <param name="cols">Number of cols for resulting grid.</param>
    /// <returns>A nested array of Cell which is (rows x cols) large.</returns>
    public static Cell[,] BuildArray(int rows, int cols)
    {
        Cell[,] gridArray = new Cell[rows, cols];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                gridArray[row, col] = new Cell(row, col);
            }
        }
        return gridArray;
    }



    public static Cell[,] ResizeArray(Cell[,] oldArray, int newRows, int newCols)
    {
        Cell[,] newArray = new Cell[newRows, newCols];

        for (int row = 0; row < newRows; row++)
        {
            for (int col = 0; col < newCols; col++)
            {
                if(row < oldArray.GetLength(0) && col < oldArray.GetLength(1))
                {
                    newArray[row, col] = oldArray[row, col];
                    newArray[row, col].ResetSignalStr();
                } else
                {
                    newArray[row, col] = new Cell(row, col);
                }
            }
        }

        return newArray;
    }



    /// <summary>
    /// Gets a list of all neighbouring cells to the cell at gridArray[y,x].
    /// </summary>
    /// <param name="y">The y-coordinate for the origin cell.</param>
    /// <param name="x">The x-coordinate for the origin cell.</param>
    /// <param name="gridArray">The grid to apply the coordinates to.</param>
    /// <returns>A List of Cell containing all the neighbouring cells for that at gridArray[y,x].</returns>
    public static List<Cell> GetNearbyCells(int y, int x, Cell[,] gridArray)
    {
        Cell cell = gridArray[y, x];
        int rows = gridArray.GetLength(0);
        int cols = gridArray.GetLength(1);

        List<Cell> neighbours = new List<Cell>();


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