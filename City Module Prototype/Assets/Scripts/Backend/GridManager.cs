using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int rows = 5;
    private int cols = 8;
    private float tileSize = 1.2F;
    public SignalBarScript coverageBar;

    private Network network; 

    private Cell[,] gridArray;
    private Dictionary<Cell, GameObject> cellToTile;

    



    // Start is called before the first frame update
    public void Start()
    {
        cellToTile = new Dictionary<Cell, GameObject>();
        gridArray = GridUtils.BuildArray(rows, cols);
        GenerateGrid();
        coverageBar.SetCoverage(0);
        network = new Network(); 

    }


    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Camera cam = Camera.main;
            Vector3 worldPoint = Input.mousePosition;
            worldPoint.z = Mathf.Abs(cam.transform.position.z);
            Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(worldPoint);
            mouseWorldPosition.z = 0f;

            GetXY(mouseWorldPosition, out int x, out int y);
            if(x >= 0 && y <= 0 && x < cols && y > -rows)
            {
                Instantiate(Resources.Load("Modules/Antenna"), GetWorldPosition(x, y), Quaternion.identity);
                gridArray[-y, x].AddCellContent(new Antenna());
                UpdateNetwork();
            }
           
        }

    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * tileSize;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {

        x = Mathf.FloorToInt(worldPosition.x / tileSize);
        y = Mathf.FloorToInt(worldPosition.y / tileSize);

    }


    //Call this method each time a module is placed on the grid.
    public void UpdateNetwork()
    {
        foreach (Cell cell in gridArray)
        {
            cell.SetSignalStr(0);
        }

        foreach (Cell cell in gridArray)
        {
            if (cell.HasAntenna())
            {
                network.BuildNetwork(gridArray, cell.GetY(), cell.GetX());
            }
        }

        float total = 0;
        float count = 0;
        foreach (Cell cell in gridArray)
        {
            total += cell.GetSignalStr();

            SetTileColor(cell, cell.GetSignalStr());

            count++;
        }

        coverageBar.SetCoverage(total/count); 
    }

    private void GenerateGrid()
    {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("Modules/Square"));

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

        //transform.position = new Vector2(-gridWidth / 2 - tileSize, gridHeight / 2 - tileSize);
    }

    
    public void SetTileColor(Cell cell, int signalStr)
    {
        var tileRendererArray = cellToTile[cell].GetComponentsInChildren<Renderer>();
        var tileRenderer = tileRendererArray[0];
        
        float[] rgbt;
        if (8 <= signalStr)
        {
            rgbt = Colors.green;
        }
        else if (5 <= signalStr)
        {
            rgbt = Colors.lightOrange;
        }
        else if (3 <= signalStr)
        {
            rgbt = Colors.red;
        }
        else
        {
            rgbt = Colors.gray;
        }

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
