using UnityEngine;

/*
 * This class contains the functionality used for the pause menu (ESC).
 */
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUi;
    public GameObject informationPageUi;
    public GameObject gridSizeUi;
    public GameObject settingsPageUi;
    public GameObject prebuiltCityUi;

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void InformationPage()
    {
        informationPageUi.SetActive(true);
        pauseMenuUi.SetActive(false);
    }

    public void CloseInformationPage()
    {
        informationPageUi.SetActive(false);
        pauseMenuUi.SetActive(true);
    }

    public void SettingsPage()
    {
        settingsPageUi.SetActive(true);
        pauseMenuUi.SetActive(false);
    }

    public void CloseSettingsPage()
    {
        settingsPageUi.SetActive(false);
        pauseMenuUi.SetActive(true);
    }

    public void GridSizePage()
    {
        gridSizeUi.SetActive(true);
        settingsPageUi.SetActive(false);
    }

    public void CloseGridSizePage()
    {
        gridSizeUi.SetActive(false);
        settingsPageUi.SetActive(true);
    }
    public void PreCityPage()
    {
        prebuiltCityUi.SetActive(true);
        settingsPageUi.SetActive(false);
    }

    public void ClosePreCityPage()
    {
        prebuiltCityUi.SetActive(false);
        settingsPageUi.SetActive(true);
    }

    public void Reset_btn()
    {
        GridManager grid = GameObject.FindGameObjectsWithTag("Grid")[0].GetComponent<GridManager>();
        grid.ResetGrid();
        Resume();
    }

    public void Menu_Btn()
    {
        Pause();
    }

    public void Quit_btn()
    {
        Debug.Log("Quit Application");
        Application.Quit();
    }
}