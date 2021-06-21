using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private int signalStr;
    private List<Module> cellContent = new List<Module>();
    private readonly int xCoord; 
    private readonly int yCoord;
    private readonly GridManager grid;

    public Cell(int xCoord, int yCoord) 
    {
        this.xCoord = xCoord;
        this.yCoord = yCoord;
        signalStr = 0;
        grid = GameObject.FindGameObjectsWithTag("Grid")[0].GetComponent<GridManager>();

    }



    public int GetSignalStr()
    {
        return signalStr; 
    }

    public void SetSignalStr(int signalStr)
    {
        this.signalStr = signalStr;
        if(8 <= signalStr && signalStr <= 10)
        {
            grid.SetTileColor(this, Colors.green);
        } else if (5 <= signalStr && signalStr <= 7)
        {
            grid.SetTileColor(this, Colors.lightOrange);
        } else if (3 <= signalStr && signalStr <= 4)
        {
            grid.SetTileColor(this, Colors.red);
        } else if (signalStr <= 2)
        {
            grid.SetTileColor(this, Colors.gray);
        }
    }

    public void SetSignalIfHigher(int signalStr)
    {
        if(this.signalStr < signalStr)
        {
            SetSignalStr(signalStr);
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

    public void AddCellContent (Module content)
    {
        cellContent.Add(content); 
    }

}
