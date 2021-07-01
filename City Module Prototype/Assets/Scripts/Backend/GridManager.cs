using System.Collections.Generic;
using UnityEngine;

/*
 * This class manages the grid which one place modules and visualizes the network on.
 */
public class GridManager : MonoBehaviour
{
    private int rows;
    private int cols;
    private float tileSize;
    private Module toBePlaced;
    private Module toBeRemoved;
    private Vector3 newScale;
    private Network network; 
    private Cell[,] gridArray;
    private List<GameObject> networkFlow;
    private double maxSignalStr;
    private Colors colors; 

    private bool shiftHeldDown;
    private bool createNetworkArrows;

    public SignalBarScript coverageBar;



    // Start is called before the first frame update
    public void Start()
    {
        maxSignalStr = 10;
        rows = 10;
        cols = 10;
        tileSize = 110F;
        shiftHeldDown = false;
        createNetworkArrows = false;
        network = new Network();
        networkFlow = new List<GameObject>();

        gridArray = GridUtils.BuildArray(rows, cols);
        colors = new Colors(maxSignalStr);
        newScale = new Vector3(0.6f, 0.6f, 1f);
        

        GenerateGrid();
        CenterGrid();

        coverageBar.SetCoverage(0, colors);

        transform.localScale = newScale;
        UpdateNetwork();
    }


    /*
     * This helper-method centers the grid to the middle of the screen.
     */
    private void CenterGrid()
    {
        transform.parent.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));


        var scaledTileSize = tileSize * newScale[0];

        float[] finalPosition = new float[2] { -scaledTileSize , 0 };

        float gridWidth = cols * scaledTileSize;
        float gridHeight = rows * scaledTileSize;

        finalPosition[1] = finalPosition[1] - gridWidth / 2;
        finalPosition[0] = finalPosition[0] + gridHeight / 2;

        transform.localPosition = new Vector2(finalPosition[1], finalPosition[0]);
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
     * This method toggles the visual of the network flow and then creates/removes the flow visuals.
     */
    public void ToggleCreateNetworkArrows()
    {
        createNetworkArrows = !createNetworkArrows;
        if (createNetworkArrows)
        {
            foreach (Cell cell in gridArray)
            {
                CreateNetworkFlow(cell);
            }
        } else
        {
            DestroyNetworkFlow();
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
            GameObject tmpObject = (GameObject)Instantiate(Resources.Load(toBePlaced.GetResourcePath()), gridArray[y, x].GetTile().transform.position, Quaternion.identity);
            tmpObject.transform.SetParent(gridArray[y, x].GetTile().transform);
            tmpObject.transform.localScale = newScale;
            toBePlaced.visualObject = tmpObject;
            SetLayerRecursive(tmpObject, LayerMask.NameToLayer("Module"));

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
     * This method sets the layer of the given objects and all its children to newLayer.
     * 
     * GameObject currentObejct: The object and its children to get the layer set.
     * 
     * int newLayer: The layer which to set the objects to.
     */
    private void SetLayerRecursive(GameObject currentObject, int newLayer)
    {
        currentObject.layer = newLayer;

        foreach (Transform child in currentObject.transform)
        {
            child.gameObject.layer = newLayer;

            Transform grandChildren = child.GetComponentInChildren<Transform>();

            if(grandChildren != null)
            {
                SetLayerRecursive(child.gameObject, newLayer);
            }
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
        ResetNetwork();

        DestroyNetworkFlow();

        network.BuildNetwork(gridArray, maxSignalStr);


        float total = 0;
        float count = 0;
        foreach (Cell cell in gridArray)
        {
            total += (float)cell.GetSignalStr();

            SetTileColor(cell);

            if (createNetworkArrows)
            {
                CreateNetworkFlow(cell);
            }

            count++;
        }

        coverageBar.SetCoverage(total/count, colors); 
    }

    private void ResetNetwork()
    {
        foreach (Cell cell in gridArray)
        {
            cell.ResetSignalStr();
        }
    }

    /*
     * This method builds and displays the visual grid of tiles on the screen and saves them in the dictionary cellToTile, 
     * mapping each cell to their corresponding visual tile to be reachable later.
     * 
     * Returns: Nothing.
     */
    private void GenerateGrid()
    {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load(Cell.resourcePath));

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);
                tile.transform.SetParent(transform);
                gridArray[row, col].SetTile(tile);

                float posX = col * tileSize;
                float posY = row * -tileSize;

                tile.transform.localPosition = new Vector2(posX, posY);
            }
        }

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
        var tileRenderer = cell.GetTile().transform.GetChild(0).GetComponent<Renderer>();
        var signalStr = cell.GetSignalStr();

        float[] rgbt;
        if (1 <= signalStr)
        {
            rgbt = colors.GetGradientColor(signalStr); 
        }
        else
        {
            rgbt = colors.gray;
        }

        tileRenderer.material.SetColor("_Color", new Color(rgbt[0], rgbt[1], rgbt[2], rgbt[3])); 

    }

    private void CreateNetworkFlow(Cell cell)
    {
        if(cell.GetSignalDir() == null)
        {
            if(cell.GetSignalStr() != 0)
            {
                throw new UnassignedReferenceException("Direction is null but signalStr > 0.");
            }
            return;
        }

        GameObject arrow = (GameObject)Instantiate(Resources.Load(cell.GetSignalDir().GetResourcePath()));
        networkFlow.Add(arrow);

        arrow.transform.SetParent(gridArray[cell.GetY(), cell.GetX()].GetTile().transform);

        arrow.transform.localPosition = new Vector2(0.5f, 0.5f);

        var rotation = new Vector3(0, 0, cell.GetSignalDir().GetDirectionArrowRotation(cell.GetSignalDirDiagonal()));
        arrow.transform.Rotate(rotation);

    }

    private void DestroyNetworkFlow()
    {
        for (int i = 0; i < networkFlow.Count; i++)
        {
            Destroy(networkFlow[i]);
        }
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
                gridArray[row, col].ResetSignalStr();

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