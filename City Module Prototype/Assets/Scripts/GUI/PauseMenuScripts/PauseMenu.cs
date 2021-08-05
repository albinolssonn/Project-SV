using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static int pausedOnLayer = 0;

    public GameObject pauseMenuUi;
    public GameObject mainPageUi;
    public GameObject informationPageUi;
    public GameObject gridSizeUi;
    public GameObject settingsPageUi;
    public GameObject loadPrebuiltCityUi;
    public GameObject savePrebuiltCityUi;
    public GameObject loadDropDown;
    public GameObject saveDropDown;
    


    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                switch (pausedOnLayer)
                {
                    case 0:
                        Resume();
                        break;

                    case 1:
                        CloseInformationPage();
                        CloseSettingsPage();
                        break;

                    case 2:
                        CloseLoadPreCityPage();
                        CloseSavePreCityPage(); 
                        CloseGridSizePage();
                        break;

                    case 3:
                        break;

                    default:
                        throw new System.Exception("This should be unreachable.");
                }

            }
            else
            {
                Pause();
            }

        }
    }


    /// <summary>
    /// Closes the menu window.
    /// </summary>
    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        //Time.timeScale = 1f; //HACK: Uncomment this line if you want time to pause as well.
        GameIsPaused = false;
        pausedOnLayer = 0;

    }


    /// <summary>
    /// Opens the menu window.
    /// </summary>
    public void Pause()
    {
        pauseMenuUi.SetActive(true);
        //Time.timeScale = 0f; //HACK: Uncomment this line if you want time to pause as well.
        GameIsPaused = true;
        pausedOnLayer = 0;

    }


    /// <summary>
    /// Opens the credits window.
    /// </summary>
    public void InformationPage()
    {
        informationPageUi.SetActive(true);
        mainPageUi.SetActive(false);
        pausedOnLayer = 1;

    }


    /// <summary>
    /// Closes the credits window.
    /// </summary>
    public void CloseInformationPage()
    {
        informationPageUi.SetActive(false);
        mainPageUi.SetActive(true);
        pausedOnLayer = 0;

    }


    /// <summary>
    /// Opens the settings window.
    /// </summary>
    public void SettingsPage()
    {
        settingsPageUi.SetActive(true);
        mainPageUi.SetActive(false);
        pausedOnLayer = 1;
    }


    /// <summary>
    /// Closes the settings window.
    /// </summary>
    public void CloseSettingsPage()
    {
        settingsPageUi.SetActive(false);
        mainPageUi.SetActive(true);
        pausedOnLayer = 0;

    }


    /// <summary>
    /// Opens the grid size window.
    /// </summary>
    public void GridSizePage()
    {
        gridSizeUi.SetActive(true);
        settingsPageUi.SetActive(false);
        pausedOnLayer = 2;
    }


    /// <summary>
    /// Closes the grid size window.
    /// </summary>
    public void CloseGridSizePage()
    {
        gridSizeUi.SetActive(false);
        settingsPageUi.SetActive(true);
        pausedOnLayer = 1;

    }


    /// <summary>
    /// Opens the pre-configured cities window.
    /// </summary>
    public void LoadPreCityPage()
    {
        loadDropDown.GetComponent<LoadPreBuiltScript>().LoadDropdown();
        loadPrebuiltCityUi.SetActive(true);
        settingsPageUi.SetActive(false);
        pausedOnLayer = 2;
        
    }


    /// <summary>
    /// Closes the pre-configured cities window.
    /// </summary>
    public void CloseLoadPreCityPage()
    {
        loadPrebuiltCityUi.SetActive(false);
        settingsPageUi.SetActive(true);
        pausedOnLayer = 1;

    }

    /// <summary>
    /// Opens the save pre-configured cities window.
    /// </summary>
    public void SavePreCityPage()
    {
        saveDropDown.GetComponent<SavePreBuiltScript>().LoadDropdown();
        savePrebuiltCityUi.SetActive(true);
        settingsPageUi.SetActive(false);
        pausedOnLayer = 2;
    }

    /// <summary>
    /// Closes the save pre-configured cities window.
    /// </summary>
    public void CloseSavePreCityPage()
    {
        savePrebuiltCityUi.SetActive(false);
        settingsPageUi.SetActive(true);
        pausedOnLayer = 1;

    }


 

    /// <summary>
    /// Resets the grid.
    /// </summary>
    public void Reset_btn()
    {
        GridManager grid = GameObject.FindGameObjectsWithTag("Grid")[0].GetComponent<GridManager>();
        grid.ResetGrid();
        Resume();
    }


    /// <summary>
    /// Opens the menu window.
    /// </summary>
    public void Menu_Btn()
    {
        Pause();
    }


    /// <summary>
    /// Quits the application.
    /// </summary>
    public void Quit_btn()
    {
        Debug.Log("Quit Application");
        Application.Quit();
    }
}