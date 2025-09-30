using UnityEngine;
using UnityEngine.UI;

public class TunnelUIManager : MonoBehaviour
{
    public TunnelElement targetTunnel; // Tunnel-element vi viser UI for
    public GameObject buttonPrefab;    // prefab med Button + ItemStatusUIButton script
    public Transform buttonParent;     // hvor knapperne skal placeres i Canvas

    private void Start()
    {
        if (targetTunnel == null || buttonPrefab == null || buttonParent == null) return;

        // Generer knapper for vent-array
        foreach (var item in targetTunnel.vent)
        {
            CreateButtonForItem(item, "Vent");
        }

        // Generer knapper for light-array
        foreach (var item in targetTunnel.light)
        {
            CreateButtonForItem(item, "Light");
        }
    }

    private void CreateButtonForItem(ItemStatus item, string type)
    {
        if (item == null) return;

        // Instantiate prefab
        GameObject btnObj = Instantiate(buttonPrefab, buttonParent);

        // Navngiv knappen så man kan se hvilket objekt det repræsenterer
        btnObj.name = $"{type}_{item.name}";

        // Sæt targetItem på scriptet
        UI_ItemStatus uiButton = btnObj.GetComponent<UI_ItemStatus>();
        if (uiButton != null)
        {
            uiButton.targetItem = item;
        }
    }
}
