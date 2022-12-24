using UnityEngine;

public class Wallet : MonoBehaviour
{
    [Header("[UiUpdate]")]
    [SerializeField] private UIUpdate _uIUpdate;

    private int _minCoins = 0;
    private int _currentCoins = 100;

    public void Buy(int itemCost)
    {
        _currentCoins = Mathf.Clamp(_currentCoins - itemCost, _minCoins, _currentCoins);
        _uIUpdate.ChangeCoinsCount(_currentCoins);
    }

    public void TakeCoin(int value)
    {
        _currentCoins += value;
        _uIUpdate.ChangeCoinsCount(_currentCoins);
    }

    public int GiveCoin()
    {
        _uIUpdate.ChangeCoinsCount(_currentCoins);
        return _currentCoins;
    }
}