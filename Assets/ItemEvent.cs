using System;
using UnityEngine;

public class ItemEvent : MonoBehaviour
{
    private int spaceCount;

    public event EventHandler<OnKeyPressedEventArgs> OnKeyPressed;

    public class OnKeyPressedEventArgs : EventArgs
    {
        public int spaceCount; 
    }
    
      
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceCount++;
            OnKeyPressed?.Invoke(this, new OnKeyPressedEventArgs { spaceCount = spaceCount });
        }

    }
}

