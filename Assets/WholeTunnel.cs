using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class WholeTunnel : Tunnel
{
    public List<GameObject> alleBiler = new List<GameObject>();

    public Collider DK_Exit;
    public Collider DE_Exit;

    private CarMovement carMovement;
    private int bilstats;
    private GameObject randomBil;

    public bool trafficJam = false;
    public Button trafficButton; 


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

        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestartStoppedCars();
        }

    }




    //public void TriggerTrafficJam()
    //{
    //    if (alleBiler.Count == 0)
    //    {
    //        Debug.LogWarning("Ingen biler i listen!");
    //        return;
    //    }

    //    int ramdomindex = Random.Range(0, alleBiler.Count);
    //    randomBil = alleBiler[ramdomindex];



    //    carMovement = randomBil.GetComponent<CarMovement>();
    //    carMovement.speed = 0;
    //    if (bilerDanmark.Contains(randomBil))
    //    {
    //        Debug.Log("Danske biler stop");
    //    }
    //    else if (bilerTyskland.Contains(randomBil))
    //    {
    //        Debug.Log("Tyske biler stop");
    //    }


    //}

    public void TriggerTrafficJam()
    {
        if (alleBiler.Count == 0)
        {
            Debug.LogWarning("Ingen biler i listen!");
            return;
        }

        int randomIndex = Random.Range(0, alleBiler.Count);
        randomBil = alleBiler[randomIndex];

        carMovement = randomBil.GetComponent<CarMovement>();
        carMovement.speed = 0;

        // Skift knapfarve til orange
        if (trafficButton != null)
            trafficButton.image.color = Color.Lerp(Color.yellow, new Color(1f, 0.5f, 0f), 0.5f);

        if (bilerDanmark.Contains(randomBil))
        {
            Debug.Log("Danske biler stop");
        }
        else if (bilerTyskland.Contains(randomBil))
        {
            Debug.Log("Tyske biler stop");
        }


    }

    public void OnCarCrash()
    {
        // Skift knapfarve til rød
        if (trafficButton != null)
            trafficButton.image.color = Color.red;

        Debug.Log("Der skete et sammenstød!");

        // Start Coroutine der stopper bilerne i 5 sekunder
        StartCoroutine(StopCarsAfterDelay(randomBil));
    }

    private IEnumerator StopCarsAfterDelay(GameObject stoppedCar)
    {
        if (stoppedCar == null) yield break;

        // Vent 5 sekunder
        yield return new WaitForSeconds(5f);

        // Bestem hvilken liste bilen tilhører
        List<GameObject> bilerAtStoppe = null;

        if (bilerDanmark.Contains(stoppedCar))
        {
            bilerAtStoppe = bilerDanmark;
        }
        else if (bilerTyskland.Contains(stoppedCar))
        {
            bilerAtStoppe = bilerTyskland;
        }

        // Stop alle biler i den valgte liste
        if (bilerAtStoppe != null)
        {
            foreach (GameObject bil in bilerAtStoppe)
            {
                if (bil == null) continue;

                CarMovement cm = bil.GetComponent<CarMovement>();
                if (cm != null)
                {
                    cm.speed = 0; // Stands bilen permanent
                }
            }
        }

        Debug.Log("Alle biler i samme land som den stoppede bil er nu stoppet.");
    }

    public void RestartStoppedCars()
    {
        // Stop alle aktive coroutines, så StopCarsAfterDelay ikke aktiveres bagefter
        StopAllCoroutines();

        // Genstart alle biler i Danmark
        foreach (GameObject bil in bilerDanmark)
        {
            if (bil == null) continue;
            CarMovement cm = bil.GetComponent<CarMovement>();
            if (cm != null && cm.speed == 0)
            {
                cm.speed = cm.dSpeed;
            }
        }

        // Genstart alle biler i Tyskland
        foreach (GameObject bil in bilerTyskland)
        {
            if (bil == null) continue;
            CarMovement cm = bil.GetComponent<CarMovement>();
            if (cm != null && cm.speed == 0)
            {
                cm.speed = cm.dSpeed;
            }
        }

        // Skift knapfarve tilbage til grøn
        if (trafficButton != null)
            trafficButton.image.color = Color.green;

        Debug.Log("Alle stoppede biler er nu genstartet.");
    }




}
