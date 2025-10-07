using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ErrorScrollManager : MonoBehaviour
{
    public ScrollRect scrollRect; // Reference til din ScrollRect
    private static ErrorScrollManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static void ScrollToWorstError()
    {
        if (instance == null || instance.scrollRect == null) return;

        // Find alle UI_TunnelButton-komponenter i ScrollRectens content
        var buttons = instance.scrollRect.content.GetComponentsInChildren<UI_TunnelButton>(true);
        if (buttons == null || buttons.Length == 0) return;

        // Find værste fejl vha. ErrorPriority
        UI_TunnelButton worstButton = null;
        int worstPriority = -999;

        foreach (var b in buttons)
        {
            // Tag værste farve fra vent og light
            int ventP = ErrorPriority.GetPriority(b.GetVentColor());
            int lightP = ErrorPriority.GetPriority(b.GetLightColor());
            int localWorst = Mathf.Max(ventP, lightP);

            if (localWorst > worstPriority)
            {
                worstPriority = localWorst;
                worstButton = b;
            }
        }

        // Ingen fejl fundet (alt grønt)
        if (worstPriority <= 0 || worstButton == null)
            return;

        // Scroll til den værste fejl
        instance.ScrollToTarget(worstButton);
    }

    private void ScrollToTarget(UI_TunnelButton targetButton)
    {
        if (scrollRect == null || targetButton == null) return;

        RectTransform content = scrollRect.content;
        RectTransform targetRect = targetButton.GetComponent<RectTransform>();

        Canvas.ForceUpdateCanvases();

        float contentHeight = content.rect.height;
        float viewportHeight = scrollRect.viewport.rect.height;
        float targetPosY = Mathf.Abs(targetRect.anchoredPosition.y);

        float normalized = targetPosY / (contentHeight - viewportHeight);
        normalized = Mathf.Clamp01(normalized);

        // Smooth scroll (valgfrit)
        StopAllCoroutines();
        StartCoroutine(SmoothScrollTo(1f - normalized));
    }

    private System.Collections.IEnumerator SmoothScrollTo(float targetPos)
    {
        float duration = 0.5f;
        float start = scrollRect.verticalNormalizedPosition;
        float time = 0f;

        while (time < duration)
        {
            scrollRect.verticalNormalizedPosition = Mathf.Lerp(start, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        scrollRect.verticalNormalizedPosition = targetPos;
    }
}
