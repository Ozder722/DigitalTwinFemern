using System;
using System.Collections;
using UnityEngine;
using static ItemEvent;

public abstract class ItemStatus : MonoBehaviour
{
    short id;
    float runtime;
    float downtime;
    public enum errorColor { green, yellow, red, blue, grey};

    //event
    public event EventHandler<OnKeyPressedEventArgs> OnKeyPressed;
    public class OnKeyPressedEventArgs : EventArgs
    {
        public enum eColor { yellow, red, grey };
    }
    public event Action<errorColor> OnActionEvent;
   /*  private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int a = UnityEngine.Random.Range(0, 3);
            errorColor e;
            switch (a)
            {
                case 0:
                    e = errorColor.yellow; break;
                case 1:
                    e = errorColor.red; break;
                case 2:
                    e = errorColor.grey; break;
                default:
                    e = errorColor.red;
                    break;
            }
            SimulateState(e);

        }
        
    }*/


    //er der en fejl eller ej true/false
    bool error=false;
    Color currentColor;
   

    public void UnderRepair()
    {
        SimulateState(errorColor.blue);
        StartCoroutine(ChangeStateAfterSeconds(UnityEngine.Random.Range(3f,5f)));
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
        OnActionEvent?.Invoke(e);
        //updater UI med current color

    }

}
