using UnityEngine;

public class Car_ListADDDanmark : MonoBehaviour
{
    public Tunnel tunnel;

    private void OnTriggerEnter(Collider other)
    {
        tunnel.bilerDanmark.Add(other.gameObject);

    }
}
