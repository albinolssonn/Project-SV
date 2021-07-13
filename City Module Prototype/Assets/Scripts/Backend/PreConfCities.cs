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
        Cell[,] config = GridUtils.BuildArray(rows, cols);

        config[0, 0].AddCellContent(new House());
        config[0, 1].AddCellContent(new House());
        config[0, 2].AddCellContent(new House());
        config[0, 3].AddCellContent(new Hospital());
        config[0, 4].AddCellContent(new House());
        config[0, 5].AddCellContent(new House());

        config[1, 0].AddCellContent(new House());
        config[1, 1].AddCellContent(new House());
        config[1, 2].AddCellContent(new House());
        config[1, 3].AddCellContent(new Park());
        config[1, 4].AddCellContent(new Park());
        config[1, 5].AddCellContent(new House());

        config[2, 0].AddCellContent(new TallBuilding());
        config[2, 1].AddCellContent(new TallBuilding());
        config[2, 2].AddCellContent(new TallBuilding());
        config[2, 3].AddCellContent(new TallBuilding());
        config[2, 4].AddCellContent(new TallBuilding());
        config[2, 5].AddCellContent(new PoliceStation());

        config[3, 0].AddCellContent(new TallBuilding());
        config[3, 1].AddCellContent(new Park());
        config[3, 2].AddCellContent(new TallBuilding());
        config[3, 3].AddCellContent(new House());
        config[3, 4].AddCellContent(new House());
        config[3, 5].AddCellContent(new House());

        config[4, 0].AddCellContent(new TallBuilding());
        config[4, 1].AddCellContent(new TallBuilding());
        config[4, 2].AddCellContent(new TallBuilding());
        config[4, 3].AddCellContent(new TallBuilding());
        config[4, 4].AddCellContent(new TallBuilding());
        config[4, 5].AddCellContent(new FireDepartment());

        config[5, 0].AddCellContent(new TallBuilding());
        config[5, 1].AddCellContent(new Hospital());
        config[5, 2].AddCellContent(new TallBuilding());
        config[5, 3].AddCellContent(new Park());
        config[5, 4].AddCellContent(new House());
        config[5, 5].AddCellContent(new House());

        config[6, 0].AddCellContent(new FireDepartment());
        config[6, 1].AddCellContent(new House());
        config[6, 2].AddCellContent(new House());
        config[6, 3].AddCellContent(new House());
        config[6, 4].AddCellContent(new House());
        config[6, 5].AddCellContent(new House());

        return config;
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
        Cell[,] config = GridUtils.BuildArray(rows, cols);

        config[0, 0].AddCellContent(new TallBuilding());
        config[0, 1].AddCellContent(new House());
        config[0, 2].AddCellContent(new House());

        config[1, 0].AddCellContent(new Park());
        config[1, 1].AddCellContent(new House());
        config[1, 2].AddCellContent(new Hospital());

        config[2, 0].AddCellContent(new FireDepartment());
        config[2, 1].AddCellContent(new House());
        config[2, 2].AddCellContent(new PoliceStation());

        return config;
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
        Cell[,] config = GridUtils.BuildArray(rows, cols);

        config[0, 0].AddCellContent(new Park());
        config[0, 1].AddCellContent(new Park());
        config[0, 2].AddCellContent(new Park());
        config[0, 3].AddCellContent(new TallBuilding());

        config[1, 0].AddCellContent(new PoliceStation());
        config[1, 1].AddCellContent(new Hospital());
        config[1, 2].AddCellContent(new Park());
        config[1, 3].AddCellContent(new FireDepartment());

        config[2, 0].AddCellContent(new House());
        config[2, 1].AddCellContent(new Park());
        config[2, 2].AddCellContent(new Park());
        config[2, 3].AddCellContent(new TallBuilding());

        config[3, 0].AddCellContent(new House());
        config[3, 1].AddCellContent(new House());
        config[3, 2].AddCellContent(new TallBuilding());
        config[3, 3].AddCellContent(new TallBuilding());

        config[4, 0].AddCellContent(new TallBuilding());
        config[4, 1].AddCellContent(new TallBuilding());
        config[4, 2].AddCellContent(new TallBuilding());
        config[4, 3].AddCellContent(new TallBuilding());

        config[5, 0].AddCellContent(new Park());
        config[5, 1].AddCellContent(new Park());
        config[5, 2].AddCellContent(new TallBuilding());
        config[5, 3].AddCellContent(new TallBuilding());

        config[6, 0].AddCellContent(new TallBuilding());
        config[6, 1].AddCellContent(new TallBuilding());
        config[6, 2].AddCellContent(new FireDepartment());
        config[6, 3].AddCellContent(new PoliceStation());

        config[7, 0].AddCellContent(new Hospital());
        config[7, 1].AddCellContent(new TallBuilding());
        config[7, 2].AddCellContent(new Park());
        config[7, 3].AddCellContent(new Park());

        return config;
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
        Cell[,] config = GridUtils.BuildArray(rows, cols);

        config[0, 0].AddCellContent(new House());
        config[0, 1].AddCellContent(new House());
        config[0, 2].AddCellContent(new House());
        config[0, 3].AddCellContent(new House());
        config[0, 4].AddCellContent(new House());
        config[0, 5].AddCellContent(new House());
        config[0, 6].AddCellContent(new House());
        config[0, 7].AddCellContent(new House());
        config[0, 8].AddCellContent(new House());
        config[0, 9].AddCellContent(new House());

        config[1, 0].AddCellContent(new House());
        config[1, 1].AddCellContent(new PoliceStation());
        config[1, 2].AddCellContent(new House());
        config[1, 3].AddCellContent(new Park());
        config[1, 4].AddCellContent(new House());
        config[1, 5].AddCellContent(new TallBuilding());
        config[1, 6].AddCellContent(new TallBuilding());
        config[1, 7].AddCellContent(new TallBuilding());
        config[1, 8].AddCellContent(new TallBuilding());
        config[1, 9].AddCellContent(new TallBuilding());

        config[2, 0].AddCellContent(new House());
        config[2, 1].AddCellContent(new House());
        config[2, 2].AddCellContent(new House());
        config[2, 3].AddCellContent(new House());
        config[2, 4].AddCellContent(new Hospital());
        config[2, 5].AddCellContent(new TallBuilding());
        config[2, 6].AddCellContent(new Park());
        config[2, 7].AddCellContent(new Park());
        config[2, 8].AddCellContent(new Park());
        config[2, 9].AddCellContent(new TallBuilding());

        config[3, 0].AddCellContent(new TallBuilding());
        config[3, 1].AddCellContent(new FireDepartment());
        config[3, 2].AddCellContent(new TallBuilding());
        config[3, 3].AddCellContent(new House());
        config[3, 4].AddCellContent(new Park());
        config[3, 5].AddCellContent(new TallBuilding());
        config[3, 6].AddCellContent(new Park());
        config[3, 7].AddCellContent(new Park());
        config[3, 8].AddCellContent(new Park());
        config[3, 9].AddCellContent(new TallBuilding());

        config[4, 0].AddCellContent(new TallBuilding());
        config[4, 1].AddCellContent(new TallBuilding());
        config[4, 2].AddCellContent(new House());
        config[4, 3].AddCellContent(new Park());
        config[4, 4].AddCellContent(new TallBuilding());
        config[4, 5].AddCellContent(new PoliceStation());
        config[4, 6].AddCellContent(new Park());
        config[4, 7].AddCellContent(new Park());
        config[4, 8].AddCellContent(new Park());
        config[4, 9].AddCellContent(new TallBuilding());

        config[5, 0].AddCellContent(new TallBuilding());
        config[5, 1].AddCellContent(new TallBuilding());
        config[5, 2].AddCellContent(new House());
        config[5, 3].AddCellContent(new House());
        config[5, 4].AddCellContent(new TallBuilding());
        config[5, 5].AddCellContent(new TallBuilding());
        config[5, 6].AddCellContent(new TallBuilding());
        config[5, 7].AddCellContent(new TallBuilding());
        config[5, 8].AddCellContent(new TallBuilding());
        config[5, 9].AddCellContent(new TallBuilding());


        config[6, 0].AddCellContent(new House());
        config[6, 1].AddCellContent(new House());
        config[6, 2].AddCellContent(new House());
        config[6, 3].AddCellContent(new House());
        config[6, 4].AddCellContent(new House());
        config[6, 5].AddCellContent(new TallBuilding());
        config[6, 6].AddCellContent(new TallBuilding());
        config[6, 7].AddCellContent(new TallBuilding());
        config[6, 8].AddCellContent(new TallBuilding());
        config[6, 9].AddCellContent(new TallBuilding());

        config[7, 0].AddCellContent(new FireDepartment());
        config[7, 1].AddCellContent(new House());
        config[7, 2].AddCellContent(new House());
        config[7, 3].AddCellContent(new House());
        config[7, 4].AddCellContent(new House());
        config[7, 5].AddCellContent(new House());
        config[7, 6].AddCellContent(new Hospital());
        config[7, 7].AddCellContent(new House());
        config[7, 8].AddCellContent(new House());
        config[7, 9].AddCellContent(new House());

        config[8, 0].AddCellContent(new House());
        config[8, 1].AddCellContent(new House());
        config[8, 2].AddCellContent(new House());
        config[8, 3].AddCellContent(new House());
        config[8, 4].AddCellContent(new Park());
        config[8, 5].AddCellContent(new Park());
        config[8, 6].AddCellContent(new House());
        config[8, 7].AddCellContent(new House());
        config[8, 8].AddCellContent(new House());
        config[8, 9].AddCellContent(new House());

        config[9, 0].AddCellContent(new House());
        config[9, 1].AddCellContent(new PoliceStation());
        config[9, 2].AddCellContent(new Park());
        config[9, 3].AddCellContent(new House());
        config[9, 4].AddCellContent(new House());
        config[9, 5].AddCellContent(new House());
        config[9, 6].AddCellContent(new House());
        config[9, 7].AddCellContent(new House());
        config[9, 8].AddCellContent(new Park());
        config[9, 9].AddCellContent(new House());

        return config;
    }
}
