using NUnit.Framework;
using UnityEngine;

public class Car_ListEnter : MonoBehaviour
{
    public Tunnel tunnel;

    private void OnTriggerEnter(Collider other)
    {
        tunnel.bilerTyskland.Add(other.gameObject);

    }
}
