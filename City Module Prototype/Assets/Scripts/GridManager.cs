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



    // Start is called before the first frame update
    void Start()
    {
        cellToTile = new Dictionary<Cell, GameObject>(); 
        gridArray = new Cell[rows, cols];
        BuildArray();
        GenerateGrid();


        setTileColor(gridArray[1, 1], "red");
        setTileColor(gridArray[1, 2], "green");
        setTileColor(gridArray[2, 2], "blue");



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
        transform.position = new Vector2(-gridWidth / 2 + tileSize / 2, gridHeight / 2 - tileSize / 2);
    }

    private void BuildArray()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                gridArray[row, col] = new Cell(); 
            }
        }
    }


    private void setTileColor(Cell cell, string color)
    {
        var tileRenderer = cellToTile[cell].GetComponent<Renderer>();
        switch (color)
        {
            case "red":
                tileRenderer.material.SetColor("_Color", Color.red);
                break;
            case "green":
                tileRenderer.material.SetColor("_Color", Color.green);
                break;
            case "blue":
                tileRenderer.material.SetColor("_Color", Color.blue);
                break;

        } 
    }

    
}
