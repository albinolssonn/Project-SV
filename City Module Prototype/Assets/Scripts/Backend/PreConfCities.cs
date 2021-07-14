// HACK: Definie or change pre-configured cities.
// Here you can alter existing pre-configured cities or create new ones.
// To create a new, simply create a new method and follow the pattern
// of the already existing pre-configured cities.
// Remember that the indexing on the array is 'gridArray[y, x]'.

/// <summary>
/// This class contains different pre-configured cities one is able to load up in the program.
/// </summary>
public static class PreConfCities
{

    /// <summary>
    /// One of several pre-configured cities to load up in the program.
    /// </summary>
    /// <param name="rows">This will be set to the number of rows of the resulting gridArray for the city.</param>
    /// <param name="cols">This will be set to the number of columns of the resulting gridArray for the city.</param>
    /// <returns>A gridArray of size (rows x cols) for the pre-configured city.</returns>
    public static Cell[,] GetConfig1(out int rows, out int cols)
    {
        rows = 7;
        cols = 6;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[0, 0].AddCellContent(new House());
        gridArray[0, 1].AddCellContent(new House());
        gridArray[0, 2].AddCellContent(new House());
        gridArray[0, 3].AddCellContent(new Hospital());
        gridArray[0, 4].AddCellContent(new House());
        gridArray[0, 5].AddCellContent(new House());

        gridArray[1, 0].AddCellContent(new House());
        gridArray[1, 1].AddCellContent(new House());
        gridArray[1, 2].AddCellContent(new House());
        gridArray[1, 3].AddCellContent(new Park());
        gridArray[1, 4].AddCellContent(new Park());
        gridArray[1, 5].AddCellContent(new House());

        gridArray[2, 0].AddCellContent(new TallBuilding());
        gridArray[2, 1].AddCellContent(new TallBuilding());
        gridArray[2, 2].AddCellContent(new TallBuilding());
        gridArray[2, 3].AddCellContent(new TallBuilding());
        gridArray[2, 4].AddCellContent(new TallBuilding());
        gridArray[2, 5].AddCellContent(new PoliceStation());

        gridArray[3, 0].AddCellContent(new TallBuilding());
        gridArray[3, 1].AddCellContent(new Park());
        gridArray[3, 2].AddCellContent(new TallBuilding());
        gridArray[3, 3].AddCellContent(new House());
        gridArray[3, 4].AddCellContent(new House());
        gridArray[3, 5].AddCellContent(new House());

        gridArray[4, 0].AddCellContent(new TallBuilding());
        gridArray[4, 1].AddCellContent(new TallBuilding());
        gridArray[4, 2].AddCellContent(new TallBuilding());
        gridArray[4, 3].AddCellContent(new TallBuilding());
        gridArray[4, 4].AddCellContent(new TallBuilding());
        gridArray[4, 5].AddCellContent(new FireDepartment());

        gridArray[5, 0].AddCellContent(new TallBuilding());
        gridArray[5, 1].AddCellContent(new Hospital());
        gridArray[5, 2].AddCellContent(new TallBuilding());
        gridArray[5, 3].AddCellContent(new Park());
        gridArray[5, 4].AddCellContent(new House());
        gridArray[5, 5].AddCellContent(new House());

        gridArray[6, 0].AddCellContent(new FireDepartment());
        gridArray[6, 1].AddCellContent(new House());
        gridArray[6, 2].AddCellContent(new House());
        gridArray[6, 3].AddCellContent(new House());
        gridArray[6, 4].AddCellContent(new House());
        gridArray[6, 5].AddCellContent(new House());

        return gridArray;
    }


    /// <summary>
    /// One of several pre-configured cities to load up in the program.
    /// </summary>
    /// <param name="rows">This will be set to the number of rows of the resulting gridArray for the city.</param>
    /// <param name="cols">This will be set to the number of columns of the resulting gridArray for the city.</param>
    /// <returns>A gridArray of size (rows x cols) for the pre-configured city.</returns>
    public static Cell[,] GetConfig2(out int rows, out int cols)
    {
        rows = 3;
        cols = 3;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[0, 0].AddCellContent(new TallBuilding());
        gridArray[0, 1].AddCellContent(new House());
        gridArray[0, 2].AddCellContent(new House());

        gridArray[1, 0].AddCellContent(new Park());
        gridArray[1, 1].AddCellContent(new House());
        gridArray[1, 2].AddCellContent(new Hospital());

        gridArray[2, 0].AddCellContent(new FireDepartment());
        gridArray[2, 1].AddCellContent(new House());
        gridArray[2, 2].AddCellContent(new PoliceStation());

        return gridArray;
    }


