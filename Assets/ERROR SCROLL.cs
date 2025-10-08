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
            var ventColor = b.GetVentColor();
            var lightColor = b.GetLightColor();

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
        {
            Debug.Log("Ingen fejl fundet - scroller ikke");
            return;
        }

        instance.ScrollToTarget(worstButton);
    }


    private void ScrollToTarget(UI_TunnelButton targetButton)
    {
        if (scrollRect == null || targetButton == null) return;

        RectTransform targetRect = targetButton.GetComponent<RectTransform>();

        // Stop tidligere scrolling og start ny coroutine
        StopAllCoroutines();
        StartCoroutine(ScrollNextFrame(targetRect));
    }

    private System.Collections.IEnumerator ScrollNextFrame(RectTransform targetRect)
    {
        // Vent et par frames så layout + CanvasRebuild er færdige
        yield return null;
        yield return null;

        RectTransform content = scrollRect.content;
        RectTransform viewport = scrollRect.viewport;

        float contentHeight = content.rect.height;
        float viewportHeight = viewport.rect.height;

        // Hvis der ikke er noget at scrolle (content mindre end viewport)
        if (contentHeight <= viewportHeight)
            yield break;

        // Bounds for target i content-local space
        Bounds targetBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(content, targetRect);
        float targetTop = targetBounds.max.y;
        float targetBottom = targetBounds.min.y;
        float targetCenter = targetBounds.center.y;

        // Viewport corners konverteret til content-local space
        Vector3[] vpWorldCorners = new Vector3[4];
        viewport.GetWorldCorners(vpWorldCorners);
        for (int i = 0; i < 4; i++)
            vpWorldCorners[i] = content.InverseTransformPoint(vpWorldCorners[i]);

        float vpTop = vpWorldCorners[1].y;
        float vpBottom = vpWorldCorners[0].y;

        // Hvis target allerede (delvist) er synligt -> scroll ikke
        if (targetTop <= vpTop && targetBottom >= vpBottom)
        {
            Debug.Log("Target allerede synligt - scroller ikke");
            yield break;
        }

        // Beregn distance fra content-top til target-center (content-top tager pivot i betragtning)
        float contentTop = content.rect.yMax; // content top i content-local coordinates
        float distFromTopToTargetCenter = contentTop - targetCenter;

        // Flyt så target bliver centreret i viewport (kan justeres hvis du vil have den højere/lavere)
        float centeredOffset = distFromTopToTargetCenter - (viewportHeight / 2f);

        // Konverter til normaliseret scroll (1 = top, 0 = bund)
        float normalized = 1f - (centeredOffset / (contentHeight - viewportHeight));
        normalized = Mathf.Clamp01(normalized);

        Debug.Log($"ScrollTo target: center={targetCenter:F1}, contentTop={contentTop:F1}, normalized={normalized:F3}");

        // Start smooth scroll (din eksisterende coroutine)
        StartCoroutine(SmoothScrollTo(normalized));
    }

    private bool IsRectVisibleInViewport(RectTransform target)
    {
        // (valgfri helper, bruger samme teknik som ovenfor)
        Bounds b = RectTransformUtility.CalculateRelativeRectTransformBounds(scrollRect.content, target);

        Vector3[] vpWorldCorners = new Vector3[4];
        scrollRect.viewport.GetWorldCorners(vpWorldCorners);
        for (int i = 0; i < 4; i++)
            vpWorldCorners[i] = scrollRect.content.InverseTransformPoint(vpWorldCorners[i]);

        float vpTop = vpWorldCorners[1].y;
        float vpBottom = vpWorldCorners[0].y;

        return (b.max.y <= vpTop && b.min.y >= vpBottom);
    }

    private System.Collections.IEnumerator SmoothScrollTo(float targetPos)
    {
        float duration = 0.4f; // hvor hurtigt scrollen bevæger sig (sekunder)
        float start = scrollRect.verticalNormalizedPosition;
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            // Lav smooth "ease out" effekt
            t = 1f - Mathf.Pow(1f - t, 3f);
            scrollRect.verticalNormalizedPosition = Mathf.Lerp(start, targetPos, t);
            time += Time.deltaTime;
            yield return null;
        }

        scrollRect.verticalNormalizedPosition = targetPos;
    }

}
