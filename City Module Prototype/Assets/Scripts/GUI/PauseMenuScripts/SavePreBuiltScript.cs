using System.IO;
using TMPro;
using UnityEngine;

public class SavePreBuiltScript : MonoBehaviour
{
    private GridManager gridManager;
    public TMP_Dropdown dropdown;
    public TMP_InputField inputField; 


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

        string[] configFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/ConfigFiles/");


        foreach (string file in configFiles)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = Path.GetFileNameWithoutExtension(file) });
        }

        dropdown.onValueChanged.AddListener(delegate { DropDownItemSelected(); }); 
    }

    /// <summary>
    /// Takes value from dropdown menu and inserts to inputfield. 
    /// </summary>
    private void DropDownItemSelected()
    {
        inputField.text = dropdown.options[dropdown.value].text; 
    }

    /// <summary>
    /// Takes input string from inputfield and passes into gridmanager.
    /// </summary>
    public void SaveBtn()
    {
        if(inputField.text.Length == 0)
        {
            gridManager.SetErrorMessage("Write filename or chose an existing file.");
            return; 
        }

        gridManager.SavePreconfigCity(inputField.text);
        gridManager.SetErrorMessage("City saved.");

    }
}
