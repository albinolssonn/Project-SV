using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class TestBFS
{ 
    [Test]
    public void BFSeach_Test()
    {
        BFS searcher = new BFS();
        int rows = 5;
        int cols = 5;
        Cell[,] gridArray = GridUtils.BuildArray(rows, cols);

        //searcher.BFSearch(gridArray, 0, 0);


        //TODO: Finish this test when the logic for the network traversal is done.
        Assert.IsTrue(false);
    }
}
