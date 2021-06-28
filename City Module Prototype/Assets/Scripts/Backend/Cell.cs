using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class represents a cell in the grid.
 */
public class Cell
{
    private double signalStr;
    private List<Module> cellContent;
    private readonly int xCoord; 
    private readonly int yCoord;
    private readonly GridManager grid;
    private int maxHeight;
    private bool hasAntenna; 

    public Cell(int yCoord, int xCoord) 
    {
        this.xCoord = xCoord;
        this.yCoord = yCoord;
        signalStr = 0;
        hasAntenna = false;
        grid = GameObject.FindGameObjectsWithTag("Grid")[0].GetComponent<GridManager>();
        cellContent = new List<Module>(); 
    }


    /*
     * Returns: the signal strength of this cell as a 'double'.
     */
    public double GetSignalStr()
    {
        return signalStr; 
    }

    /*
     * Returns: A if this cell has an Antenna in it or not as a 'bool'.
     */
    public bool HasAntenna()
    {
        return hasAntenna;
    }

    /*
     * This method sets the signal strength of this cell.
     * 
     * double signalStr: the value which the variable 'signalStr' should be set to.
     */
    public void SetSignalStr(double signalStr)
    {
        this.signalStr = signalStr;
        
        
    }

    /*
     * This method sets the signal strength of this cell to a new value ONLY if the new value is larger.
     * 
     * Returns: Nothing.
     */
    public void SetSignalIfHigher(double signalStr)
    {
        if(this.signalStr < signalStr)
        {
            SetSignalStr(signalStr);
        }
    }

    /*
     * Returns: The variable cellContent.
     */
    public List<Module> GetCellContent()
    {
        return cellContent; 
    }

    /*
     * Returns: The variable xCoord.
     */
    public int GetX()
    {
        return xCoord;
    }

    /*
     * Returns: The variable yCoord.
     */
    public int GetY()
    {
        return yCoord;
    }

    /*
     * This method adds a module to the List<Module> cellContent and sets the max height of this module
     * cell to the height of the module if the new height is higher than any previous.
     * 
     * Module content: The 'Module' object which one wants to add to this cell.
     * 
     * Returns: Nothing.
     */
    public void AddCellContent (Module content)
    {
        if(content is Antenna)
        {
            hasAntenna = true; 
        }

        cellContent.Add(content);
        maxHeight = System.Math.Max(maxHeight, content.Height()); 
    }

    /*
     * Returns: The max height of any modules placed in this cell.
     */
    public int GetMaxHeight()
    {
        return maxHeight;
    }

    /*
     * This method removes every instance of the given 'Module' type from this cell and sets the variable 'maxHeight' to its new correct value.
     * Example, RemoveCellContent(new House()) to remove every House from this cell.
     * 
     * Module module: The module type one wants to remove from this cell.
     * 
     * Returns: Nothing.
     */
    public void RemoveCellContent(Module module)
    {
        List<Module> newList = new List<Module>();
        foreach (Module elem in cellContent)
        {
            if(elem.GetType() == module.GetType())
            {
                GameObject.Destroy(elem.visualObject);
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
    }

    /*
     * This method clears the cell of all modules it contains, as well as resets every variable dependent on the modules in it.
     * 
     * Returns: Nothing.
     */
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
