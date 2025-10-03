using UnityEngine;

public class Car_List_Remove : MonoBehaviour
{
    public Tunnel tunnel;
    private void OnTriggerEnter(Collider other)
    {
        tunnel.biler.Remove(other.gameObject);
    }
}
