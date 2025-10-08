using System.Collections;
using UnityEngine;

public class CameraAngleSwitcher : MonoBehaviour
{
    public Camera mainCamera;           // Reference til kameraet
    public Transform[] cameraPositions; // Liste over kameravinkler
    private int currentIndex = 0;       // Aktuel position

    private Coroutine moveCoroutine;    // For at stoppe tidligere bevægelse, hvis man trykker igen

    // Skift til næste vinkel
    public void NextCameraAngle()
    {
        currentIndex++;

        if (currentIndex >= cameraPositions.Length)
            currentIndex = 0;

        MoveSmoothlyToCurrent();
    }

    // Skift til forrige vinkel
    public void PreviousCameraAngle()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = cameraPositions.Length - 1;

        MoveSmoothlyToCurrent();
    }

    // Starter smooth bevægelse til nuværende position
    private void MoveSmoothlyToCurrent()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(SmoothMove(cameraPositions[currentIndex]));
    }

    // Coroutine der flytter kameraet glidende
    private IEnumerator SmoothMove(Transform target)
    {
        float duration = 1f; // tid i sekunder
        float t = 0f;
        Vector3 startPos = mainCamera.transform.position;
        Quaternion startRot = mainCamera.transform.rotation;

        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = t / duration;
            mainCamera.transform.position = Vector3.Lerp(startPos, target.position, progress);
            mainCamera.transform.rotation = Quaternion.Slerp(startRot, target.rotation, progress);
            yield return null;
        }

        // Sørg for at slutte præcist på målet
        mainCamera.transform.position = target.position;
        mainCamera.transform.rotation = target.rotation;
    }
}
