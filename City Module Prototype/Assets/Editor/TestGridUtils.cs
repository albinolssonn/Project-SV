using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class TestGridUtils
{
    [Test]
    public void GetNearbyCells_Test()
    {
        int rows = 5;
        int cols = 5;
        //cellToTile = new Dictionary<Cell, GameObject>();
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);
        
        

        ArrayList tmp = GridUtils.GetNearbyCells(1, 2, gridArray);
        for (int i = 0; i < tmp.Count; i++)
        {
            //SetTileColor((Cell)tmp[i], lightOrange);
        }
    }


}
