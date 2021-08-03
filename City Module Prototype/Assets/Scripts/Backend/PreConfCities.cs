using System.Collections.Generic;
using System.IO;

/// <summary>
/// This class loads pre-configured cities from text files.
/// </summary>
public static class PreConfCities
{


    /// <summary>
    /// Loads a pre-configured city from a text file.
    /// </summary>
    /// <param name="rows">Number of rows of the new grid.</param>
    /// <param name="cols">Number of columns of the new grid.</param>
    /// <param name="antennaCells">List of the cells containing Antennas in the new grid.</param>
    /// <param name="gridManager">The gridManager to send error messages with.</param>
    /// <param name="fileName">The file name to load the pre-configured city from.</param>
    /// <returns>The new grid.</returns>
    public static Cell[,] LoadFromFile(out int rows, out int cols, out List<Cell> antennaCells, GridManager gridManager, string fileName)
    {
        try
        {
            StreamReader inputStream = new StreamReader(Directory.GetCurrentDirectory() + "/ConfigFiles/" + fileName + ".txt");
            if (!inputStream.EndOfStream)
            {
                rows = int.Parse(inputStream.ReadLine().Split(',')[1]);

                cols = int.Parse(inputStream.ReadLine().Split(',')[1]);
            }
            else
            {
                gridManager.SetMessage("Config file was empty.");
                return Crashed(out rows, out cols, out antennaCells);
            }


            Cell[,] gridArray = GridUtils.BuildArray(rows, cols);
            antennaCells = new List<Cell>();

            while (!inputStream.EndOfStream)
            {
                string inputLine = inputStream.ReadLine();
                string[] inputs = inputLine.Split(' ');

                string[] xy = inputs[0].Split(',');
                int x = int.Parse(xy[0]);
                int y = int.Parse(xy[1]);


                string moduleInput = inputs[1];
                Module module = ToModule(moduleInput, gridManager);


                if (module != null)
                {
                    gridArray[y, x].AddCellContent(module);
                    if (module is Antenna)
                    {
                        antennaCells.Add(gridArray[y, x]);
                    }
                }
            }

            inputStream.Close();

            return gridArray;
        }
        catch (DirectoryNotFoundException)
        {
            gridManager.SetMessage("A directory in the given path was not found.");
            return Crashed(out rows, out cols, out antennaCells);
        }
        catch (FileNotFoundException)
        {
            gridManager.SetMessage("File was not found.");
            return Crashed(out rows, out cols, out antennaCells);
        }
        catch (IOException)
        {
            gridManager.SetMessage("Could not load file.");
            return Crashed(out rows, out cols, out antennaCells);
        }
        catch (System.FormatException)
        {
            gridManager.SetMessage("Config file was in wrong format.");
            return Crashed(out rows, out cols, out antennaCells);
        }

    }

    /// <summary>
    /// Sets variables to standard values.
    /// </summary>
    /// <param name="rows">The rows variable.</param>
    /// <param name="cols">The cols variable</param>
    /// <param name="antennaCells">The antennaCells list.</param>
    /// <returns>A grid of empty cells.</returns>
    private static Cell[,] Crashed(out int rows, out int cols, out List<Cell> antennaCells)
    {
        rows = 2;
        cols = 2;
        antennaCells = new List<Cell>();

        return GridUtils.BuildArray(rows, cols);
    }


    /// <summary>
    /// Creates a Module matching the given string.
    /// </summary>
    /// <param name="moduleString">The string to create matching Module from.</param>
    /// <param name="gridManager">The gridManager used for error messages.</param>
    /// <returns></returns>
    private static Module ToModule(string moduleString, GridManager gridManager)
    {
        switch (moduleString)
        {
            case "Park":
                return new Park();

            case "House":
                return new House();

            case "TallBuilding":
                return new TallBuilding();

            case "Hospital":
                return new Hospital();
            case "PoliceStation":
                return new PoliceStation();

            case "FireStation":
                return new FireStation();

            case "Antenna":
                return new Antenna();

            default:
                gridManager.SetMessage("The module \"" + moduleString + "\" is not defined in the program.");
                return null;
        }
    }

}
