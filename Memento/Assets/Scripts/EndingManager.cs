using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    private bool triggered = false;

    void Update()
    {
        if (triggered) return;
        if (DegradationManager.Instance == null) return;
        if (DegradationManager.Instance.CurrentAct >= 5)
        {
            triggered = true;
            Invoke("ShowEnding", 3f);
        }
    }

    void ShowEnding()
    {
        GameObject cv = new GameObject("EndCanvas");
        Canvas canvas = cv.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 10;
        cv.AddComponent<CanvasScaler>();

        GameObject panel = new GameObject("EndPanel");
        panel.transform.SetParent(cv.transform, false);
        Image img = panel.AddComponent<Image>();
        img.color = new Color(0, 0, 0, 0.95f);
        RectTransform rt = panel.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.sizeDelta = Vector2.zero;

        GameObject textObj = new GameObject("EndText");
        textObj.transform.SetParent(panel.transform, false);
        Text endText = textObj.AddComponent<Text>();
        endText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        endText.fontSize = 32;
        endText.color = Color.white;
        endText.alignment = TextAnchor.MiddleCenter;
        endText.text = "She forgot everything.\n\nBut for a moment, she remembered.";
        RectTransform trt = textObj.GetComponent<RectTransform>();
        trt.anchorMin = Vector2.zero;
        trt.anchorMax = Vector2.one;
        trt.sizeDelta = Vector2.zero;
    }
}