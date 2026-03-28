using UnityEngine;
using UnityEngine.Events;

// Singleton. Tracks the 3 mementos.
// Call MementoManager.Instance.Collect("MusicBox") from an InteractableObject's OnInteract event.
public class MementoManager : MonoBehaviour
{
    public static MementoManager Instance { get; private set; }

    // Which mementos have been collected
    public bool MusicBoxCollected    { get; private set; }
    public bool PhotographCollected  { get; private set; }
    public bool WristwatchCollected  { get; private set; }

    public UnityEvent<string> OnMementoCollected; // fires with the memento name
    public UnityEvent         OnAllCollected;      // fires when all 3 are found

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Call this from each memento's InteractableObject.OnInteract event
    // Pass the memento name: "MusicBox", "Photograph", or "Wristwatch"
    public void Collect(string mementoName)
    {
        switch (mementoName)
        {
            case "MusicBox":   if (MusicBoxCollected)   return; MusicBoxCollected   = true; break;
            case "Photograph": if (PhotographCollected)  return; PhotographCollected  = true; break;
            case "Wristwatch": if (WristwatchCollected) return; WristwatchCollected = true; break;
            default:
                Debug.LogWarning($"[MementoManager] Unknown memento: {mementoName}"); return;
        }

        Debug.Log($"[MementoManager] Collected: {mementoName}");
        OnMementoCollected?.Invoke(mementoName);

        // Advance the act each time a memento is found
        DegradationManager.Instance?.AdvanceAct();

        if (MusicBoxCollected && PhotographCollected && WristwatchCollected)
        {
            Debug.Log("[MementoManager] All mementos collected!");
            OnAllCollected?.Invoke();
        }
    }

    public int CollectedCount =>
        (MusicBoxCollected ? 1 : 0) +
        (PhotographCollected ? 1 : 0) +
        (WristwatchCollected ? 1 : 0);
}
