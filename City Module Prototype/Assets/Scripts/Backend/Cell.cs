using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    public double GetSignalStr()
    {
        return signalStr; 
    }

    public bool HasAntenna()
    {
        return hasAntenna;
    }

    public void SetSignalStr(double signalStr)
    {
        this.signalStr = signalStr;
        
        
    }

    public bool SetSignalIfHigher(double signalStr)
    {
        if(this.signalStr < signalStr)
        {
            SetSignalStr(signalStr);
            return true;
        }
        return false;
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
