using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "CreateLevelConfig")]
public class LoadConfig : ScriptableObject
{
    [Header("[Sound]")]
    [SerializeField] private float _ambientVolume;
    [SerializeField] private float _interfaceVolume;
    [Header("[Story]")]
    [SerializeField] private bool _isStoryShow = false;

    public float AmbientVolume => _ambientVolume;
    public float InterfaceVolume => _interfaceVolume;
    public bool IsStoryShow => _isStoryShow;

    public void SetConfigParametrs(Slider sliderAmbient, Slider sliderInterface)
    {
        _ambientVolume = sliderAmbient.value;
        _interfaceVolume = sliderInterface.value;
    }

    public void SetStoryShowValue(bool value)
    {
        _isStoryShow = value;
    }
}