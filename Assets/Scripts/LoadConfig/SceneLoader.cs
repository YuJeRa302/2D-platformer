using UnityEngine;
using IJunior.TypedScenes;

public class SceneLoader : MonoBehaviour, ISceneLoadHandler<LoadConfig>
{
    [Header("[Audio Source]")]
    [SerializeField] private LevelSounds _levelSounds;
    [SerializeField] private ButtonFX _buttonFX;

    public void OnSceneLoaded(LoadConfig argument)
    {
        _levelSounds.SetValueVolume(argument.AmbientVolume);
        _buttonFX.SetValueVolume(argument.InterfaceVolume);
    }
}