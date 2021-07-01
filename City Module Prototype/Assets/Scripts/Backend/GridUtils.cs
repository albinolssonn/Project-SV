using System.Collections.Generic;

/*
 * This class contains utitilies used by the gridManager. The reason these are in a class of their own is to be able to run automatic tests on them
 * without having to start the entire program.
 */
public static class GridUtils
{
    /*
     * Fills a nested array of (rows x cols) with instances of Cell.
     * 
     * int rows: number of rows for resulting grid.
     * 
     * int cols: number of cols for resulting grid.
     * 
     * Returns: A nested array Cell[,] which is (rows x cols) large.
     */
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


    /*
     * Gets a list of all neighbouring cells to the cell at gridArray[y,x].
     * 
     * int y: The y-coordinate for the origin cell.
     * 
     * int x: The x-coordinate for the origin cell.
     * 
     * Cell[,]: The grid to apply the coordinates to.
     * 
     * Returns: A List<Cell> containing all the neighbouring cells for the one at gridArray[y,x].
     */
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