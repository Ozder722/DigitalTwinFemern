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

    private static bool IsIgnoredColor(ItemStatus.errorColor color)
    {
        return color == ItemStatus.errorColor.green || color == ItemStatus.errorColor.blue;
    }



    public static void ScrollToWorstError()
    {
        if (instance == null || instance.scrollRect == null) return;

        var buttons = instance.scrollRect.content.GetComponentsInChildren<UI_TunnelButton>(true);
        if (buttons == null || buttons.Length == 0) return;

        UI_TunnelButton worstButton = null;
        int worstPriority = -999;

        foreach (var b in buttons)
        {
            // Antag at dine GetVentColor()/GetLightColor() returnerer ItemStatus.errorColor
            var ventColor = b.GetVentColor();
            var lightColor = b.GetLightColor();

            // Spring over, hvis begge farver er "ufarlige"
            if (IsIgnoredColor(ventColor) && IsIgnoredColor(lightColor))
                continue;

            int ventP = ErrorPriority.GetPriority(ventColor);
            int lightP = ErrorPriority.GetPriority(lightColor);
            int localWorst = Mathf.Max(ventP, lightP);

            if (localWorst > worstPriority)
            {
                worstPriority = localWorst;
                worstButton = b;
            }
        }

        if (worstButton == null || worstPriority <= 0)
            return;

        instance.ScrollToTarget(worstButton);
    }



    private void ScrollToTarget(UI_TunnelButton targetButton)
    {
        if (scrollRect == null || targetButton == null) return;

        RectTransform content = scrollRect.content;
        RectTransform targetRect = targetButton.GetComponent<RectTransform>();
        RectTransform viewport = scrollRect.viewport;

        // Vent ét frame, så layoutet er opdateret
        StartCoroutine(ScrollNextFrame(targetRect));
    }

    private System.Collections.IEnumerator ScrollNextFrame(RectTransform targetRect)
    {
        yield return null; // Vent ét frame, så layout er opdateret

        RectTransform content = scrollRect.content;
        RectTransform viewport = scrollRect.viewport;

        float contentHeight = content.rect.height;
        float viewportHeight = viewport.rect.height;

        // Find midtpunkt for target i world-space
        Vector3[] targetCorners = new Vector3[4];
        targetRect.GetWorldCorners(targetCorners);
        float targetMiddleY = (targetCorners[0].y + targetCorners[1].y + targetCorners[2].y + targetCorners[3].y) / 4f;

        // Konverter til content-space
        Vector3 targetLocalPos = content.InverseTransformPoint(new Vector3(0, targetMiddleY, 0));

        float contentTop = content.rect.height * (1 - content.pivot.y);
        float distanceFromTop = contentTop - targetLocalPos.y;

        // Beregn normaliseret scrollværdi
        float normalized = Mathf.Clamp01(1f - (distanceFromTop / (contentHeight - viewportHeight)));

        // Tilføj offset for at vise hele panelet
        float offset = (targetRect.rect.height * 1.5f) / (contentHeight - viewportHeight);
        normalized = Mathf.Clamp01(normalized + offset);

        StopAllCoroutines();
        StartCoroutine(SmoothScrollTo(normalized));
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
