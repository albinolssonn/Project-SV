using NUnit.Framework;

public class TestGridUtils
{

    [Test]
    public void BuildArray_Test()
    {
        Cell[,] grid = GridUtils.BuildArray(10, 5);
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        Assert.AreEqual(10, rows);
        Assert.AreEqual(5, cols);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Assert.IsTrue(grid[row, col].GetType() == typeof(Cell));
            }
        }
    }


    [Test]
    public void ResizeGrid_Test()
    {
        Cell[,] grid = GridUtils.BuildArray(10, 5);
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        Assert.AreEqual(10, rows);
        Assert.AreEqual(5, cols);


        Assert.AreEqual(0, grid[0, 0].GetCellContent().Count);

        grid[0, 0].AddCellContent(new House());

        Assert.AreEqual(1, grid[0, 0].GetCellContent().Count);


        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Assert.IsTrue(grid[row, col].GetType() == typeof(Cell));
            }
        }


        Cell[,] newGrid = GridUtils.ResizeGrid(grid, 2, 2, out _);

        int newRows = newGrid.GetLength(0);
        int newCols = newGrid.GetLength(1);
        Assert.AreEqual(2, newRows);
        Assert.AreEqual(2, newCols);

        Assert.AreEqual(1, grid[0, 0].GetCellContent().Count);

        Assert.AreEqual(0, newGrid[0, 1].GetCellContent().Count);
        Assert.AreEqual(0, newGrid[1, 0].GetCellContent().Count);
        Assert.AreEqual(0, newGrid[1, 1].GetCellContent().Count);




        grid[2, 0].AddCellContent(new Hospital());

        rows = 8;
        cols = 5;
        newGrid = GridUtils.ResizeGrid(grid, rows, cols, out _);

        newRows = newGrid.GetLength(0);
        newCols = newGrid.GetLength(1);
        Assert.AreEqual(8, newRows);
        Assert.AreEqual(5, newCols);

        Assert.AreEqual(1, grid[0, 0].GetCellContent().Count);
        Assert.AreEqual(1, grid[2, 0].GetCellContent().Count);



        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if ((row != 0 && col != 0) || (row != 2 && col != 0))
                {
                    Assert.AreEqual(0, newGrid[row, col].GetCellContent().Count);
                }

            }
        }
    }

}
