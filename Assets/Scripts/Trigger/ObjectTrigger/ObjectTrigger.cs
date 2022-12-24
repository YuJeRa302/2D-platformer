using UnityEngine;
using UnityEngine.UI;

public class ObjectTrigger : MonoBehaviour
{
    [SerializeField] private DialogPanel _dialogPanel;
    [SerializeField] private Text _nameObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _dialogPanel.gameObject.SetActive(true);
            _dialogPanel.GetObjectName(_nameObject.text);
            _dialogPanel.GetPlayer(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _dialogPanel.gameObject.SetActive(false);
        }
    }
}