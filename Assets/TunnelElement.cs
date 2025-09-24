using System.Collections.Generic;
using UnityEngine;

public class TunnelElement1 : MonoBehaviour
{
    //liste af biler
    [SerializeField] private List<GameObject> biler = new List<GameObject>();

    //array af ventilation
    [SerializeField] private GameObject[] vent;

    //lys
    [SerializeField] private GameObject[] light;
}
