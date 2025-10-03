using UnityEngine;

public class Car_ListRemoveDanmark : MonoBehaviour
{
    public Tunnel tunnel;
    private void OnTriggerEnter(Collider other)
    {

        tunnel.bilerDanmark.Remove(other.gameObject);
    }
}
