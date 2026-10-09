using UnityEngine;
using ServiceLocatorPattern;
using UnityEngine.UI;

public class BrightnessOverlay : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Start()
    {
        ServiceLocator.Instance.Register(typeof(BrightnessOverlay), gameObject);
        canvasGroup = GetComponent<CanvasGroup>();
        DontDestroyOnLoad(gameObject);
    }

    public void SetAlpha(float value)
    {
        if (canvasGroup == null){return;}

        canvasGroup.alpha = 1f - value;
    }

    public float GetAlpha()
    {
        if (canvasGroup == null){return 0f;}

        return 1f - canvasGroup.alpha;
    }
}
