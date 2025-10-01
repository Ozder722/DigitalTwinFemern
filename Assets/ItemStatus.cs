using System;
using System.Collections;
using UnityEngine;
using static ItemEvent;

public abstract class ItemStatus : MonoBehaviour
{
    

    public enum ItemType
    {
        Vent,
        Light
    }
    public ItemType type;
    public enum errorColor { green, yellow, red, blue, grey};

    protected void Start()
    {
        //StartCoroutine(ChangeStateAfterSeconds(1));
    }

    private void Update()
    {
        if (error && Input.GetKeyDown(KeyCode.R))
        {
            UnderRepair();
        }
    }
    //event
    public event EventHandler<OnKeyPressedEventArgs> OnKeyPressed;
    public class OnKeyPressedEventArgs : EventArgs
    {
        public enum eColor { yellow, red, grey };
    }
    public event Action<errorColor> OnActionEvent;
   
    //er der en fejl eller ej true/false
    protected bool error=false;
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
            case errorColor.green: currentColor = Color.green; error = false; break;
            case errorColor.yellow: currentColor = Color.yellow; error = true; break;
            case errorColor.red: currentColor = Color.red; error = true; break;
            case errorColor.blue: currentColor = Color.blue; break;
            case errorColor.grey: currentColor = Color.grey; error = true; break;
        }

        OnActionEvent?.Invoke(e);

    }

}
