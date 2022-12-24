using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [Header("[Image]")]
    [SerializeField] private Image _image;
    [Header("[Sprite]")]
    [SerializeField] private Sprite _acceptSprite;
    [SerializeField] private Sprite _cancelSprite;
    [Header("[Level Prefab]")]
    [SerializeField] private Levels _loadLevel;
    [Header("[Button]")]
    [SerializeField] private Button _button;

    public bool IsLevelComplete => _loadLevel.IsComplete;

    public void SetImage()
    {
        if (_loadLevel.IsComplete == true)
        {
            _image.sprite = _acceptSprite;
        }
        else
        {
            _image.sprite = _cancelSprite;
        }
    }

    public void UnlockButton()
    {
        _button.interactable = true;
    }
}