using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("[Menu Panel]")]
    [SerializeField] private Menu _mainMenuPanel;
    [Header("[Config]")]
    [SerializeField] private LoadConfig _config;
    [Header("[Sliders]")]
    [SerializeField] private Slider _ambientSoundsSlider;
    [SerializeField] private Slider _buttonFXSlider;
    [Header("[Sound]")]
    [SerializeField] private AudioSource _ambientSounds;
    [SerializeField] private AudioSource _buttonFX;

    private void Start()
    {
        _ambientSoundsSlider.value = _config.AmbientVolume;
        _buttonFXSlider.value = _config.InterfaceVolume;
    }

    public void OpenSettingsPanel()
    {
        gameObject.SetActive(true);
        _mainMenuPanel.gameObject.SetActive(false);
    }

    public void ApplyChanges()
    {
        _config.SetConfigParametrs(_ambientSoundsSlider, _buttonFXSlider);
        SetValueVolume(_ambientSoundsSlider, _buttonFXSlider);
        CloseSettingsPanel();
    }

    public void CloseSettingsPanel()
    {
        gameObject.SetActive(false);
        _mainMenuPanel.gameObject.SetActive(true);
    }

    private void SetValueVolume(Slider ambientSoundsSlider, Slider buttonFXSlider)
    {
        _ambientSounds.volume = ambientSoundsSlider.value;
        _buttonFX.volume = buttonFXSlider.value;
    }
}