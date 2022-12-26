using UnityEngine;

public abstract class Panels : MonoBehaviour
{
    [Header("[Name Object]")]
    [SerializeField] private string _name;

    protected Player Player;

    private PlayerMovement _playerMovement;

    public string Name => _name;

    public virtual void OpenPanel(Player player)
    {
        Player = player;
        _playerMovement = player.GetComponent<PlayerMovement>();
        UpdatePanelInfo();
        gameObject.SetActive(true);
        _playerMovement.enabled = false;
        _playerMovement.Recover();
    }

    public virtual void ClosePanel()
    {
        gameObject.SetActive(false);
        _playerMovement.enabled = true;
        _playerMovement.Recover();
    }

    protected virtual void UpdatePanelInfo() { }
}