using UnityEngine;

/*
 * This class contains the functionality used for the pause menu (ESC).
 */
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUi; 

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume(); 
            } else
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