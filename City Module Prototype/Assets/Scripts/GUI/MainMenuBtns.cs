using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuBtns : MonoBehaviour
{
    public void Start_btn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void Settings_btn()
    {
        Debug.Log("I Clicked Settings!"); 
    }

    public void Quit_btn()
    {
        Debug.Log("I Clicked Quit!"); 
    }
}
