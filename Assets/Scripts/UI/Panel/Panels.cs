using UnityEngine;

public abstract class Panels : MonoBehaviour
{
    [Header("[Name Object]")]
    [SerializeField] private string _name;

    protected Player _player;

    public string Name => _name;

    public virtual void OpenPanel(Player player)
    {
        _player = player;
        UpdatePanelInfo();
        gameObject.SetActive(true);
        _player.GetComponent<PlayerMovement>().enabled = false;
        _player.SetIdleState();
    }

    public virtual void ClosePanel()
    {
        gameObject.SetActive(false);
        _player.GetComponent<PlayerMovement>().enabled = true;
        _player.SetIdleState();
    }

    protected virtual void UpdatePanelInfo() { }
}