
using UnityEngine;
using UnityEngine.UI;

public class BrightnessOverlay : MonoBehaviour
{
    private Image overlayImage;

    private const string BrightnessKey = "GameBrightness";

    private void Awake()
    {
        overlayImage = GetComponent<Image>();
    }

    private void Start()
    {
        float brightness = PlayerPrefs.GetFloat(BrightnessKey, 1f);
        SetBrightness(brightness);
    }

    public void SetBrightness(float brightness)
    {
        brightness = Mathf.Clamp01(brightness);

        Color color = overlayImage.color;
        color.a = 1f - brightness;

        overlayImage.color = color;

        PlayerPrefs.SetFloat("GameBrightness", brightness);
        PlayerPrefs.Save();
    }
}
