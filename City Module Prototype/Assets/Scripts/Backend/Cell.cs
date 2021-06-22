using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private int signalStr;
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



    public int GetSignalStr()
    {
        return signalStr; 
    }

    public bool HasAntenna()
    {
        return hasAntenna;
    }

    public void SetSignalStr(int signalStr)
    {
        this.signalStr = signalStr;
        
        
    }

    public bool SetSignalIfHigher(int signalStr)
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
        maxHeight = System.Math.Max(maxHeight, content.height()); 
    }

    public int GetMaxHeight()
    {
        return maxHeight;
    }

}
