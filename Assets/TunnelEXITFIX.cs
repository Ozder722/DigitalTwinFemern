using UnityEngine;

public class TunnelEXITFIX : MonoBehaviour
{
    public WholeTunnel tunnel;  // Reference til WholeTunnel-scriptet
    public string direction;    // "Danmark" eller "Tyskland"

    private void OnTriggerEnter(Collider other)
    {
        if (tunnel.alleBiler.Contains(other.gameObject))
        {
            tunnel.alleBiler.Remove(other.gameObject);
            Debug.Log($"{other.name} forlod tunnelen mod {direction}");
        }
    }
}
