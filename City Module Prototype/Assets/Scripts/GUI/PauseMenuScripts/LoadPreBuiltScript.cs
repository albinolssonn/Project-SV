using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// HACK: Create pre-configured city button.
// Here you can add button methods to create additional pre-configured cities.
// First create a method in the class 'PreConfCities' as instructed in the method 'LoadPreconfigCity'
// and then call on 'LoadPreconfigCity' with the correct index as you define in the switch case.
// Then attach PrebuiltCities_Panel to your button in Unity and call the method you created here on click and it's done.

public class LoadPreBuiltScript : MonoBehaviour
{

    private GridManager grid;
    private TMP_Dropdown dropdown; 



    public void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
        dropdown = transform.GetComponent<TMP_Dropdown>();
        
        //DropdownItemSelected(); 


        //OnPointerClick();
        //dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); }); 

        

    }

    /* void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;  
    } */

    public void LoadDropdown()  
    {
        dropdown.options.Clear();

        string[] configFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/ConfigFiles/"); 


        foreach(string file in configFiles)
        {
            
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = Path.GetFileNameWithoutExtension(file) });


        }
        dropdown.RefreshShownValue();   
    }

    /// <summary>
    /// Loads a pre-configured city.
    /// </summary>
    public void PreCofigBtn1()
    {
        grid.LoadPreconfigCity("Config1");

    }


    /// <summary>
    /// Loads a pre-configured city.
    /// </summary>
    public void PreCofigBtn2()
    {
        grid.LoadPreconfigCity("Config2");

    }


    /// <summary>
    /// Loads a pre-configured city.
    /// </summary>
    public void PreCofigBtn3()
    {
        grid.LoadPreconfigCity("Config3");

    }


    /// <summary>
    /// Loads a pre-configured city.
    /// </summary>
    public void PreCofigBtn4()
    {
        grid.LoadPreconfigCity("Config4");

    }


}
