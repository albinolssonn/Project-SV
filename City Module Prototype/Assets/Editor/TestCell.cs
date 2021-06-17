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

        Assert.AreEqual(cell.GetSignalStr(), 0);
    }

    [Test]
    public void SetSignalStr_Test()
    {
        Cell cell = new Cell(3, 6);
        Assert.AreEqual(cell.GetSignalStr(), 0);

        cell.SetSignalStr(5);
        Assert.AreEqual(cell.GetSignalStr(), 5);
    }

    [Test]
    public void GetCoordinates_Test()
    {
        Cell cell = new Cell(1, 1);

        int x = cell.GetX();
        int y = cell.GetY();

        Assert.AreEqual(x, 1);
        Assert.AreEqual(y, 1);
    }

    [Test]
    public void GetCellContent_Test()
    {
        Cell cell = new Cell(3, 2);
        Assert.AreEqual(cell.GetCellContent().Count, 0);

        cell.AddCellContent(3);
        cell.AddCellContent(5);

        Assert.AreEqual(cell.GetCellContent()[0], 3);
        Assert.AreEqual(cell.GetCellContent()[1], 5);
    }

   [Test]
   public void AddCellContent_Test()
    {
        Cell cell = new Cell(3, 2);
        Assert.AreEqual(cell.GetCellContent().Count, 0);

        cell.AddCellContent(3);
        Assert.AreEqual(cell.GetCellContent().Count, 1);
        
        cell.AddCellContent(5);
        Assert.AreEqual(cell.GetCellContent().Count, 2);
    }

    
}
