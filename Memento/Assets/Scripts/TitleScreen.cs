using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    private GameObject titleCanvas;
    private bool started = false;

    void Start()
    {
        Time.timeScale = 0f;
        BuildUI();
    }

    void BuildUI()
    {
        titleCanvas = new GameObject("TitleCanvas");
        Canvas canvas = titleCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 20;
        titleCanvas.AddComponent<CanvasScaler>();

        GameObject panel = new GameObject("TitlePanel");
        panel.transform.SetParent(titleCanvas.transform, false);
        Image img = panel.AddComponent<Image>();
        img.color = Color.black;
        RectTransform rt = panel.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.sizeDelta = Vector2.zero;

        GameObject titleObj = new GameObject("TitleText");
        titleObj.transform.SetParent(panel.transform, false);
        Text title = titleObj.AddComponent<Text>();
        title.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        title.fontSize = 48;
        title.color = Color.white;
        title.alignment = TextAnchor.MiddleCenter;
        title.text = "Memento";
        RectTransform trt = titleObj.GetComponent<RectTransform>();
        trt.anchorMin = new Vector2(0, 0.4f);
        trt.anchorMax = new Vector2(1, 0.7f);
        trt.sizeDelta = Vector2.zero;

        GameObject subObj = new GameObject("SubText");
        subObj.transform.SetParent(panel.transform, false);
        Text sub = subObj.AddComponent<Text>();
        sub.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        sub.fontSize = 24;
        sub.color = new Color(1, 1, 1, 0.7f);
        sub.alignment = TextAnchor.MiddleCenter;
        sub.text = "Click to begin";
        RectTransform srt = subObj.GetComponent<RectTransform>();
        srt.anchorMin = new Vector2(0, 0.3f);
        srt.anchorMax = new Vector2(1, 0.45f);
        srt.sizeDelta = Vector2.zero;
    }

    void Update()
    {
        if (started) return;
        if (Input.GetMouseButtonDown(0))
        {
            started = true;
            Time.timeScale = 1f;
            Destroy(titleCanvas);
            Destroy(this);
        }
    }
}