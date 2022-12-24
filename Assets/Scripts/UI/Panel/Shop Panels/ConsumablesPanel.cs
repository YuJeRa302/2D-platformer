using UnityEngine;

public class ConsumablesPanel : ShopPanel
{
    private int costPotion = 50;

    public void GetPlayer(Player player)
    {
        _player = player;
    }

    public void TrySellItem()
    {
        if (costPotion <= _player.Coins)
        {
            _player.BuyItems(costPotion);
        }
    }

    public void OpenPanel()
    {
        CloseAllPanels();
        gameObject.SetActive(true);
    }

    public override void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}