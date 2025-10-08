using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [Header("Camera Setup")]
    public Camera insideViewCamera; // 👈 this will be InsideViewCamera

    [Header("Camera Targets")]
    public Transform CameraTarget1;
    public Transform CameraTarget2;
    public Transform CameraTarget3;

    [Header("Settings")]
    public float sSpeed = 10.0f;
    public Vector3 dist;
    public Transform lookTarget;

    private int currentTarget;
    private Transform cameraTarget;

    private void Start()
    {
        currentTarget = 1;
        SetCameraTarget(currentTarget);
    }

    private void FixedUpdate()
    {
        if (insideViewCamera == null || cameraTarget == null) return;

        Vector3 desiredPos = cameraTarget.position + dist;
        Vector3 smoothPos = Vector3.Lerp(insideViewCamera.transform.position, desiredPos, sSpeed * Time.deltaTime);
        insideViewCamera.transform.position = smoothPos;

        if (lookTarget != null)
            insideViewCamera.transform.LookAt(lookTarget.position);
    }

    public void SetCameraTarget(int num)
    {
        switch (num)
        {
            case 1: cameraTarget = CameraTarget1; break;
            case 2: cameraTarget = CameraTarget2; break;
            case 3: cameraTarget = CameraTarget3; break;
        }
    }

    public void SwitchTheCamera()
    {
        currentTarget = currentTarget < 3 ? currentTarget + 1 : 1;
        SetCameraTarget(currentTarget);
    }
}
