using System.Collections.Generic;
using System.Threading;
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
    private Vector3 gridScale;
    private Network network;
    private Cell[,] gridArray;
    private List<GameObject> networkFlowVisuals;
    private string simulationModeSelected;
    private Dictionary<string, Colors> colors;
    private InformationScript informationScript;
    private List<Cell> antennaCells;

    private GameObject gridManager;

    private int totalAntennas;
    private int maxAntennas;


    /// <summary>Set to true if game mode of limited antennas is wanted.</summary>
    public static bool limitedAntennasMode = false;

    /// <summary>Set to true if game mode of critical coverage is wanted.</summary>
    public static bool criticalMode = false;

    /// <summary>Set to false if colored network flow arrows is not wanted.</summary>
    public static bool networkFlowColorsActive = true;

    /// <summary>The strength of which an Antenna transmits a signal with.</summary>
    public static readonly double baseSignalStr = 10;

    /// <summary>The capacity of which an Antenna can supply users with.</summary>
    public static readonly double baseCapacity = 40;

    /// <summary>The reduction in signal strength for traveling one step in any direction.</summary>
    /// <remarks>Should be subtracted from the signal strength each time it travels from one cell to another.</remarks>
    public static readonly double distancePenalty = 1;

    /// <summary>The reduction in signal strength for traveling through a cell with a higher max height than the cell which the antenna is located in.</summary>
    public static readonly double heightPenalty = 2;

    private bool shiftHeldDown;
    private bool createNetworkArrows;

    public AntennaStatistics antennaStatistics;

    public SignalBarScript coverageBarScript;
    public CapacityBarScript capacityBarScript;
    public CriticalCoverageScript criticalCoverageScript;
    public CriticalCapacityScript criticalCapacityScript;



    // Start is called before the first frame update
    public void Start()
    {
        rows = 20;
        cols = 20;
        tileSize = 110F;
        totalAntennas = 0;
        simulationModeSelected = "coverage";
        shiftHeldDown = false;
        createNetworkArrows = false;
        networkFlowVisuals = new List<GameObject>();
        gridManager = GameObject.FindGameObjectsWithTag("Grid")[0];

        colors = new Dictionary<string, Colors>
        {
            { "coverage", new RedGreen(baseSignalStr) },
            { "capacity", new WhiteBlue(baseCapacity) },
            { "none", null }
        };

        network = new Network(baseSignalStr, distancePenalty, heightPenalty);
        gridArray = GridUtils.BuildArray(rows, cols);
        antennaCells = new List<Cell>();


        float ratio = (Screen.height - 100) / (rows * tileSize);
        gridScale = new Vector3(ratio, ratio, 1f);

        GenerateGrid();
        CenterGrid();

        coverageBarScript.SetCoverage(0, colors["coverage"]);
        capacityBarScript.SetCapacity(0, colors["capacity"]);
        criticalCoverageScript.SetCoverage(0, colors["coverage"]);
        criticalCapacityScript.SetCapacity(0, colors["capacity"]);

        antennaStatistics.setAntennaStatistics(totalAntennas, 0);

        informationScript = GameObject.Find("Information_Label").GetComponent<InformationScript>();


        transform.localScale = gridScale;
        UpdateNetwork();

    }


    // Update is called once per frame. It checks if the mouse has been clicked to place down a module.
    public void Update()
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
                MouseToGridCoord(out int y, out int x);
                AddModule(y, x);
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

        var scaledTileSize = tileSize * gridScale[0];

        float[] finalPosition = new float[2] { -scaledTileSize, 0 };

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
        }
        else
        {
            DestroyNetworkFlow();
        }

    }


    /// <summary>
    /// Toggles if the gamification "Limited Antennas" is active or not.
    /// </summary>
    /// <returns>The new state of the toggle.</returns>
    public bool ToggleLimitedAntennas()
    {
        limitedAntennasMode = !limitedAntennasMode;

        if (!limitedAntennasMode)
        {
            antennaStatistics.setAntennaStatistics(totalAntennas, maxAntennas);
        }

        return limitedAntennasMode;
    }


    /// <summary>
    /// Toggles if the gamification "Critical Coverage" is active or not.
    /// </summary>
    /// <returns>The new state of the toggle.</returns>
    public bool ToggleCritical()
    {
        criticalMode = !criticalMode;

        if (criticalMode)
        {
            float criticalCoverageCount = 0;
            float criticalCapacityCount = 0;

            float criticalCoverage = 0;
            float criticalCapacity = 0;

            foreach (Cell cell in gridArray)
            {
                if (cell.HasCriticalModule())
                {
                    criticalCoverageCount++;
                    criticalCoverage += (float)cell.GetSignalStr();

                    if(cell.GetAvailableCapacity() < baseCapacity)
                    {
                        criticalCapacityCount++;
                        criticalCapacity += (float)cell.GetAvailableCapacity();
                    }

                }
            }

            if (criticalCoverageCount == 0)
            {
                criticalCoverage = 0;
                criticalCoverageCount = 1;
            }
            if(criticalCapacityCount == 0)
            {
                criticalCapacity = 0;
                criticalCapacityCount = 1;
            }

            criticalCoverageScript.SetCoverage(criticalCoverage / criticalCoverageCount, colors["coverage"]);
            criticalCapacityScript.SetCapacity(criticalCapacity / criticalCapacityCount, colors["capacity"]);

        }


        return criticalMode;
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
    private void AddModule(int y, int x)
    {
        if (toBePlaced == null)
        {
            throw new System.ArgumentException("'toBeRemoved' has not been set prior to calling this method.");
        }

        if (!(limitedAntennasMode && toBePlaced is Antenna && maxAntennas <= totalAntennas))
        {
            if (x >= 0 && y >= 0 && x < cols && y < rows)
            {
                if (!(gridArray[y, x].HasAntenna() && toBePlaced is Antenna))
                {
                    if (gridArray[y, x].GetCellContent().Count == 0 || (gridArray[y, x].GetCellContent().Count < 2 && toBePlaced is Antenna) ||
                    gridArray[y, x].GetCellContent().Count == 1 && gridArray[y, x].HasAntenna())
                    {
                        AddModuleVisual(y, x);

                        gridArray[y, x].AddCellContent(toBePlaced);

                        if (toBePlaced is Antenna)
                        {
                            antennaCells.Add(gridArray[y, x]);
                            AntennaPlaced(y, x);
                        }
                        else
                        {
                            UpdateNetwork();
                        }
                    }
                    else
                    {
                        SetErrorMessage("A cell can only have a single building type in it.");
                    }
                }
                else
                {
                    SetErrorMessage("This cell already has an Antenna in it.");
                }
            }
        }
        else
        {
            SetErrorMessage("Maximum number of antennas already placed.");
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
    /// Places a GameObject of the module assigned to 'toBePlaced'.
    /// </summary>
    /// <param name="y">The y-coordinate for the cell to recieve the module.</param>
    /// <param name="x">The x-coordinate for the cell to recieve the module.</param>
    /// <remarks>
    /// This method should only be called upon if the variable 'toBePlaced' has been set.
    /// </remarks>
    /// <exception cref="System.ArgumentException">
    /// Throws if 'toBePlaced' has not been set.
    /// </exception>
    private void AddModuleVisual(int y, int x)
    {
        if (toBePlaced == null)
        {
            throw new System.ArgumentException("'toBeRemoved' has not been set prior to calling this method.");
        }

        GameObject tmpObject = (GameObject)Instantiate(Resources.Load(toBePlaced.GetResourcePath()), gridArray[y, x].GetTile().transform.position, Quaternion.identity);
        tmpObject.transform.SetParent(gridArray[y, x].GetTile().transform);
        tmpObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        toBePlaced.visualObject = tmpObject;

        SetLayerRecursive(tmpObject, LayerMask.NameToLayer("Module"));

        if (toBePlaced is Antenna)
        {
            totalAntennas += 1;
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

            if (child.GetComponentInChildren<Transform>() != null)
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
        if (toBeRemoved == null)
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
                antennaCells.Remove(gridArray[y, x]);
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

    public void AntennaPlaced(int y, int x)
    {
        DestroyNetworkFlow();

        network.BuildSingleNetwork(gridArray[y, x], gridArray);

        UpdateStatistics();
    }

    /// <summary>
    /// Updates the network by rebuilding it using the Network class and then setting each tile to their correct color.
    /// </summary>
    public void UpdateNetwork()
    {
        ResetNetwork();

        network.BuildEntireNetwork(gridArray, antennaCells);

        UpdateStatistics();
    }



    private void UpdateStatistics()
    {
        float totalCoverage = 0;
        float criticalCoverage = 0;

        float totalCapacity = 0;
        float criticalCapacity = 0;

        float totalCount = 0;
        float totalCapacityCount = 0;
        float criticalCount = 0;

        foreach (Cell cell in gridArray)
        {
            SetTileColor(cell);

            if (createNetworkArrows)
            {
                CreateNetworkFlow(cell);
            }

            if (criticalMode && cell.HasCriticalModule())
            {
                criticalCoverage += (float)cell.GetSignalStr();
                criticalCapacity += (float)cell.GetAvailableCapacity();

                criticalCount++;
            }

            if (cell.GetSignalDir() != null && cell.GetSignalDir().originCell.GetAvailableCapacity() < baseCapacity)
            {
                totalCapacity += (float)cell.GetAvailableCapacity();
                totalCapacityCount++;
            }


            totalCoverage += (float)cell.GetSignalStr();
            totalCount++;
        }

        if (criticalCount == 0)
        {
            criticalCoverage = 0;
            criticalCount = 1;
        }

        if (totalCapacityCount == 0)
        {
            totalCapacityCount = 1;
            totalCapacity = 0;
        }

        if (criticalMode)
        {
            criticalCoverageScript.SetCoverage(criticalCoverage / criticalCount, colors["coverage"]);
            criticalCapacityScript.SetCapacity(criticalCapacity / criticalCount, colors["capacity"]);
        }
        coverageBarScript.SetCoverage(totalCoverage / totalCount, colors["coverage"]);
        capacityBarScript.SetCapacity(totalCapacity / totalCapacityCount, colors["capacity"]);
        antennaStatistics.setAntennaStatistics(totalAntennas, maxAntennas);
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

                foreach (Module module in gridArray[row, col].GetCellContent())
                {
                    toBePlaced = module;
                    AddModuleVisual(row, col);
                    toBePlaced = null;
                }
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
            throw new System.ArgumentException("The selected color gradient string in 'simulationModeSelected' does not exist.");
        }

        float[] rgbt;


        switch (simulationModeSelected)
        {
            case "coverage":
                double signalStr = cell.GetSignalStr();
                if (1 <= signalStr)
                {
                    rgbt = color.GetGradientColor(signalStr);
                }
                else
                {
                    rgbt = Colors.gray;
                }
                break;


            case "capacity":
                if (cell.GetSignalDir() != null)
                {
                    rgbt = color.GetGradientColor(cell.GetSignalDir().originCell.GetAntenna().AvailableCapacity());
                }
                else
                {
                    rgbt = Colors.gray;
                }
                break;

            case "none":
                rgbt = Colors.white;
                break;

            default:
                throw new System.Exception("This should be unreachable.");
        }


        if (rgbt == null)
        {
            throw new System.Exception("'rgbt' was never assigned.");
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
        if (cell.GetSignalDir() == null ^ cell.GetSignalStr() == 0)
        {
            throw new System.ArgumentException("Direction and signalStr doesn't match.");
        }

        var oldChild = gridArray[cell.GetY(), cell.GetX()].GetTile().transform.Find("Arrow(Clone)");
        if (oldChild != null)
        {
            Destroy(oldChild.gameObject);
        }
        else
        {
            oldChild = gridArray[cell.GetY(), cell.GetX()].GetTile().transform.Find("Dot(Clone)");
            if (oldChild != null)
            {
                Destroy(oldChild.gameObject);
            }
        }


        if (cell.GetSignalDir() == null)
        {
            return;
        }

        GameObject arrow = (GameObject)Instantiate(Resources.Load(cell.GetSignalDir().GetResourcePath()));

        Origin originDirection = (Origin)cell.GetSignalDir().originCell.GetSignalDir();
        Renderer arrowRenderer = arrow.transform.GetChild(0).GetComponent<Renderer>();


        if (networkFlowColorsActive)
        {
            arrowRenderer.material.SetColor("_Color", Network.networkFlowColors[originDirection.networkFlowColorIndex]);
        }
        else
        {
            arrowRenderer.material.SetColor("_Color", Color.black);

        }

        networkFlowVisuals.Add(arrow);

        arrow.transform.SetParent(gridArray[cell.GetY(), cell.GetX()].GetTile().transform);

        arrow.transform.localPosition = new Vector2(0.5f, 0.5f);

        var rotation = new Vector3(0, 0, cell.GetSignalDir().GetDirectionInDegrees(cell.GetSignalDirDiagonal()));
        arrow.transform.Rotate(rotation);

        arrow.transform.localScale = new Vector3(.25f, .25f, 1f);


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
        antennaCells.Clear();
        UpdateNetwork();
    }


    /// <summary>
    /// Resets the signal strength of every Cell and the demand of every Antenna in the grid.
    /// </summary>
    /// <param name="andAntennas">If to reset cells with Antennas as well or not.</param>
    private void ResetNetwork()
    {
        foreach (Cell cell in gridArray)
        {
            if (cell.HasAntenna())
            {
                cell.GetAntenna().ResetDemand();
            }
            else
            {
                cell.ResetSignalStr();
            }
        }
    }



    public void LoadPreconfigCity(int index)
    {
        DestroyGrid();
        totalAntennas = 0;

        switch (index)
        {
            case 0:
                gridArray = PreConfCities.GetConfig0(out rows, out cols);
                break;

            case 1:
                gridArray = PreConfCities.GetConfig1(out rows, out cols);
                break;

            case 2:
                gridArray = PreConfCities.GetConfig2(out rows, out cols);
                break;
            default:
                throw new System.Exception("This should be unreachable.");
        }

        GenerateGrid();
        UpdateNetwork();
        CenterGrid();
    }


    /// <summary>
    /// Sets the grid to a new size by adding or subtracting cols and rows towards the right and down.
    /// Transfers any modules from the old grid to the new which are within the new size bounds.
    /// </summary>
    /// <param name="newRows">Number of rows for the new grid.</param>
    /// <param name="newCols">Number of cols for the new grid.</param>
    public void SetNewGridSize(int newRows, int newCols)
    {
        rows = newRows;
        cols = newCols;
        gridArray = GridUtils.ResizeGrid(gridArray, rows, cols, out antennaCells);
        totalAntennas = 0;

        float ratio = (Screen.height - 100) / (rows * tileSize);
        gridScale = new Vector3(ratio, ratio, 1f);

        transform.localScale = gridScale;

        DestroyGrid();
        GenerateGrid();
        UpdateNetwork();
        CenterGrid();
    }


    /// <summary>
    /// Destroys the current visuals for the grid.
    /// </summary>
    private void DestroyGrid()
    {
        foreach (Transform child in gridManager.transform)
        {
            Destroy(child.gameObject);
        }
    }


    /// <summary>
    /// Set 'maxAntennas' to a new value and update the statistics panel accordingly.
    /// </summary>
    /// <param name="maxAntennas">The new maximum number of Antennas allowed.</param>
    public void SetAndUpdateMaxAntennas(int maxAntennas)
    {
        this.maxAntennas = maxAntennas;
        antennaStatistics.setAntennaStatistics(totalAntennas, maxAntennas);

    }


    /// <summary>
    /// Changes which simulation to show, such as coverage or capacity, 
    /// and then updates the colors on the grid accordingly.
    /// </summary>
    /// <param name="mode">Which simulation mode to show.</param>
    public void SetSimulationMode(string mode)
    {
        if (!colors.ContainsKey(mode))
        {
            throw new System.ArgumentException("The given mode does not exist.");
        }
        simulationModeSelected = mode;
        foreach (Cell cell in gridArray)
        {
            SetTileColor(cell);
        }
    }


    /// <summary>
    /// Prints a string on the screen for the user.
    /// </summary>
    /// <param name="error">The text to print out.</param>
    public void SetErrorMessage(string error)
    {
        informationScript.SetInformationText(error);
    }

    public string GetSimulationMode()
    {
        return simulationModeSelected; 
    }

    public bool GetCriticalMode()
    {
        return criticalMode; 
    }

}