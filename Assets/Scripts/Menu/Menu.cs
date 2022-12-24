using UnityEngine;
using IJunior.TypedScenes;
using System.Threading.Tasks;

public class Menu : MonoBehaviour
{
    [Header("[Panels]")]
    [SerializeField] private Guid _guidPanel;
    [SerializeField] private Settings _settingsPanel;
    [SerializeField] private Menu _mainMenuPanel;
    [SerializeField] private Canvas _loadPanel;
    [Header("[Config]")]
    [SerializeField] private LoadConfig _loadConfig;
    [Header("[Audio Source]")]
    [SerializeField] private AudioSource _audioSource;

    private int _delayLoad = 8000;

    public async void PlayGame()
    {
        Game.LoadAsync(_loadConfig);
        _audioSource.Stop();
        _loadPanel.gameObject.SetActive(true);
        await Task.Delay(_delayLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}