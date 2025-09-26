using System.Collections;
using UnityEngine;

public abstract class ItemStatus : MonoBehaviour
{
    short id;
    float runtime;
    float downtime;
    public enum errorColor { green, yellow, red, blue, grey};

    //er der en fejl eller ej true/false
    bool error=false;
    Color currentColor;

    public void UnderRepair()
    {
        SimulateState(errorColor.blue);
        StartCoroutine(ChangeStateAfterSeconds(Random.Range(3f,5f)));
    }

    IEnumerator ChangeStateAfterSeconds(float s)
    {
        yield return new WaitForSeconds(s);
        SimulateState(errorColor.green);
    }

    public void SimulateState(errorColor e)
    {
        
        switch (e)
        {
            case errorColor.green:
                currentColor = Color.green;
                break;
            case errorColor.yellow:
                currentColor = Color.yellow; 

                break;
            case errorColor.red:
                currentColor = Color.red; 
                break;
            case errorColor.blue:
                currentColor = Color.blue;
                break;
            case errorColor.grey: 
                currentColor = Color.grey;
                break;
             
        }
        
        //updater UI med current color
         
    }

}
