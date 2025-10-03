using System.Collections.Generic;
using UnityEngine;

public class WholeTunnel : Tunnel
{
    public List<GameObject> alleBiler = new List<GameObject>();

    public Tunnel tunnel;


    private CarMovement carMovement;
    private int bilstats;
    private GameObject randomBil;

    public bool trafficJam = false;

    private void Update()
    {
        for (int i = 0; i < bilerDanmark.Count; i++)
        {
            if (alleBiler.Contains(bilerDanmark[i]))
            {
                continue;

            }
            else
            {
                alleBiler.Add(bilerDanmark[i]);
            }
            
        }

        for (int i = 0; i < bilerTyskland.Count; i++)
        {
            if (alleBiler.Contains(bilerTyskland[i]))
            {
                continue;

            }
            else
            {
                alleBiler.Add(bilerTyskland[i]);
            }

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TriggerTrafficJam();
            trafficJam = true;
        }
        

    }

    public void TriggerTrafficJam()
    {
        if (alleBiler.Count == 0)
        {
            Debug.LogWarning("Ingen biler i listen!");
            return;
        }

        int ramdomindex = Random.Range(0, alleBiler.Count);
        randomBil = alleBiler[ramdomindex];



        carMovement = randomBil.GetComponent<CarMovement>();
        carMovement.speed = 0;
        if (bilerDanmark.Contains(randomBil))
        {
            Debug.Log("Danske biler stop");
        }
        else if (bilerTyskland.Contains(randomBil))
        {
            Debug.Log("Tyske biler stop");
        }


    }



}
