public class ConsumablesPanel : ShopPanel
{
    private int costPotion = 50;

    public void GetPlayer(Player player)
    {
        Player = player;
    }

    public void TrySellItem()
    {
        if (costPotion <= Player.Coins)
        {
            Player.BuyConsumables(costPotion);
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