using System.IO;
using TMPro;
using UnityEngine;

// HACK: Create pre-configured city button.
// Here you can add button methods to create additional pre-configured cities.
// First create a method in the class 'PreConfCities' as instructed in the method 'LoadPreconfigCity'
// and then call on 'LoadPreconfigCity' with the correct index as you define in the switch case.
// Then attach PrebuiltCities_Panel to your button in Unity and call the method you created here on click and it's done.

public class LoadPreBuiltScript : MonoBehaviour
{

    private GridManager gridManager;
    public TMP_Dropdown dropdown;


    public GameObject deleteMenuUi;
    public TMP_Text deleteMenuText;



    public void Start()
    {
        gridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }

    /// <summary>
    /// Loads dropdown menu with menu options.
    /// </summary>
    public void LoadDropdown()
    {
        dropdown.options.Clear();

        string[] configFiles;

        try
        {
            configFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/ConfigFiles/");
        }
        catch (DirectoryNotFoundException)
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/ConfigFiles");
            configFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/ConfigFiles/");
        }


        foreach (string file in configFiles)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = Path.GetFileNameWithoutExtension(file) });
        }
        dropdown.RefreshShownValue();
        dropdown.value = 0;
    }

    /// <summary>
    /// Loads a pre-configured city.
    /// </summary>
    public void LoadBtn()
    {
        if(dropdown.options.Count > 0)
        {
            gridManager.LoadPreconfigCity(dropdown.options[dropdown.value].text);
            gridManager.SetMessage("Load successful.");
        }
        else
        {
            gridManager.SetMessage("There are no save files."); 
        }
    }

    public void DeleteCity()
    {
        if (dropdown.options[dropdown.value].text.Length == 0)
        {
            gridManager.SetMessage("Choose a file to delete.");
            return;
        }

        int filesBefore = Directory.GetFiles(Directory.GetCurrentDirectory() + "/ConfigFiles/").Length;
        try
        {
            File.Delete(Directory.GetCurrentDirectory() + "/ConfigFiles/" + dropdown.options[dropdown.value].text + ".txt");
        }
        catch (System.ArgumentException)
        {
            gridManager.SetMessage("Invalid file location.");
        }
        catch (DirectoryNotFoundException)
        {
            gridManager.SetMessage("Unreachable file path.");
        }
        catch (IOException)
        {
            gridManager.SetMessage("The specified file is in use.");
        }

        int filesAfter = Directory.GetFiles(Directory.GetCurrentDirectory() + "/ConfigFiles/").Length;

        if (filesAfter < filesBefore)
        {
            gridManager.SetMessage("City '" + dropdown.options[dropdown.value].text + "' has been deleted.");
            LoadDropdown();
        }
        else
        {
            gridManager.SetMessage("City '" + dropdown.options[dropdown.value].text + "' could not be deleted.");
        }
    }



    public void OpenDeleteMenu()
    {
        deleteMenuUi.SetActive(true);
        deleteMenuText.text = "Are you sure you want to delete '" + dropdown.options[dropdown.value].text + "'?";
        PauseMenu.pausedOnLayer = 3;
    }

    public void CloseDeleteMenu()
    {
        deleteMenuUi.SetActive(false);
        PauseMenu.pausedOnLayer = 2;
    }




}
