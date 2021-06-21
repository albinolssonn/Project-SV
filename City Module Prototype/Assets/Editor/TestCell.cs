using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class TestCell
{

   [Test]
   public void GetSignalStr_Test()
    {
        Cell cell = new Cell(5, 1);

        Assert.AreEqual(0, cell.GetSignalStr());
    }

    

    [Test]
    public void GetCellContent_Test()
    {
        Cell cell = new Cell(3, 2);
        Assert.AreEqual(0, cell.GetCellContent().Count);

        Module a = new House();
        Module b = new Hospital(); 

        cell.AddCellContent(a);
        cell.AddCellContent(b);

        Assert.AreEqual(a, cell.GetCellContent()[0]);
        Assert.AreEqual(b, cell.GetCellContent()[1]);
    }

    [Test]
    public void GetCoordinates_Test()
    {
        Cell cell = new Cell(1, 1);

        int x = cell.GetX();
        int y = cell.GetY();

        Assert.AreEqual(1, x);
        Assert.AreEqual(1, y);
    }

   [Test]
   public void AddCellContent_Test()
    {
        Cell cell = new Cell(3, 2);
        Assert.AreEqual(0, cell.GetCellContent().Count);

        cell.AddCellContent(new House());
        Assert.AreEqual(1, cell.GetCellContent().Count);
        
        cell.AddCellContent(new Hospital());
        Assert.AreEqual(2, cell.GetCellContent().Count);
    }

    
}
