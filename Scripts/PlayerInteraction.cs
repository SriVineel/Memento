using UnityEngine;
using TMPro; // TextMeshPro — add via Package Manager if missing

// Attach to the Player GameObject.
// Detects nearby InteractableObjects and handles E key press.
public class PlayerInteraction : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI PromptLabel; // drag your UI Text element here

    private InteractableObject _nearest;

    void Update()
    {
        FindNearest();

        // Show/hide prompt
        if (PromptLabel != null)
        {
            PromptLabel.gameObject.SetActive(_nearest != null);
            if (_nearest != null)
                PromptLabel.text = _nearest.PromptText;
        }

        // Interact
        if (Input.GetKeyDown(KeyCode.E) && _nearest != null)
            _nearest.Interact();
    }

    void FindNearest()
    {
        // Find all interactables in the scene and pick the closest in range
        var all = FindObjectsOfType<InteractableObject>();
        _nearest = null;
        float bestDist = float.MaxValue;

        foreach (var obj in all)
        {
            if (!obj.PlayerInRange) continue;
            float dist = Vector2.Distance(transform.position, obj.transform.position);
            if (dist < bestDist) { bestDist = dist; _nearest = obj; }
        }
    }
}
