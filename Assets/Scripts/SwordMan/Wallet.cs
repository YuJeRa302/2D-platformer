using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _coins;
    [Header("[Canvas]")]
    [SerializeField] private UIUpdate _uiUpdate;

    private void Awake()
    {
        TakeCoin(_coins);
    }

    public void TakeCoin(int value) 
    {
        _coins += value;
        _uiUpdate.UpdateCoin(_coins);
    }
}