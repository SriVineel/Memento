using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// Attach this to the GameObject that holds your Global Volume.
// Requires: a Volume component on the same GameObject with
//   Vignette and DepthOfField (Gaussian) overrides added.
[RequireComponent(typeof(Volume))]
public class DegradationVisuals : MonoBehaviour
{
    [Header("Vignette")]
    public float MaxVignetteIntensity = 0.6f;

    [Header("Blur (Depth of Field — Gaussian mode)")]
    public float MaxBlurStart  = 0f;
    public float MaxBlurEnd    = 30f;

    [Header("Color Desaturation")]
    public float MaxDesaturation = 1f; // 0 = full colour, 1 = greyscale

    private Volume          _volume;
    private Vignette        _vignette;
    private DepthOfField    _dof;
    private ColorAdjustments _colorAdj;

    void Awake()
    {
        _volume = GetComponent<Volume>();

        // Try to grab each override — won't crash if one isn't added yet
        _volume.profile.TryGet(out _vignette);
        _volume.profile.TryGet(out _dof);
        _volume.profile.TryGet(out _colorAdj);
    }

    void Update()
    {
        if (DegradationManager.Instance == null) return;
        float d = DegradationManager.Instance.DegradationLevel; // 0.0 → 1.0

        // Vignette
        if (_vignette != null)
            _vignette.intensity.Override(Mathf.Lerp(0f, MaxVignetteIntensity, d));

        // Gaussian blur via Depth of Field
        if (_dof != null)
        {
            _dof.gaussianStart.Override(Mathf.Lerp(100f, MaxBlurStart,  d));
            _dof.gaussianEnd  .Override(Mathf.Lerp(100f, MaxBlurEnd,    d));
            _dof.gaussianMaxRadius.Override(Mathf.Lerp(0f, 2f, d));
        }

        // Desaturation
        if (_colorAdj != null)
            _colorAdj.saturation.Override(Mathf.Lerp(0f, -100f * MaxDesaturation, d));
    }
}
