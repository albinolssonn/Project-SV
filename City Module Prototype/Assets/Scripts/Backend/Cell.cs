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
    private double maxHeight;
    private Antenna antenna;
    private GameObject tile;
    private GameObject NVTile;
    private bool hasCriticalModule;
    private double capacityDemand;

    private Direction signalDirection;
    private bool signalDirDiagonal;

    /// <summary>
    /// The file path to the resource used to visualize a cell.
    /// </summary>
    public static readonly string resourcePath = "Modules/Square";

    /// <summary>
    /// Represents a cell in the grid.
    /// </summary>
    /// <param name="yCoord">The y-coordinate of this cell in the grid.</param>
    /// <param name="xCoord">The x-coordinate of this cell in the grid.</param>
    public Cell(int yCoord, int xCoord)
    {
        signalStr = 0;
        cellContent = new List<Module>();
        this.xCoord = xCoord;
        this.yCoord = yCoord;
        maxHeight = 0;
        antenna = null;
        tile = null;
        NVTile = null;
        hasCriticalModule = false;
        capacityDemand = 0;

        signalDirection = null;
        signalDirDiagonal = false;

    }


    /// <summary>
    /// Connects a game object to the cell used to visualize it.
    /// </summary>
    /// <param name="tile">The game object visualizing the Cell.</param>
    public void SetTile(GameObject tile)
    {
        this.tile = tile;
    }


    /// <summary>
    /// Connects a game object to the cell used to visualize it in the Network View.
    /// </summary>
    /// <param name="tile">The game object visualizing the Cell.</param>
    public void SetNVTile(GameObject tile)
    {
        NVTile = tile;
    }


    /// <returns>The game object visualizing this cell.</returns>
    public GameObject GetTile()
    {
        return tile;
    }


    /// <returns>The Network View tile used to visualize the tile in the network view.</returns>
    public GameObject GetNVTile()
    {
        return NVTile;
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


    /// <returns>The signal strength being supplied to this cell.</returns>
    public double GetSignalStr()
    {
        return signalStr;
    }


    /// <returns>The Antenna contained in this cell. Null if none.</returns>
    public Antenna GetAntenna()
    {
        return antenna;
    }


    /// <returns>If this cell has an Antenna or not.</returns>
    public bool HasAntenna()
    {
        return antenna != null;
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
    /// Sets the signal strength of this cell to a new value if the new value is higher than the current.
    /// </summary>
    /// <param name="signalStr">The new signal strength.</param>
    /// <param name="cameFrom">Which direction this signal came from.</param>
    /// <param name="wasDiagonal">If the direction the signal came from was diagonal.</param>
    /// <exception cref="System.ArgumentException">
    /// Throws if cameFrom was passed as null.
    /// </exception>
    public void SetSignalIfStronger(double signalStr, Direction cameFrom, bool wasDiagonal)
    {
        if (cameFrom == null)
        {
            throw new System.ArgumentException("cameFrom was null.");
        }

        if (this.signalStr < signalStr ||
            (signalDirection != null &&
            this.signalStr == signalStr &&
            signalDirection.originCell.GetAntenna().AvailableCapacity() + capacityDemand < cameFrom.originCell.GetAntenna().AvailableCapacity() - capacityDemand))
        {
            if (signalDirection != null)
            {
                signalDirection.originCell.GetAntenna().RemoveDemand(capacityDemand);
            }

            this.signalStr = signalStr;
            signalDirection = cameFrom;
            signalDirDiagonal = wasDiagonal;

            signalDirection.originCell.GetAntenna().AddDemand(capacityDemand);
        }
    }


    /// <returns>The content of Modules of this cell.</returns>
    public List<Module> GetCellContent()
    {
        return cellContent;
    }


    /// <returns>The capacity demand of this cell.</returns>
    public double GetCapacityDemand()
    {
        return capacityDemand;
    }



    /// <returns>The available capacity which this cell has access to.</returns>
    public double GetAvailableCapacity()
    {
        if (signalDirection != null)
        {
            return signalDirection.originCell.GetAntenna().AvailableCapacity();
        }

        return 0;
    }

    /// <returns>The x-coordinate of this cell.</returns>
    public int GetX()
    {
        return xCoord;
    }

    /// <returns>The y-coordinate of this cell.</returns>
    public int GetY()
    {
        return yCoord;
    }


    /// <summary>
    /// Adds a module to 'cellContent' and sets the max height of this 
    /// cell to the height of the module if its height is higher.
    /// </summary>
    /// <param name="content">The module object to add to this cell.</param>
    public void AddCellContent(Module content)
    {
        if (content is Antenna newAntenna)
        {
            antenna = newAntenna;
        }

        hasCriticalModule = content.IsCritical();


        capacityDemand += content.CapacityDemand();

        cellContent.Add(content);
        maxHeight = System.Math.Max(maxHeight, content.Height());
    }


    /// <returns>The height of this cell.</returns>
    public double GetHeight()
    {
        return maxHeight;
    }


    /// <summary>
    /// Removes the Module instance matching the given type from this cell.
    /// </summary>
    /// <param name="module">The module type to remove from this cell.</param>
    /// <returns>True if the Module was removed.</returns>
    public bool RemoveCellContent(Module module)
    {
        bool removedSomething = false;
        List<Module> newList = new List<Module>();
        foreach (Module elem in cellContent)
        {
            if (elem.GetType() == module.GetType())
            {
                GameObject.Destroy(elem.visualObject);
                capacityDemand -= module.CapacityDemand();
                removedSomething = true;
            }
            else
            {
                newList.Add(elem);
            }
        }
        cellContent = newList;

        if (removedSomething)
        {
            if (module is Antenna)
            {
                RemoveAntenna();
            }
            else
            {
                hasCriticalModule = false;
            }
        }


        double newMax = 0;
        foreach (Module elem in cellContent)
        {
            newMax = System.Math.Max(newMax, elem.Height());
        }
        maxHeight = newMax;

        if (capacityDemand < 0)
        {
            throw new System.Exception("Capacity of Cell is negative.");
        }

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

        capacityDemand = 0;

        RemoveAntenna();
        hasCriticalModule = false;
        maxHeight = 0;
        cellContent = new List<Module>();
    }


    /// <summary>Removes the Antenna from the cell in the correct way.</summary>
    /// <remarks>Does nothing if there is no Antenna in this cell.</remarks>
    private void RemoveAntenna()
    {
        if (antenna == null)
        {
            return;
        }

        if (signalDirection is Origin origin)
        {
            origin.OriginRemoved();
        }
        else
        {
            throw new System.Exception("Direction for cell with Antenna was not Origin.");
        }
        antenna = null;
    }


    /// <returns>If this cell has a critical module in it.</returns>
    public bool HasCriticalModule()
    {
        return hasCriticalModule;
    }
}