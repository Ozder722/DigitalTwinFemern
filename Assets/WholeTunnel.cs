using System.Collections.Generic;
using UnityEngine;

public class WholeTunnel : Tunnel
{
    public List<GameObject> alleBiler = new List<GameObject>();

    public Tunnel tunnel;
    public Collider DK_Exit;
    public Collider DE_Exit;

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

    private void OnTriggerEnter(Collider other)
    {
        // Tjek om det er en bil
        if (!alleBiler.Contains(other.gameObject)) return;

        // Tjek hvilken exit bilen ramte
        if (other == DK_Exit)
        {
            Debug.Log($"{other.name} forlod tunnelen mod Danmark");
            alleBiler.Remove(other.gameObject);
        }
        else if (other == DE_Exit)
        {
            Debug.Log($"{other.name} forlod tunnelen mod Tyskland");
            alleBiler.Remove(other.gameObject);
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
