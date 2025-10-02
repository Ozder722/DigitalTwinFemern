using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UI_TunnelButton : MonoBehaviour
{
    public TunnelElement targetTunnel;
    public Button button;

    private ItemStatus.errorColor currentVentColor = ItemStatus.errorColor.green;
    private ItemStatus.errorColor currentLightColor = ItemStatus.errorColor.green;

    private void Start()
    {
        if (button == null) button = GetComponent<Button>();
        if (targetTunnel == null) return;

        foreach (var item in targetTunnel.vent.Concat(targetTunnel.light))
        {
            if (item != null)
                item.OnActionEvent += HandleItemStatusChanged;
        }

        UpdateButtonColor();
    }

    private void HandleItemStatusChanged(ItemStatus.errorColor state)
    {
        UpdateButtonColor();
    }

    private void UpdateButtonColor()
    {
        if (button == null || targetTunnel == null) return;

        // Vent
        ItemStatus.errorColor worstVent = ItemStatus.errorColor.green;
        int worstVentPriority = -1;
        foreach (var item in targetTunnel.vent)
        {
            if (item == null) continue;
            int p = ErrorPriority.GetPriority(item.LastKnownState);
            if (p > worstVentPriority)
            {
                worstVentPriority = p;
                worstVent = item.LastKnownState;
            }
        }
        currentVentColor = worstVent;

        // Light
        ItemStatus.errorColor worstLight = ItemStatus.errorColor.green;
        int worstLightPriority = -1;
        foreach (var item in targetTunnel.light)
        {
            if (item == null) continue;
            int p = ErrorPriority.GetPriority(item.LastKnownState);
            if (p > worstLightPriority)
            {
                worstLightPriority = p;
                worstLight = item.LastKnownState;
            }
        }
        currentLightColor = worstLight;

        // Knapfarve kan fx følge værste af begge (eller vælge anden logik)
        ItemStatus.errorColor worstOverall = worstVentPriority > worstLightPriority ? worstVent : worstLight;
        button.image.color = GetColorFromError(worstOverall);
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

    // Master-knapper kan bruge disse metoder
    public ItemStatus.errorColor GetVentColor() => currentVentColor;
    public ItemStatus.errorColor GetLightColor() => currentLightColor;
}