    /// <summary>
    /// One of several pre-configured cities to load up in the program.
    /// </summary>
    /// <param name="rows">This will be set to the number of rows of the resulting gridArray for the city.</param>
    /// <param name="cols">This will be set to the number of columns of the resulting gridArray for the city.</param>
    /// <returns>A gridArray of size (rows x cols) for the pre-configured city.</returns>
    public static Cell[,] GetConfig3(out int rows, out int cols)
    {
        rows = 8;
        cols = 4;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[0, 0].AddCellContent(new Park());
        gridArray[0, 1].AddCellContent(new Park());
        gridArray[0, 2].AddCellContent(new Park());
        gridArray[0, 3].AddCellContent(new TallBuilding());

        gridArray[1, 0].AddCellContent(new PoliceStation());
        gridArray[1, 1].AddCellContent(new Hospital());
        gridArray[1, 2].AddCellContent(new Park());
        gridArray[1, 3].AddCellContent(new FireDepartment());

        gridArray[2, 0].AddCellContent(new House());
        gridArray[2, 1].AddCellContent(new Park());
        gridArray[2, 2].AddCellContent(new Park());
        gridArray[2, 3].AddCellContent(new TallBuilding());

        gridArray[3, 0].AddCellContent(new House());
        gridArray[3, 1].AddCellContent(new House());
        gridArray[3, 2].AddCellContent(new TallBuilding());
        gridArray[3, 3].AddCellContent(new TallBuilding());

        gridArray[4, 0].AddCellContent(new TallBuilding());
        gridArray[4, 1].AddCellContent(new TallBuilding());
        gridArray[4, 2].AddCellContent(new TallBuilding());
        gridArray[4, 3].AddCellContent(new TallBuilding());

        gridArray[5, 0].AddCellContent(new Park());
        gridArray[5, 1].AddCellContent(new Park());
        gridArray[5, 2].AddCellContent(new TallBuilding());
        gridArray[5, 3].AddCellContent(new TallBuilding());

        gridArray[6, 0].AddCellContent(new TallBuilding());
        gridArray[6, 1].AddCellContent(new TallBuilding());
        gridArray[6, 2].AddCellContent(new FireDepartment());
        gridArray[6, 3].AddCellContent(new PoliceStation());

        gridArray[7, 0].AddCellContent(new Hospital());
        gridArray[7, 1].AddCellContent(new TallBuilding());
        gridArray[7, 2].AddCellContent(new Park());
        gridArray[7, 3].AddCellContent(new Park());

        return gridArray;
    }


