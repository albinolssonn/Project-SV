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


    /// <summary>
    /// Creates a new grid array of the given size, transfering the old content to the new. Any content of the old grid which ends up outside the new grid is removed.
    /// </summary>
    /// <param name="oldArray">The grid array to transfer to the new.</param>
    /// <param name="newRows">The number of rows of the new grid array.</param>
    /// <param name="newCols">The number of columns of the new grid array.</param>
    /// <returns>The newly created grid array.</returns>
    public static Cell[,] ResizeGrid(Cell[,] oldArray, int newRows, int newCols, out List<Cell> antennaCells)
    {
        Cell[,] newArray = new Cell[newRows, newCols];
        antennaCells = new List<Cell>();

        for (int row = 0; row < newRows; row++)
        {
            for (int col = 0; col < newCols; col++)
            {
                if (row < oldArray.GetLength(0) && col < oldArray.GetLength(1))
                {
                    newArray[row, col] = oldArray[row, col];
                    newArray[row, col].ResetSignalStr();
                    if(newArray[row, col].HasAntenna())
                    {
                        antennaCells.Add(newArray[row, col]);
                    }
                }
                else
                {
                    newArray[row, col] = new Cell(row, col);
                }
            }
        }

        return newArray;
    }
}