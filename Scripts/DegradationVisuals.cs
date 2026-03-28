using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DegradationVisuals : MonoBehaviour
{
    private Volume volume;
    private Vignette vignette;
    private DepthOfField dof;
    private ColorAdjustments colorAdj;

    void Start()
    {
        volume = GetComponent<Volume>();
        if (volume == null) { Debug.LogError("No Volume found!"); return; }

        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out dof);
        volume.profile.TryGet(out colorAdj);

        Debug.Log($"Vignette: {vignette != null}, DoF: {dof != null}, Color: {colorAdj != null}");
    }

    void Update()
    {
        float d = DegradationManager.Instance != null ? DegradationManager.Instance.DegradationLevel : 0f;

        if (vignette != null)
            vignette.intensity.Override(Mathf.Lerp(0f, 0.7f, d));

        if (colorAdj != null)
            colorAdj.saturation.Override(Mathf.Lerp(0f, -100f, d));

        if (dof != null)
        {
            dof.gaussianStart.Override(Mathf.Lerp(10f, 0.1f, d));
            dof.gaussianEnd.Override(Mathf.Lerp(30f, 0.5f, d));
        }
    }
}