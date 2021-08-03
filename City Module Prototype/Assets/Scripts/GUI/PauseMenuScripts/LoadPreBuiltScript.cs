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
        dropdown.RefreshShownValue();
    }

    /// <summary>
    /// Loads a pre-configured city.
    /// </summary>
    public void LoadBtn()
    {
        if(dropdown.options.Count > 0)
        {
            gridManager.LoadPreconfigCity(dropdown.options[dropdown.value].text);
        }

        else
        {
            gridManager.SetErrorMessage("There are no save files."); 
        }
    }
}
