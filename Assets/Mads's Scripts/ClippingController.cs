using UnityEngine;
using UnityEngine.UI;

public class ClippingController : MonoBehaviour
{
    [Header("References")]
    public Transform cutter;          // assign CutterCube
    public Material targetMaterial;   // material on RevealObject
    public Slider slider;             // UI Slider

    [Header("Slider -> movement")]
    //public float minX = -2f;          // slider=0 => x = minX
    //public float maxX = 2f;           // slider=1 => x = maxX
    public float minY = 0.5f;
    public float maxY = 4f;
    public bool useOrientedBox = false; // if true, shader expects world->local matrix

    void Start()
    {
        if (slider != null) slider.onValueChanged.AddListener(OnSliderChanged);
        UpdateMaterial(); // push initial values
    }

    void OnSliderChanged(float value)
    {
        Vector3 p = cutter.position;
        //p.x = Mathf.Lerp(minX, maxX, value);
        p.y = Mathf.Lerp(minY, maxY, value);
        cutter.position = p;
        UpdateMaterial();
    }

    // Call whenever cutter moves/scale/rot changes
    public void UpdateMaterial()
    {
        if (targetMaterial == null || cutter == null) return;

        if (useOrientedBox)
        {
            // Provide a matrix into the shader that converts world -> cutter-local
            targetMaterial.SetMatrix("_WorldToLocalClip", cutter.worldToLocalMatrix);
            // extents in the cutter's local space (unit cube default is 1 unit)
            Vector3 extentsLocal = cutter.localScale * 0.5f;
            targetMaterial.SetVector("_ClipExtents", extentsLocal);
            targetMaterial.SetFloat("_UseWorldToLocal", 1f);
        }
        else
        {
            // axis-aligned box in world space
            targetMaterial.SetFloat("_UseWorldToLocal", 0f);
            targetMaterial.SetVector("_ClipCenter", cutter.position);
            // extents in world units (lossyScale gives world size)
            Vector3 extentsWorld = cutter.lossyScale * 0.5f;
            targetMaterial.SetVector("_ClipExtents", extentsWorld);
        }
    }

    // optional: update every frame if cutter moved by other code
    void Update() { /* if you need continuous updates, call UpdateMaterial() here */ }
}

