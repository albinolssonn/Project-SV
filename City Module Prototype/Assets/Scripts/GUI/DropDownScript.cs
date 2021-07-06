using UnityEngine;

public class DropDownScript : MonoBehaviour
{
    public int simMode = 0; 

    public void ChooseSimMode(int value)
    {
        if(value == 0)
        {
            simMode = value; 
            Debug.Log(simMode + " = Signal Strength"); 
        }

        if (value == 1)
        {
            simMode = value;
            Debug.Log(simMode + " = Capacity"); 
        }
    }


}
