using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class manages the grid which one place modules and visualizes the network on.
 */
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

    // Update is called once per frame. It checks if the mouse has been clicked to place down a module.
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
            else if (toBeRemoved != null)
            {
                RemoveModule();
            }
        }
    }


    /*
     * When clicking a button to place a module, the module type to be placed should be set in the variable 'toBePlaced'.
     * If one wants to place an Antenna, then set 'toBePlaced' to 'new Antenna()'.
     * 
     * Module toBePlaced: An instance of the object one wants to place on the next mousclick.
     * 
     * Returns: Nothing.
     */
    public void SetToBePlaced(Module toBePlaced)
    {
        this.toBePlaced = toBePlaced; 
    }


    /*
     * When clicking a button to remove a module, the module type to be removed should be set in the variable 'toBeRemoved'.
     * If one wants to remove an Antenna, then set 'toBeRemoved' to 'new Antenna()'.
     * 
     * Module toBeRemoved: An instance of the object one wants to remove on the next mousclick.
     * 
     * Returns: Nothing.
     */
    public void SetToBeRemoved(Module toBeRemoved)
    {
        this.toBeRemoved = toBeRemoved;
    }

    /*
     * Helper-method which places a module on the clicked cell of the type that was set in the variable 'toBePlaced'.
     * 
     * Returns: Nothing.
     */
    private void AddModule()
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

    /*
     * Helper-method which removes a module on the clicked cell of the type that was set in the variable 'toBeRemoved'.
     * 
     * Returns: Nothing.
     */
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

    /*
     * Helper-method which locates which cell was clicked and sets the y and x to the correct grid-coordinates.
     * 
     * out int y: This variable will be set to the y value of the grid corresponding to the position clicked with the mouse.
     * 
     * out int x: This variable will be set to the x value of the grid corresponding to the position clicked with the mouse.
     */
    private void ClickedCell(out int y, out int x)
    {
        Camera cam = Camera.main;
        Vector3 worldPoint = Input.mousePosition;
        worldPoint.z = Mathf.Abs(cam.transform.position.z);
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(worldPoint);
        mouseWorldPosition.z = 0f;

        GetXY(mouseWorldPosition, out y, out x);
    }

    /*
     * Helper-method which translates the world position clicked with the mouse to y- and x- coordinates.
     * 
     * Vector3 worldPosition: The position in world coordinates.
     * 
     * out int y: This variable will be set to the y value of the grid corresponding to the position worldPosition.
     * 
     * out int x: This variable will be set to the x value of the grid corresponding to the position worldPosition.
     */
    private void GetXY(Vector3 worldPosition, out int y, out int x)
    {
        var tmp = transform.InverseTransformPoint(worldPosition);
        x = Mathf.FloorToInt(tmp[0] / tileSize);
        y = -Mathf.FloorToInt(tmp[1] / tileSize);

    }


    /*
     * This method updates the network by rebuilding it using the Network class and then setting each tile to their correct color.
     * 
     * Returns: Nothing.
     */
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
            total += (float)cell.GetSignalStr();

            SetTileColor(cell);

            count++;
        }

        coverageBar.SetCoverage(total/count); 
    }

    /*
     * This method builds and displays the visual grid of tiles on the screen and saves them in the dictionary cellToTile, 
     * mapping each cell to their corresponding visual tile to be reachable later.
     * 
     * Returns: Nothing.
     */
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

    /*
     * This method sets the color of a given cell's tile based on cell's signal strength.
     * 
     * Cell cell: The cell corresponding to the tile one wants to change color of.
     * 
     * Returns: Nothing.
     */
    public void SetTileColor(Cell cell)
    {
        var tileRenderer = cellToTile[cell].transform.GetChild(0).GetComponent<Renderer>();
        var signalStr = cell.GetSignalStr();

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

    /*
     * This method resets the entire grid by emptying every cell of content, 
     * setting every cell's signal strength to zero and then updating the network.
     */
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

    /*
     * Returns the number of rows of the grid.
     * 
     * Returns: Number of rows of the grid as an int.
     */
    public int GetRows()
    {
        return rows;
    }

    /*
     * Returns the number of columns of the grid.
     * 
     * Returns: Number of cols of the grid as an int.
     */
    public int GetCols()
    {
        return cols;
    }

}
