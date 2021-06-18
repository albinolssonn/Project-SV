using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private int signalStr;
    private ArrayList cellContent = new ArrayList();
    private readonly int xCoord; 
    private readonly int yCoord; 

    public Cell(int xCoord, int yCoord) 
    {
        this.xCoord = xCoord;
        this.yCoord = yCoord;
        signalStr = 0;
    }

    
    
    public int GetSignalStr()
    {
        return signalStr; 
    }

    public void SetSignalStr(int signalStr)
    {
        this.signalStr = signalStr; 
    }

    public ArrayList GetCellContent()
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

    public void AddCellContent (int content)
    {
        cellContent.Add(content); 
    }

}
