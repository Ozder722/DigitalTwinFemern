using System.Collections.Generic;
using UnityEngine;

public class TunnelEventSystem : MonoBehaviour
{
    [SerializeField] List<TunnelElement> elementer = new List<TunnelElement>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TriggerRandomEvent();
        }
       
    }

    private void TriggerRandomEvent()
    {
        if (elementer == null || elementer.Count == 0) return;

        //tilfældig tunnel
        Tunnel chosenTunnel = elementer[Random.Range(0, elementer.Count)];
        if (chosenTunnel == null) return;

        //Vælg om vi tager vent eller light
        bool pickVent = (Random.Range(0, 2) == 0);
        ItemStatus[] arrayToPick = pickVent ? chosenTunnel.vent : chosenTunnel.light;
        if (arrayToPick == null || arrayToPick.Length == 0) return;

        //Vælg tilfældigt objekt i arrayet
        ItemStatus chosenItem = arrayToPick[Random.Range(0, arrayToPick.Length)];
        if (chosenItem == null) return;

        //tilfældig fejl (gul, rød eller blå)
        ItemStatus.errorColor randomError = GetRandomErrorColor();

        // 5. Trigger state på objektet
        chosenItem.SimulateState(randomError);

        Debug.Log($"Tunnel event: {chosenTunnel.name} - {chosenItem.name} -> {randomError}");
    }

    private ItemStatus.errorColor GetRandomErrorColor()
    {
        int r = Random.Range(0, 3); // 0,1,2
        switch (r)
        {
            case 0: return ItemStatus.errorColor.yellow;
            case 1: return ItemStatus.errorColor.red;
            case 2: return ItemStatus.errorColor.grey;
            default: return ItemStatus.errorColor.red;
        }
    }
}
