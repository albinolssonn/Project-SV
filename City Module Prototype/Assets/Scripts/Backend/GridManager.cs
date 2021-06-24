using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int rows = 10;
    private int cols = 10;
    private float tileSize = 110F;
    public SignalBarScript coverageBar;
    private Module toBePlaced;
    private Module toBeRemoved;
    private Vector3 newScale;
    private Network network; 
    private Cell[,] gridArray;
    private Dictionary<Cell, GameObject> cellToTile;

    private bool shiftHeldDown;

    
    public void SetToBePlaced(Module toBePlaced)
    {
        this.toBePlaced = toBePlaced; 
    }

    public void SetToBeRemoved(Module toBeRemoved)
    {
        this.toBeRemoved = toBeRemoved;
    }


    // Start is called before the first frame update
    public void Start()
    {
        cellToTile = new Dictionary<Cell, GameObject>();
        gridArray = GridUtils.BuildArray(rows, cols);
        GenerateGrid();
        coverageBar.SetCoverage(0);
        network = new Network();
        newScale = new Vector3(0.6f, 0.6f, 1f);
        shiftHeldDown = false;

        transform.parent.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        transform.localScale = newScale;
        UpdateNetwork();
    }



    // Update is called once per frame
    private void Update()
    {
        
        if (!Input.GetKey(KeyCode.LeftShift) && shiftHeldDown)
        {
            shiftHeldDown = false;
            toBePlaced = null;
            toBeRemoved = null;
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (toBePlaced != null)
            {
                AddModule();
            }
            else if(toBeRemoved != null)
            {
                RemoveModule();
            }
        }
    }

    public void AddModule()
    {
        ClickedCell(out int y, out int x);
        if (x >= 0 && y >= 0 && x < cols && y < rows)
        {
            GameObject tmpObject = (GameObject)Instantiate(Resources.Load(toBePlaced.GetResourcePath()), cellToTile[gridArray[y, x]].transform.position, Quaternion.identity);
            tmpObject.transform.SetParent(cellToTile[gridArray[y, x]].transform);
            tmpObject.transform.localScale = newScale;
            toBePlaced.visualObject = tmpObject;

            gridArray[y, x].AddCellContent(toBePlaced);
            UpdateNetwork();
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            toBePlaced = null;
        }
        else
        {
            shiftHeldDown = true;
            toBePlaced = toBePlaced.Copy();
        }
    }


    public void RemoveModule()
    {
        if (Input.GetMouseButtonDown(0) && toBeRemoved != null)
        {
            ClickedCell(out int y, out int x);

            if (x >= 0 && y >= 0 && x < cols && y < rows)
            {
                gridArray[y, x].RemoveCellContent(toBeRemoved);
                UpdateNetwork();
            }
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            toBeRemoved = null;
        }
        else
        {
            shiftHeldDown = true;
        }
    }

    private void ClickedCell(out int y, out int x)
    {
        Camera cam = Camera.main;
        Vector3 worldPoint = Input.mousePosition;
        worldPoint.z = Mathf.Abs(cam.transform.position.z);
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(worldPoint);
        mouseWorldPosition.z = 0f;

        GetXY(mouseWorldPosition, out x, out y);
    }


    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        var tmp = transform.InverseTransformPoint(worldPosition);
        x = Mathf.FloorToInt(tmp[0] / tileSize);
        y = -Mathf.FloorToInt(tmp[1] / tileSize);

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
                tile.transform.SetParent(this.transform);
                cellToTile[gridArray[row, col]] = tile;

                float posX = col * tileSize;
                float posY = row * -tileSize;

                tile.transform.localPosition = new Vector2(posX, posY);
            }
        }


        float gridWidth = cols * tileSize;
        float gridHeight = rows * tileSize;


        transform.localPosition = new Vector2(-gridWidth / 4, gridHeight / 4);

        Destroy(referenceTile);

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

    

    public void ResetGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                gridArray[row, col].ClearCellContent();
                gridArray[row, col].SetSignalStr(0);

            }
        }
        UpdateNetwork();
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
