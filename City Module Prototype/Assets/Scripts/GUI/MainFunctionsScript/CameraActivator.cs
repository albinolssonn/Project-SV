using UnityEngine;

public class CameraActivator : MonoBehaviour
{
    /// <summary>
    /// Activates the different displays used in the program.
    /// </summary>
    public void Start()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[1].Activate();
        }
    }
}