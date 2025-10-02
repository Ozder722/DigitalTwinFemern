using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UI_MasterButton : MonoBehaviour
{
    public Button masterButton;
    public ItemStatus.ItemType type; // Vent eller Light
    public TunnelElement[] tunnels;  // Alle tunneler i systemet

    private void Update()
    {
        UpdateMasterColor();
    }

    private void UpdateMasterColor()
    {
        if (masterButton == null || tunnels == null || tunnels.Length == 0) return;

        ItemStatus.errorColor worst = ItemStatus.errorColor.green;
        int worstPriority = -1;

        foreach (var t in tunnels)
        {
            if (t == null) continue;

            // Find værste farve for denne type
            ItemStatus[] items = (type == ItemStatus.ItemType.Vent) ? t.vent : t.light;
            if (items == null) continue;

            foreach (var item in items)
            {
                if (item == null) continue;
                int p = ErrorPriority.GetPriority(item.LastKnownState);
                if (p > worstPriority)
                {
                    worstPriority = p;
                    worst = item.LastKnownState;
                }
            }
        }

        masterButton.image.color = GetColorFromError(worst);
    }

    private Color GetColorFromError(ItemStatus.errorColor state)
    {
        switch (state)
        {
            case ItemStatus.errorColor.green: return Color.green;
            case ItemStatus.errorColor.yellow: return Color.yellow;
            case ItemStatus.errorColor.red: return Color.red;
            case ItemStatus.errorColor.blue: return Color.blue;
            case ItemStatus.errorColor.grey: return Color.grey;
            default: return Color.green;
        }
    }
}
