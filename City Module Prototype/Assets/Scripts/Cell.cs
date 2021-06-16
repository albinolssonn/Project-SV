using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private int signal;
    private ArrayList cellContent = new ArrayList();
    
    
    public int getSignal()
    {
        return signal; 
    }

    public void setSignal(int signal)
    {
        this.signal = signal; 
    }

    public ArrayList getCellContent()
    {
        return cellContent; 
    }

    public void addCellContent (int content)
    {
        cellContent.Add(content); 
    }

    // Remove Method




    // Start is called before the first frame update
    void Start()
    {
        
    }

}
