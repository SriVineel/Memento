using UnityEngine;
using UnityEngine.Events;

// Singleton — the spine of the entire game.
// Every other system reads DegradationLevel and CurrentAct from here.
// Usage from any script: DegradationManager.Instance.CurrentAct
public class DegradationManager : MonoBehaviour
{
    public static DegradationManager Instance { get; private set; }

    [Header("State")]
    public int CurrentAct = 1;           // 1 through 5
    public float DegradationLevel = 0f;  // 0.0 (clear) → 1.0 (total fog)

    // Other systems subscribe to this to react when the act changes
    public UnityEvent<int> OnActChanged;

    // How long the fade between acts takes (seconds)
    [Header("Transition")]
    public float TransitionDuration = 2f;
    private bool _transitioning = false;

    void Awake()
    {
        // Classic singleton pattern — same idea as C++ static instance
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject); // persist across scenes
    }

    // Call this to move to the next act
    public void AdvanceAct()
    {
        if (_transitioning) return;
        if (CurrentAct >= 5) return;
        StartCoroutine(TransitionToAct(CurrentAct + 1));
    }

    // Or jump directly to a specific act (useful for testing)
    public void SetAct(int act)
    {
        if (_transitioning) return;
        act = Mathf.Clamp(act, 1, 5);
        StartCoroutine(TransitionToAct(act));
    }

    private System.Collections.IEnumerator TransitionToAct(int newAct)
    {
        _transitioning = true;

        float targetLevel = (newAct - 1) / 4f; // Act1=0.0, Act3=0.5, Act5=1.0
        float startLevel  = DegradationLevel;
        float elapsed     = 0f;

        while (elapsed < TransitionDuration)
        {
            elapsed += Time.deltaTime;
            DegradationLevel = Mathf.Lerp(startLevel, targetLevel, elapsed / TransitionDuration);
            yield return null;
        }

        DegradationLevel = targetLevel;
        CurrentAct       = newAct;

        OnActChanged?.Invoke(newAct);
        Debug.Log($"[DegradationManager] Advanced to Act {newAct} | DegradationLevel = {DegradationLevel:F2}");

        _transitioning = false;
    }

    // ── Quick test: press 1-5 keys in the Editor to jump acts ──
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetAct(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetAct(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetAct(3);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SetAct(4);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SetAct(5);
#endif
    }
}
