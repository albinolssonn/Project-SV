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
    public void Start()
    {
        cellToTile = new Dictionary<Cell, GameObject>();
        gridArray = GridUtils.BuildArray(rows, cols);
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

    
    public void SetTileColor(Cell cell, float[] rgbt)
    {
        var tileRenderer = cellToTile[cell].GetComponent<Renderer>();

        tileRenderer.material.SetColor("_Color", new Color(rgbt[0], rgbt[1], rgbt[2], rgbt[3])); 
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
