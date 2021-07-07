using UnityEngine;

public class CameraActivator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[1].Activate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}