    /// <summary>
    /// One of several pre-configured cities to load up in the program.
    /// </summary>
    /// <param name="rows">This will be set to the number of rows of the resulting gridArray for the city.</param>
    /// <param name="cols">This will be set to the number of columns of the resulting gridArray for the city.</param>
    /// <returns>A gridArray of size (rows x cols) for the pre-configured city.</returns>
    public static Cell[,] GetConfig4(out int rows, out int cols)
    {
        rows = 10;
        cols = 10;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        gridArray[0, 0].AddCellContent(new House());
        gridArray[0, 1].AddCellContent(new House());
        gridArray[0, 2].AddCellContent(new House());
        gridArray[0, 3].AddCellContent(new House());
        gridArray[0, 4].AddCellContent(new House());
        gridArray[0, 5].AddCellContent(new House());
        gridArray[0, 6].AddCellContent(new House());
        gridArray[0, 7].AddCellContent(new House());
        gridArray[0, 8].AddCellContent(new House());
        gridArray[0, 9].AddCellContent(new House());

        gridArray[1, 0].AddCellContent(new House());
        gridArray[1, 1].AddCellContent(new PoliceStation());
        gridArray[1, 2].AddCellContent(new House());
        gridArray[1, 3].AddCellContent(new Park());
        gridArray[1, 4].AddCellContent(new House());
        gridArray[1, 5].AddCellContent(new TallBuilding());
        gridArray[1, 6].AddCellContent(new TallBuilding());
        gridArray[1, 7].AddCellContent(new TallBuilding());
        gridArray[1, 8].AddCellContent(new TallBuilding());
        gridArray[1, 9].AddCellContent(new TallBuilding());

        gridArray[2, 0].AddCellContent(new House());
        gridArray[2, 1].AddCellContent(new House());
        gridArray[2, 2].AddCellContent(new House());
        gridArray[2, 3].AddCellContent(new House());
        gridArray[2, 4].AddCellContent(new Hospital());
        gridArray[2, 5].AddCellContent(new TallBuilding());
        gridArray[2, 6].AddCellContent(new Park());
        gridArray[2, 7].AddCellContent(new Park());
        gridArray[2, 8].AddCellContent(new Park());
        gridArray[2, 9].AddCellContent(new TallBuilding());

        gridArray[3, 0].AddCellContent(new TallBuilding());
        gridArray[3, 1].AddCellContent(new FireDepartment());
        gridArray[3, 2].AddCellContent(new TallBuilding());
        gridArray[3, 3].AddCellContent(new House());
        gridArray[3, 4].AddCellContent(new Park());
        gridArray[3, 5].AddCellContent(new TallBuilding());
        gridArray[3, 6].AddCellContent(new Park());
        gridArray[3, 7].AddCellContent(new Park());
        gridArray[3, 8].AddCellContent(new Park());
        gridArray[3, 9].AddCellContent(new TallBuilding());

        gridArray[4, 0].AddCellContent(new TallBuilding());
        gridArray[4, 1].AddCellContent(new TallBuilding());
        gridArray[4, 2].AddCellContent(new House());
        gridArray[4, 3].AddCellContent(new Park());
        gridArray[4, 4].AddCellContent(new TallBuilding());
        gridArray[4, 5].AddCellContent(new PoliceStation());
        gridArray[4, 6].AddCellContent(new Park());
        gridArray[4, 7].AddCellContent(new Park());
        gridArray[4, 8].AddCellContent(new Park());
        gridArray[4, 9].AddCellContent(new TallBuilding());

        gridArray[5, 0].AddCellContent(new TallBuilding());
        gridArray[5, 1].AddCellContent(new TallBuilding());
        gridArray[5, 2].AddCellContent(new House());
        gridArray[5, 3].AddCellContent(new House());
        gridArray[5, 4].AddCellContent(new TallBuilding());
        gridArray[5, 5].AddCellContent(new TallBuilding());
        gridArray[5, 6].AddCellContent(new TallBuilding());
        gridArray[5, 7].AddCellContent(new TallBuilding());
        gridArray[5, 8].AddCellContent(new TallBuilding());
        gridArray[5, 9].AddCellContent(new TallBuilding());


        gridArray[6, 0].AddCellContent(new House());
        gridArray[6, 1].AddCellContent(new House());
        gridArray[6, 2].AddCellContent(new House());
        gridArray[6, 3].AddCellContent(new House());
        gridArray[6, 4].AddCellContent(new House());
        gridArray[6, 5].AddCellContent(new TallBuilding());
        gridArray[6, 6].AddCellContent(new TallBuilding());
        gridArray[6, 7].AddCellContent(new TallBuilding());
        gridArray[6, 8].AddCellContent(new TallBuilding());
        gridArray[6, 9].AddCellContent(new TallBuilding());

        gridArray[7, 0].AddCellContent(new FireDepartment());
        gridArray[7, 1].AddCellContent(new House());
        gridArray[7, 2].AddCellContent(new House());
        gridArray[7, 3].AddCellContent(new House());
        gridArray[7, 4].AddCellContent(new House());
        gridArray[7, 5].AddCellContent(new House());
        gridArray[7, 6].AddCellContent(new Hospital());
        gridArray[7, 7].AddCellContent(new House());
        gridArray[7, 8].AddCellContent(new House());
        gridArray[7, 9].AddCellContent(new House());

        gridArray[8, 0].AddCellContent(new House());
        gridArray[8, 1].AddCellContent(new House());
        gridArray[8, 2].AddCellContent(new House());
        gridArray[8, 3].AddCellContent(new House());
        gridArray[8, 4].AddCellContent(new Park());
        gridArray[8, 5].AddCellContent(new Park());
        gridArray[8, 6].AddCellContent(new House());
        gridArray[8, 7].AddCellContent(new House());
        gridArray[8, 8].AddCellContent(new House());
        gridArray[8, 9].AddCellContent(new House());

        gridArray[9, 0].AddCellContent(new House());
        gridArray[9, 1].AddCellContent(new PoliceStation());
        gridArray[9, 2].AddCellContent(new Park());
        gridArray[9, 3].AddCellContent(new House());
        gridArray[9, 4].AddCellContent(new House());
        gridArray[9, 5].AddCellContent(new House());
        gridArray[9, 6].AddCellContent(new House());
        gridArray[9, 7].AddCellContent(new House());
        gridArray[9, 8].AddCellContent(new Park());
        gridArray[9, 9].AddCellContent(new House());

        return gridArray;
    }
}
