using UnityEngine;

public class TunnelUIManager : MonoBehaviour
{
    public TunnelElement targetTunnel;   // Dit element med vent/light arrays
    public GameObject buttonPrefab;       // prefab med UI_ItemStatus
    public Transform ventRow;             // reference til VentRow i panel
    public Transform lightRow;            // reference til LightRow i panel

    private void Start()
    {
        if (targetTunnel == null) return;

        // Fyld Vent-række
        foreach (var item in targetTunnel.vent)
        {
            CreateButtonForItem(item, "Vent", ventRow);
        }

        // Fyld Light-række
        foreach (var item in targetTunnel.light)
        {
            CreateButtonForItem(item, "Light", lightRow);
        }
    }

    private void CreateButtonForItem(ItemStatus item, string type, Transform parent)
    {
        if (item == null) return;

        GameObject btnObj = Instantiate(buttonPrefab, parent);
        btnObj.name = $"{type}_{item.name}";

        UI_ItemStatus uiButton = btnObj.GetComponent<UI_ItemStatus>();
        if (uiButton != null)
        {
            uiButton.targetItem = item;
        }
    }
}
