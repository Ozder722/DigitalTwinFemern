using System;
using UnityEngine;

public class ItemEvent : MonoBehaviour
{
    public event EventHandler OnKeyPressed;

    private void Start()
    {
        OnKeyPressed += Test_OnSpacePressed;
    }
    private void Test_OnSpacePressed(object sender, EventArgs e)
    {
        Debug.Log("Space!");
    }    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnKeyPressed?.Invoke(this, EventArgs.Empty);
        }

    }
}

