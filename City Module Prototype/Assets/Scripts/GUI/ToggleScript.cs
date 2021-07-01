using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    private GridManager gridManager;

    public void Start()
    {
        gridManager = GameObject.FindGameObjectsWithTag("Grid")[0].GetComponent<GridManager>();
    }

    public void ShowNetworkDirection()
    {
        gridManager.ToggleCreateNetworkArrows();
    }
}
