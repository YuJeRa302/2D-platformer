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
        _coins.text = Player.Coins.ToString();
        _equipmentPanel.FillingEquipment(Player);
        _abilityPanel.FillingAbility(Player);
        _consumablesPanel.GetPlayer(Player);
    }

    protected void CloseAllPanels()
    {
        _equipmentPanel.gameObject.SetActive(false);
        _abilityPanel.gameObject.SetActive(false);
        _consumablesPanel.gameObject.SetActive(false);
    }
}