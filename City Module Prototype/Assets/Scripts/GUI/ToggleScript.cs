using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    private bool toggle; 

    public void ShowNetworkDirection()
    {
        toggle = !toggle;
        Debug.Log(toggle); 
    }
}
