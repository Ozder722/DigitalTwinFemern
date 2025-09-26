using System.Collections.Generic;
using UnityEngine;

public abstract class Tunnel : MonoBehaviour
{
    //liste af biler
    [SerializeField] protected List<GameObject> biler = new List<GameObject>();
    //liste af fejl
    [SerializeField] protected List<ItemStatus> ventErrors = new List<ItemStatus>();
    [SerializeField] protected List<ItemStatus> lightErrors = new List<ItemStatus>();

    //array af ventilation
    [SerializeField] protected ItemStatus[] vent;

    //lys
    [SerializeField] protected ItemStatus[] light;


}
