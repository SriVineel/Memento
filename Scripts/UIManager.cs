using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text memoryText;
    public GameObject memoryPanel;

    void Awake() { Instance = this; }

    public void ShowMemory(string text)
    {
        memoryPanel.SetActive(true);
        memoryText.text = text;
        memoryText.color = Color.black;
        memoryText.fontSize = 28;
        StartCoroutine(HideAfterDelay(4f));
    }

    IEnumerator HideAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        memoryPanel.SetActive(false);
    }
}