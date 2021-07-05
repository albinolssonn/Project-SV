using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Represents a cell in the grid.
/// </summary>
public class Cell
{
    private double signalStr;
    private List<Module> cellContent;
    private readonly int xCoord; 
    private readonly int yCoord;
    private int maxHeight;
    private bool hasAntenna;
    private GameObject tile;

    private Direction signalDirection;
    private bool signalDirDiagonal;

    /// <summary>
    /// The file path to the resource used to visualize a cell.
    /// </summary>
    public static readonly string resourcePath = "Modules/Square";

    /// <summary>
    /// Represents a cell in the grid.
    /// </summary>
    /// <param name="yCoord">The y-coordinate in the grid of this cell.</param>
    /// <param name="xCoord">The x-coordinate in the grid of this cell.</param>
    public Cell(int yCoord, int xCoord) 
    {
        this.xCoord = xCoord;
        this.yCoord = yCoord;
        signalStr = 0;
        hasAntenna = false;
        cellContent = new List<Module>();
    }


    /// <summary>
    /// Connects a game object to the cell used to visualize it.
    /// </summary>
    /// <param name="tile">The game object to connect to the Cell.</param>
    public void SetTile(GameObject tile)
    {
        this.tile = tile;
    }

    public GameObject GetTile()
    {
        return tile;
    }


    /// <returns>Which direction the signal for this cell came from.</returns>
    public Direction GetSignalDir()
    {
        return signalDirection;
    }


    /// <returns>If the signal came to this sell via a diagonal.</returns>
    public bool GetSignalDirDiagonal()
    {
        return signalDirDiagonal;
    }


    public double GetSignalStr()
    {
        return signalStr; 
    }


    /// <returns>If this cell has an Antenna in it.</returns>
    public bool HasAntenna()
    {
        return hasAntenna;
    }


    /// <summary>
    /// Resets the signal strength of this cell to 0 and removes its Direction object.
    /// </summary>
    public void ResetSignalStr()
    {
        signalStr = 0;
        signalDirDiagonal = false;
        signalDirection = null;
        
    }


    /// <summary>
    /// Sets the signal strength of this cell to a new value if the new value is larger.
    /// </summary>
    /// <param name="signalStr">The new signal strength.</param>
    /// <param name="cameFrom">Which direction this signal came from.</param>
    /// <param name="wasDiagonal">If the direction the signal came from was diagonal.</param>
    /// <exception cref="System.ArgumentException">
    /// Throws if cameFrom was passed as null.
    /// </exception>
    public void SetSignalIfHigher(double signalStr, Direction cameFrom, bool wasDiagonal)
    {
        if(cameFrom == null)
        {
            throw new System.ArgumentException("cameFrom was null.");
        }

        if(this.signalStr < signalStr)
        {
            this.signalStr = signalStr;
            signalDirection = cameFrom;
            signalDirDiagonal = wasDiagonal;
        }
    }


    public List<Module> GetCellContent()
    {
        return cellContent; 
    }


    public int GetX()
    {
        return xCoord;
    }


    public int GetY()
    {
        return yCoord;
    }

    /*
     * 
     * 
     * Module content: The 'Module' object which one wants to add to this cell.
     * 
     * Returns: Nothing.
     */
    /// <summary>
    /// Adds a module to cellContent and sets the max height of this 
    /// cell to the height of the module if its height is higher.
    /// </summary>
    /// <param name="content">The module object to add to this cell.</param>
    public void AddCellContent (Module content)
    {
        if(content is Antenna)
        {
            hasAntenna = true; 
        }

        cellContent.Add(content);
        maxHeight = System.Math.Max(maxHeight, content.Height()); 
    }


    public int GetMaxHeight()
    {
        return maxHeight;
    }


    /// <summary>
    /// Removes every Module instance matching the given type from this cell and sets the variable 'maxHeight' to its new correct value.
    /// </summary>
    /// <param name="module">The module type to remove every instance of from this cell.</param>
    public bool RemoveCellContent(Module module)
    {
        bool removedSomething = false;
        List<Module> newList = new List<Module>();
        foreach (Module elem in cellContent)
        {
            if(elem.GetType() == module.GetType())
            {
                GameObject.Destroy(elem.visualObject);
                removedSomething = true;
            } else
            {
                newList.Add(elem);
            }
        }
        cellContent = newList;

        if (module is Antenna)
        {
            hasAntenna = false;
        }

        int newMax = 0;
        foreach (Module elem in cellContent)
        {
            newMax = System.Math.Max(newMax, elem.Height());
        }
        maxHeight = newMax;

        return removedSomething;
    }


    /// <summary>
    /// Clears the cell of all modules it contains, as well as resets every variable dependent on the modules.
    /// </summary>
    public void ClearCellContent()
    {
        foreach (Module module in cellContent)
        {
            GameObject.Destroy(module.visualObject);
        }
        hasAntenna = false;
        maxHeight = 0;
        cellContent = new List<Module>();
    }
}