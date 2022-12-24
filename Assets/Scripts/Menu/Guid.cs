using UnityEngine;

public class Guid : MonoBehaviour
{
    [Header("[Menu Panel]")]
    [SerializeField] private Menu _mainMenuPanel;

    public void OpenGuidPanel()
    {
        gameObject.SetActive(true);
        _mainMenuPanel.gameObject.SetActive(false);
    }

    public void CloseGuidPanel()
    {
        gameObject.SetActive(false);
        _mainMenuPanel.gameObject.SetActive(true);
    }
}