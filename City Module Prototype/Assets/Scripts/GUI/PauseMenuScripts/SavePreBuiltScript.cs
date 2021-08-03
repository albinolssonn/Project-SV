using System.IO;
using TMPro;
using UnityEngine;

public class SavePreBuiltScript : MonoBehaviour
{
    private GridManager gridManager;
    public TMP_Dropdown dropdown;
    public TMP_InputField inputField;
    private int numberOfFiles;
    private bool updateText;

    public GameObject deleteMenuUi;
    public TMP_Text deleteMenuText;


    public void Start()
    {
        gridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
        dropdown.onValueChanged.AddListener(delegate { DropDownItemSelected(); });
        inputField.onSelect.AddListener(delegate { DeselectDropdown(); });
        updateText = true;
    }

    /// <summary>
    /// Loads dropdown menu with menu options.
    /// </summary>
    public void LoadDropdown()
    {
        dropdown.options.Clear();

        string[] configFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/ConfigFiles/");


        foreach (string file in configFiles)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = Path.GetFileNameWithoutExtension(file) });
        }

        dropdown.options.Add(new TMP_Dropdown.OptionData() { text = "" });
        numberOfFiles = configFiles.Length;
        dropdown.value = numberOfFiles;
        dropdown.RefreshShownValue();

    }

    /// <summary>
    /// Takes value from dropdown menu and inserts to inputfield. 
    /// </summary>
    private void DropDownItemSelected()
    {
        if (updateText)
        {
            inputField.text = dropdown.options[dropdown.value].text;
        }

        updateText = true;
    }


    private void DeselectDropdown()
    {
        updateText = false;
        dropdown.value = numberOfFiles;
    }


    /// <summary>
    /// Takes input string from inputfield and passes into gridmanager.
    /// </summary>
    public void SaveBtn()
    {
        if(inputField.text.Length == 0)
        {
            gridManager.SetMessage("Write filename or choose an existing file.");
            return; 
        }

        gridManager.SavePreconfigCity(inputField.text);
        inputField.text = "";
        dropdown.value = 0;
        LoadDropdown();
        gridManager.SetMessage("City saved.");



    }



    public void DeleteCity()
    {
        if (inputField.text.Length == 0)
        {
            gridManager.SetMessage("Write filename or chose an existing file.");
            return;
        }

        int filesBefore = Directory.GetFiles(Directory.GetCurrentDirectory() + "/ConfigFiles/").Length;
        try
        {
            File.Delete(Directory.GetCurrentDirectory() + "/ConfigFiles/" + inputField.text + ".txt");
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

        if(filesAfter < filesBefore)
        {
            gridManager.SetMessage("City '" + inputField.text + "' has been deleted.");
            inputField.text = "";
            LoadDropdown(); 
        }
        else
        {
            gridManager.SetMessage("City '" + inputField.text + "' could not be deleted." );
        }
    }


    public void OpenDeleteMenu()
    {
        if(inputField.text.Length > 0)
        {
            deleteMenuUi.SetActive(true);
            deleteMenuText.text = "Are you sure you want to delete '" + inputField.text + "'?";
            PauseMenu.pausedOnLayer = 3;
        }
        else
        {
            gridManager.SetMessage("Select a city to delete.");
        }
    }

    public void CloseDeleteMenu()
    {
        deleteMenuUi.SetActive(false);
        PauseMenu.pausedOnLayer = 2;
    }


}
