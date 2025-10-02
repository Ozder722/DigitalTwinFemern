using UnityEngine;

public static class ErrorPriority
{
    public static int GetPriority(ItemStatus.errorColor color)
    {
        switch (color)
        {
            case ItemStatus.errorColor.red: return 3;
            case ItemStatus.errorColor.yellow: return 2;
            case ItemStatus.errorColor.grey: return 1;
            case ItemStatus.errorColor.blue: return 0; // reparation
            case ItemStatus.errorColor.green: return -1; // alt ok
            default: return -1;
        }
    }
}

