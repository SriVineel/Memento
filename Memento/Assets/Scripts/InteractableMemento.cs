using UnityEngine;

public class InteractableMemento : MonoBehaviour
{
    public int actToUnlock = 1;
    public string memoryText = "A memory surfaces...";
    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;
        if (other.CompareTag("Player"))
        {
            collected = true;
            DegradationManager.Instance.SetAct(actToUnlock);
            UIManager.Instance.ShowMemory(memoryText);
            gameObject.SetActive(false);
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayCollect();
    }
}