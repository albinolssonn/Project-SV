using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int rows = 5;
    private int cols = 8;
    private float tileSize = 1.2F;

    private Cell[,] gridArray;
    private Dictionary<Cell, GameObject> cellToTile;

    private float[] gray = new float[] { 0.5f, 0.5f, 0.5f, 0.75f };
    private float[] lightOrange = new float[] { 1f, 1f, 0.425f, 0.75f };
    //private float[] red = new float[] { 1f, 0.276f, 0.231f, 0.75f };
    //private float[] green = new float[] { 0.23f, 1f, 0.325f, 0.75f };



    // Start is called before the first frame update
    public void Start()
    {
        cellToTile = new Dictionary<Cell, GameObject>();
        gridArray = new Cell[rows, cols];
        BuildArray();
        GenerateGrid();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateGrid()
    {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("Square"));

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);


                cellToTile[gridArray[row, col]] = tile;

                float posX = col * tileSize;
                float posY = row * -tileSize;

                tile.transform.position = new Vector2(posX, posY);
            }
        }

        Destroy(referenceTile);

        float gridWidth = cols * tileSize;
        float gridHeight = rows * tileSize;

        transform.position = new Vector2(-gridWidth / 2 - tileSize, gridHeight / 2 - tileSize);
    }

    private void BuildArray()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                gridArray[row, col] = new Cell(col, row);
            }
        }
    }

    private void SetTileColor(Cell cell, float[] rgbt)
    {
        var tileRenderer = cellToTile[cell].GetComponent<Renderer>();

        tileRenderer.material.SetColor("_Color", new Color(rgbt[0], rgbt[1], rgbt[2], rgbt[3])); 
    }

    public ArrayList GetNearbyCells(int x, int y)
    {
        Cell cell = gridArray[y, x];

        ArrayList neighbours = new ArrayList(); 

        cell.GetCoordinates(out x, out y); 

        if(x > 0)
        {
            neighbours.Add(gridArray[y, x - 1]); 
        } 

        if(x < cols - 1)
        {
            neighbours.Add(gridArray[y, x + 1]);
        }

        if(y > 0)
        {
            neighbours.Add(gridArray[y - 1, x]);
        }

        if(y < rows - 1)
        {
            neighbours.Add(gridArray[y + 1, x]);
        }

        if(x > 0 && y > 0)
        {
            neighbours.Add(gridArray[y - 1, x - 1]);
        }

        if (x < cols - 1 && y > 0)
        {
            neighbours.Add(gridArray[y - 1, x + 1]);
        }

        if (x < cols - 1 && y < rows - 1)
        {
            neighbours.Add(gridArray[y + 1, x + 1]);
        }

        if (x > 0 && y < rows - 1)
        {
            neighbours.Add(gridArray[y + 1, x - 1]);
        }

        return neighbours;
    }
    
    public int GetRows()
    {
        return rows;
    }

    public int GetCols()
    {
        return cols;
    }

}
