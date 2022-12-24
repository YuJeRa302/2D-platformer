using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : Panels
{
    [Header("[Panels]")]
    [SerializeField] private EquipmentPanel _equipmentPanel;
    [SerializeField] private AbilityPanel _abilityPanel;
    [SerializeField] private ConsumablesPanel _consumablesPanel;
    [Header("[Coins]")]
    [SerializeField] private Text _coins;

    protected override void UpdatePanelInfo()
    {
        CloseAllPanels();
        _coins.text = _player.Coins.ToString();
        _equipmentPanel.FillingEquipment(_player);
        _abilityPanel.FillingAbility(_player);
        _consumablesPanel.GetPlayer(_player);
    }

    protected void CloseAllPanels()
    {
        _equipmentPanel.gameObject.SetActive(false);
        _abilityPanel.gameObject.SetActive(false);
        _consumablesPanel.gameObject.SetActive(false);
    }
}