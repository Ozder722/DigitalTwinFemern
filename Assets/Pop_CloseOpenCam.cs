using UnityEngine;

public class Pop_CloseOpenCam : MonoBehaviour
{
   


    public GameObject panelA;
    public GameObject panelB;

    private bool showingA = true;

    public void TogglePanels()
    {
        // Skift bool-værdien
        showingA = !showingA;

        // Slå paneler til/fra baseret på ny værdi
        panelA.SetActive(showingA);
        panelB.SetActive(!showingA);
    }
}


