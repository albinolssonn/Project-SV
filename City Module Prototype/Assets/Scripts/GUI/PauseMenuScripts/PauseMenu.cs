using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static int pausedOnLayer = 0;

    public GameObject pauseMenuUi;
    public GameObject informationPageUi;
    public GameObject gridSizeUi;
    public GameObject settingsPageUi;
    public GameObject loadPrebuiltCityUi;
    public GameObject savePrebuiltCityUi;


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
                        pausedOnLayer = 0;
                        break;

                    case 2:
                        CloseLoadPreCityPage();
                        CloseSavePreCityPage(); 
                        CloseGridSizePage();
                        pausedOnLayer = 1;
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
        Time.timeScale = 1f;
        GameIsPaused = false;
    }


    /// <summary>
    /// Opens the menu window.
    /// </summary>
    public void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }


    /// <summary>
    /// Opens the credits window.
    /// </summary>
    public void InformationPage()
    {
        informationPageUi.SetActive(true);
        pauseMenuUi.SetActive(false);
        pausedOnLayer = 1;

    }


    /// <summary>
    /// Closes the credits window.
    /// </summary>
    public void CloseInformationPage()
    {
        informationPageUi.SetActive(false);
        pauseMenuUi.SetActive(true);
    }


    /// <summary>
    /// Opens the settings window.
    /// </summary>
    public void SettingsPage()
    {
        settingsPageUi.SetActive(true);
        pauseMenuUi.SetActive(false);
        pausedOnLayer = 1;
    }


    /// <summary>
    /// Closes the settings window.
    /// </summary>
    public void CloseSettingsPage()
    {
        settingsPageUi.SetActive(false);
        pauseMenuUi.SetActive(true);
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
    }


    /// <summary>
    /// Opens the pre-configured cities window.
    /// </summary>
    public void LoadPreCityPage()
    {
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
    }

    /// <summary>
    /// Opens the save pre-configured cities window.
    /// </summary>
    public void SavePreCityPage()
    {
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