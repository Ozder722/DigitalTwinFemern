using System.Collections.Generic;
using UnityEngine;

public abstract class Tunnel : MonoBehaviour
{
    //liste af biler
    public List<GameObject> bilerTyskland = new List<GameObject>();
    public List<GameObject> bilerDanmark = new List<GameObject>();

    

    //liste af fejl
    [SerializeField] List<ItemStatus> ventErrors = new List<ItemStatus>();
    [SerializeField] List<ItemStatus> lightErrors = new List<ItemStatus>();

    //array af ventilation
    public ItemStatus[] vent;

    //lys
    public ItemStatus[] light;

    


}
