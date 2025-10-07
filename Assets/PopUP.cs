using UnityEngine;

public class PopUp : MonoBehaviour
{
    public RectTransform popupRect;
    public Vector2 pointA;
    public Vector2 pointB;
    public float moveSpeed = 5f;

    private bool isAtPointB = false;
    private Vector2 targetPos;

    void Start()
    {
        if (popupRect == null)
        {
            popupRect = GetComponent<RectTransform>();
        }
        
        pointA = popupRect.anchoredPosition;
        targetPos = pointA;
    }

    void Update()
    {
        popupRect.anchoredPosition = Vector2.Lerp( popupRect.anchoredPosition, targetPos, moveSpeed * Time.deltaTime);
    }

    public void OnButtonPress()
    {
        isAtPointB = !isAtPointB;
        if (isAtPointB)
        {
            targetPos = pointB;
        }
        else
        {
            targetPos = pointA;
        }
    }
}