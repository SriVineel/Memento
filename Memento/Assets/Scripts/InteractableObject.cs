using UnityEngine;
using UnityEngine.Events;

// Attach to any object the player can interact with:
// mementos, note surfaces, doors, etc.
// The player must be within range and press E.
public class InteractableObject : MonoBehaviour
{
    [Header("Settings")]
    public string PromptText    = "Press E to interact";
    public float  InteractRange = 1.5f;
    public bool   OneTimeOnly   = false;  // true = disable after first use

    [Header("Events")]
    public UnityEvent OnInteract; // drag other components here in the Inspector

    private bool _used = false;
    private bool _playerInRange = false;

    // ── Called by PlayerInteraction when E is pressed ──
    public void Interact()
    {
        if (_used) return;

        OnInteract?.Invoke();
        Debug.Log($"[Interactable] Interacted with: {gameObject.name}");

        if (OneTimeOnly) _used = true;
    }

    // ── Trigger-based range detection ──
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) _playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) _playerInRange = false;
    }

    // ── Show prompt label in Scene view ──
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, InteractRange);
    }

    public bool PlayerInRange => _playerInRange && !_used;
}
