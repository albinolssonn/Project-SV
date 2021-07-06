using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the grid to place modules and visualizes the network on.
/// Works as the main of the program.
/// </summary>
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
    private List<GameObject> networkFlowVisuals;
    private string simulationModeSelected;
    private Dictionary<string, Colors> colors;


    private int totalAntennas;
    private int maxAntennas; 

    public static bool limitedAntennas;
    public static bool criticalCoverage;


    /// <summary>The strength of which an Antenna transmits a signal with.</summary>
    public static readonly double baseSignalStr = 10;

    /// <summary>The capacity of which an Antenna can supply users with.</summary>
    public static readonly double baseCapacity = 20;

    /// <summary>The reduction in signal strength for traveling one step in any direction.</summary>
    public static readonly double distancePenalty = 1;

    /// <summary>The reduction in signal strength for traveling through a cell with a higher max height than the cell which the antenna is located in.</summary>
    public static readonly double heightPenalty = 2;

    private bool shiftHeldDown;
    private bool createNetworkArrows;

    public SignalBarScript coverageBar;
    public AntennaStatistics antennaStatistics;
    public CriticalCoverageScript criticalCoverageScript;



    // Start is called before the first frame update
    public void Start()
    {
        rows = 10;
        cols = 10;
        tileSize = 110F;
        totalAntennas = 0;
        simulationModeSelected = "coverage";
        shiftHeldDown = false;
        createNetworkArrows = false;
        limitedAntennas = false;
        criticalCoverage = false;
        networkFlowVisuals = new List<GameObject>();
        colors = new Dictionary<string, Colors>();

        colors.Add("coverage", new RedGreen(baseSignalStr));
        colors.Add("capacity", new WhiteBlue(baseCapacity));

        network = new Network(baseSignalStr, distancePenalty, heightPenalty);
        gridArray = GridUtils.BuildArray(rows, cols);
        newScale = new Vector3(0.6f, 0.6f, 1f);
        

        GenerateGrid();
        CenterGrid();

        coverageBar.SetCoverage(0, colors["coverage"]);
        criticalCoverageScript.SetCoverage(0, colors["capacity"]); 
        antennaStatistics.setAntennaStatistics(totalAntennas, 0); 


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


    /// <summary>
    /// Centers the grid to the middle of the screen.
    /// </summary>
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




    /// <summary>
    /// Toggles the visual of the network flow and then creates/removes the network flow visuals.
    /// </summary>
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


    public bool ToggleLimitedAntennas()
    {
        limitedAntennas = !limitedAntennas;

        if (!limitedAntennas)
        {
            antennaStatistics.setAntennaStatistics(totalAntennas, maxAntennas);
        }

        return limitedAntennas; 
    }

    public bool ToggleCriticalCoverage()
    {
        criticalCoverage = !criticalCoverage;

        if (criticalCoverage)
        {
            float criticalCount = 0;
            float criticalTotal = 0;

            foreach (Cell cell in gridArray)
            {
                if (cell.HasCriticalModule())
                {
                    criticalCount++;
                    criticalTotal += (float)cell.GetSignalStr();
                }
            }

            if (criticalCount == 0)
            {
                criticalTotal = 0;
                criticalCount = 1;
            }

            criticalCoverageScript.SetCoverage(criticalTotal / criticalCount, colors["coverage"]);
   
        }
     

        return criticalCoverage;
    }



    /// <summary>
    /// When clicking a button to place a module, the module type to be placed should be set in the variable 'toBePlaced'.
    /// If one wants to place an Antenna, then set 'toBePlaced' to 'new Antenna()'.
    /// </summary>
    /// <param name="toBePlaced">An instance of the Module one wants to place on the next mousclick.</param>
    public void SetToBePlaced(Module toBePlaced)
    {
        this.toBePlaced = toBePlaced;
    }



    /// <summary>
    /// When clicking a button to remove a module, the module type to be removed should be set in the variable 'toBeRemoved'.
    /// If one wants to remove an Antenna, then set 'toBeRemoved' to 'new Antenna()'.
    /// </summary>
    /// <param name="toBeRemoved">An instance of the Module one wants to remove on the next mousclick.</param>
    public void SetToBeRemoved(Module toBeRemoved)
    {
        this.toBeRemoved = toBeRemoved;
    }


    /// <summary>
    /// Places a module on the clicked cell of the type that was set in the variable 'toBePlaced'.
    /// </summary>
    /// <remarks>
    /// This method should only be called upon if the variable 'toBePlaced' has been set.
    /// </remarks>
    /// <exception cref="System.ArgumentException">
    /// Throws if 'toBePlaced' has not been set.
    /// </exception>
    private void AddModule()
    {
        if (toBePlaced == null)
        {
            throw new System.ArgumentException("'toBeRemoved' has not been set prior to calling this method.");
        }

        if(!(toBePlaced is Antenna && limitedAntennas && maxAntennas <= totalAntennas))
        {
            MouseToGridCoord(out int y, out int x);
            if (x >= 0 && y >= 0 && x < cols && y < rows)
            {
                if (!(gridArray[y, x].GetAntenna() != null && toBePlaced is Antenna) &&
                    ((gridArray[y, x].GetCellContent().Count < 1 || (gridArray[y, x].GetCellContent().Count < 2 && toBePlaced is Antenna)) ||
                    gridArray[y, x].GetCellContent().Count == 1 && gridArray[y, x].GetAntenna() != null))
                {
                    GameObject tmpObject = (GameObject)Instantiate(Resources.Load(toBePlaced.GetResourcePath()), gridArray[y, x].GetTile().transform.position, Quaternion.identity);
                    tmpObject.transform.SetParent(gridArray[y, x].GetTile().transform);
                    tmpObject.transform.localScale = newScale;
                    toBePlaced.visualObject = tmpObject;

                    SetLayerRecursive(tmpObject, LayerMask.NameToLayer("Module"));

                    if (toBePlaced is Antenna)
                    {
                        totalAntennas += 1;
                    }

                    gridArray[y, x].AddCellContent(toBePlaced);

                    
                    UpdateNetwork();
                }
                else
                {
                    Debug.Log("Can't place this module there.");
                }
            }
        } else
        {
            Debug.Log("Maximum number of Antennas already placed.");
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



    /// <summary>
    /// Sets the layer of the given object and all its children to a given layer.
    /// </summary>
    /// <param name="currentObject">The parent object to get the layer set.</param>
    /// <param name="newLayer">The layer which to set the objects to.</param>
    private void SetLayerRecursive(GameObject currentObject, int newLayer)
    {
        currentObject.layer = newLayer;

        foreach (Transform child in currentObject.transform)
        {
            child.gameObject.layer = newLayer;

            if(child.GetComponentInChildren<Transform>() != null)
            {
                SetLayerRecursive(child.gameObject, newLayer);
            }
        }
    }


    /// <summary>
    /// Removes every instance of a module type, matching the variable 'toBeRemoved', from the cell which the mouse is pointing at.
    /// </summary>
    /// <remarks>
    /// This method should only be called upon if the variable 'toBeRemoved' has been set.
    /// </remarks>
    /// <exception cref="System.ArgumentException">
    /// Throws if 'toBeRemoved' has not been set.
    /// </exception>
    public void RemoveModule()
    {
        if(toBeRemoved == null)
        {
            throw new System.ArgumentException("'toBeRemoved' has not been set prior to calling this method.");
        }

        MouseToGridCoord(out int y, out int x);

        if (x >= 0 && y >= 0 && x < cols && y < rows)
        {
            if (gridArray[y, x].RemoveCellContent(toBeRemoved) &&
                toBeRemoved is Antenna)
            {
                totalAntennas--;
            }
            
            UpdateNetwork();
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


    /// <summary>
    /// Locates which cell corresponds to the mouse position and sets the out y and out x to the correct grid-coordinates.
    /// </summary>
    /// <param name="y">This out variable will be set to the y coordinate of the grid corresponding to the position of the mouse.</param>
    /// <param name="x">This out variable will be set to the x coordinate of the grid corresponding to the position of the mouse.</param>
    private void MouseToGridCoord(out int y, out int x)
    {
        Camera cam = Camera.main;
        Vector3 worldPoint = Input.mousePosition;
        worldPoint.z = Mathf.Abs(cam.transform.position.z);
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(worldPoint);
        mouseWorldPosition.z = 0f;

        WorldPosToGridCoord(mouseWorldPosition, out y, out x);
    }


    /// <summary>
    /// Translates the world position to y- and x- coordinates.
    /// </summary>
    /// <param name="worldPosition">The position in world coordinates to be translated.</param>
    /// <param name="y">This variable will be set to the y value of the grid corresponding to the position worldPosition.</param>
    /// <param name="x">This variable will be set to the x value of the grid corresponding to the position worldPosition.</param>
    private void WorldPosToGridCoord(Vector3 worldPosition, out int y, out int x)
    {
        var tmp = transform.InverseTransformPoint(worldPosition);
        x = Mathf.FloorToInt(tmp[0] / tileSize);
        y = -Mathf.FloorToInt(tmp[1] / tileSize);

    }



    /// <summary>
    /// Updates the network by rebuilding it using the Network class and then setting each tile to their correct color.
    /// </summary>
    public void UpdateNetwork()
    {
        ResetNetwork();

        DestroyNetworkFlow();

        network.BuildNetwork(gridArray);


        float total = 0;
        float count = 0;
        float criticalTotal = 0;
        float criticalCount = 0;
        foreach (Cell cell in gridArray)
        {
            total += (float)cell.GetSignalStr();
            if (criticalCoverage && cell.HasCriticalModule())
            {
                criticalCount++;
                criticalTotal += (float)cell.GetSignalStr();
            }

            SetTileColor(cell);

            if (createNetworkArrows)
            {
                CreateNetworkFlow(cell);
            }

            count++;
        }

        if(criticalCount == 0)
        {
            criticalTotal = 0;
            criticalCount = 1;
        }

        if (criticalCoverage) {
            criticalCoverageScript.SetCoverage(criticalTotal / criticalCount, colors["coverage"]);
        }
        coverageBar.SetCoverage(total/count, colors["coverage"]);
        antennaStatistics.setAntennaStatistics(totalAntennas, maxAntennas); 
    }

    /// <summary>
    /// Resets the signal strength of every cell in the grid.
    /// </summary>
    private void ResetNetwork()
    {
        foreach (Cell cell in gridArray)
        {
            cell.ResetSignalStr();
            if(cell.GetAntenna() != null)
            {
                cell.GetAntenna().ResetDemand();
            }
            
        }
    }


    /// <summary>
    /// Builds and displays the visual grid of tiles on the screen and stores them in their corresponding Cell object.
    /// </summary>
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


    /// <summary>
    /// Sets the color of a given cell's tile based on cell's signal strength.
    /// </summary>
    /// <param name="cell">The cell corresponding to the tile to change color of.</param>
    public void SetTileColor(Cell cell)
    {
        float[] rgbt = GetColor(cell);

        var tileRenderer = cell.GetTile().transform.GetChild(0).GetComponent<Renderer>();

        tileRenderer.material.SetColor("_Color", new Color(rgbt[0], rgbt[1], rgbt[2], rgbt[3])); 

    }


    /// <summary>
    /// Returns the correct color based on which type to simulate on the grid.
    /// </summary>
    /// <param name="cell">The cell to create the color for.</param>
    /// <returns>The rgbt color code for this cell.</returns>
    private float[] GetColor(Cell cell)
    {
        Colors color;
        try
        {
            color = colors[simulationModeSelected];
        }
        catch (System.ArgumentException)
        {
            throw new System.ArgumentException("The selected color gradient name in 'simulationModeSelected' does not exist.");
        }

        float[] rgbt = null;

        if (simulationModeSelected.Equals("coverage"))
        {
            double signalStr = cell.GetSignalStr();
            if (1 <= signalStr)
            {
                rgbt = color.GetGradientColor(signalStr);
            }
            else
            {
                rgbt = Colors.gray;
            }
        } else if (simulationModeSelected.Equals("capacity"))
        {
            if(cell.GetSignalDir() != null)
            {
                rgbt = color.GetGradientColor(cell.GetSignalDir().originCell.GetAntenna().GetDemand());
            } else
            {
                rgbt = Colors.gray;
            }
        }


        if(rgbt == null)
        {
            throw new System.Exception("rgbt was never assigned.");
        }
        return rgbt;

    }


    /// <summary>
    /// Visualizes the networkflow through the given cell.
    /// </summary>
    /// <param name="cell">The cell to visualize the network flow in.</param>
    /// <exception cref="System.ArgumentException">
    /// Throws if the direction of the cell is null even if it has a signal strength.
    /// </exception>
    private void CreateNetworkFlow(Cell cell)
    {
        if(cell.GetSignalDir() == null ^ cell.GetSignalStr() == 0)
        {
            throw new System.ArgumentException("Direction and signalStr doesn't match.");
        }

        if(cell.GetSignalDir() == null)
        {
            return;
        }

        GameObject arrow = (GameObject)Instantiate(Resources.Load(cell.GetSignalDir().GetResourcePath()));
        networkFlowVisuals.Add(arrow);

        arrow.transform.SetParent(gridArray[cell.GetY(), cell.GetX()].GetTile().transform);

        arrow.transform.localPosition = new Vector2(0.5f, 0.5f);

        var rotation = new Vector3(0, 0, cell.GetSignalDir().GetDirectionInDegrees(cell.GetSignalDirDiagonal()));
        arrow.transform.Rotate(rotation);

    }


    /// <summary>
    /// Destroys the network flow visualization throughout the entire grid.
    /// </summary>
    private void DestroyNetworkFlow()
    {
        for (int i = 0; i < networkFlowVisuals.Count; i++)
        {
            Destroy(networkFlowVisuals[i]);
        }
    }


    /// <summary>
    /// Resets the entire grid by emptying every cell of content, 
    /// setting every cell's signal strength to zero and then updating the network.
    /// </summary>
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
        totalAntennas = 0; 
        UpdateNetwork();
    }


    /// <returns>Number of rows of the grid.</returns>
    public int GetRows()
    {
        return rows;
    }


    /// <returns>Number of columns of the grid.</returns>
    public int GetCols()
    {
        return cols;
    }

    public void SetAndUpdateMaxAntennas(int maxAntennas)
    {
        this.maxAntennas = maxAntennas;
        antennaStatistics.setAntennaStatistics(totalAntennas, maxAntennas);

    }

}