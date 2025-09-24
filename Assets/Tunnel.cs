using System.Collections.Generic;
using UnityEngine;

public abstract class Tunnel : MonoBehaviour
{
    //liste af biler
    [SerializeField] private List<GameObject> biler = new List<GameObject>();

    //array af ventilation
    [SerializeField] private GameObject[] vent;

    //lys
    [SerializeField] private GameObject[] light;
}
