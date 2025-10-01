using UnityEngine;

public class OpenDetailPanel : MonoBehaviour
{
    public GameObject[] DetailPanelPrefab;
    public GameObject Close;

    private static GameObject currentlyOpenPanel;

    public void PanelActive(int index)
    {
        Close.SetActive(true);

        if (currentlyOpenPanel != null && currentlyOpenPanel != DetailPanelPrefab[index])
        {
            currentlyOpenPanel.SetActive(false);
        }

        DetailPanelPrefab[index].SetActive(true);
        currentlyOpenPanel = DetailPanelPrefab[index];
    }

    public void ClosePanel()
    {
        foreach (GameObject panel in DetailPanelPrefab)
        {
            panel.SetActive(false);
        }

        currentlyOpenPanel = null;
        Close.SetActive(false);
    }
}
