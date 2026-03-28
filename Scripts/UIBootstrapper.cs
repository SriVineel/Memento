using UnityEngine;
using UnityEngine.UI;

public class UIBootstrapper : MonoBehaviour
{
    void Awake()
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        gameObject.AddComponent<CanvasScaler>();
        gameObject.AddComponent<GraphicRaycaster>();

        GameObject panel = new GameObject("MemoryPanel");
        panel.transform.SetParent(transform, false);
        Image img = panel.AddComponent<Image>();
        img.color = new Color(0, 0, 0, 0.85f);
        RectTransform prt = panel.GetComponent<RectTransform>();
        prt.anchorMin = new Vector2(0, 0);
        prt.anchorMax = new Vector2(1, 0);
        prt.pivot = new Vector2(0.5f, 0);
        prt.sizeDelta = new Vector2(0, 150);
        prt.anchoredPosition = Vector2.zero;

        GameObject textObj = new GameObject("MemoryText");
        textObj.transform.SetParent(panel.transform, false);
        Text txt = textObj.AddComponent<Text>();
        txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        txt.fontSize = 24;
        txt.color = Color.white;
        txt.alignment = TextAnchor.MiddleCenter;
        RectTransform trt = textObj.GetComponent<RectTransform>();
        trt.anchorMin = Vector2.zero;
        trt.anchorMax = Vector2.one;
        trt.sizeDelta = Vector2.zero;

        panel.SetActive(false);

        UIManager ui = GetComponent<UIManager>();
        if (ui != null)
        {
            ui.memoryPanel = panel;
            ui.memoryText = txt;
        }
    }
}