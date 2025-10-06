using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListManagerToTMPTEXT : MonoBehaviour
{
    public enum ListType { Alle, Danmark, Tyskland }

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private ListType listType;
    private WholeTunnel wholeTunnel;

    private void Start()
    {
        // Finder WholeTunnel i scenen
        wholeTunnel = FindFirstObjectByType<WholeTunnel>();
    }

    private void Update()
    {
        if (text == null || wholeTunnel == null) return;

        switch (listType)
        {
            case ListType.Alle:
                text.text = wholeTunnel.alleBiler.Count.ToString();
                break;
            case ListType.Danmark:
                text.text = wholeTunnel.bilerDanmark.Count.ToString();
                break;
            case ListType.Tyskland:
                text.text = wholeTunnel.bilerTyskland.Count.ToString();
                break;
        }
    }
}
