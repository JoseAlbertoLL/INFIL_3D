
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider musicSlider;

    [Header("Brillo")]
    public Slider brightnessSlider;

    private const string MusicKey = "MusicVolume_v2";
    private const string BrightnessKey = "GameBrightness";

    private BrightnessOverlay[] brightnessOverlays;

    private void Start()
    {
        // Cargar volumen
        float savedVolume = PlayerPrefs.GetFloat(MusicKey, 0.75f);

        musicSlider.SetValueWithoutNotify(savedVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);

        SetMusicVolume(savedVolume);

        // Buscar los filtros de brillo de las escenas cargadas
        brightnessOverlays = FindObjectsByType<BrightnessOverlay>(
            FindObjectsSortMode.None
        );

        // Cargar brillo
        float savedBrightness = PlayerPrefs.GetFloat(
            BrightnessKey,
            1f
        );

        brightnessSlider.SetValueWithoutNotify(savedBrightness);
        brightnessSlider.onValueChanged.AddListener(SetBrightness);

        SetBrightness(savedBrightness);
    }

    public void SetMusicVolume(float volume)
    {
        float decibels = volume <= 0.0001f
            ? -80f
            : Mathf.Log10(volume) * 20f;

        audioMixer.SetFloat("MusicVolume", decibels);

        PlayerPrefs.SetFloat(MusicKey, volume);
        PlayerPrefs.Save();
    }

    public void SetBrightness(float brightness)
    {
        brightness = Mathf.Clamp01(brightness);

        foreach (BrightnessOverlay overlay in brightnessOverlays)
        {
            if (overlay != null)
                overlay.SetBrightness(brightness);
        }

        PlayerPrefs.SetFloat(BrightnessKey, brightness);
        PlayerPrefs.Save();
    }


    public void ResetSettings()
    {
        
        float defaultVolume = 0.75f;

        musicSlider.SetValueWithoutNotify(defaultVolume);
        SetMusicVolume(defaultVolume);

        
        float defaultBrightness = 1f;

        brightnessSlider.SetValueWithoutNotify(defaultBrightness);
        SetBrightness(defaultBrightness);
    }


    private void OnDestroy()
    {
        if (musicSlider != null)
            musicSlider.onValueChanged.RemoveListener(SetMusicVolume);

        if (brightnessSlider != null)
            brightnessSlider.onValueChanged.RemoveListener(SetBrightness);
    }
}



