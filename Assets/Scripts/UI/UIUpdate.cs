using UnityEngine;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour
{
    [Header("[Coin]")]
    [SerializeField] private Text _coinCount;

    public void UpdateCoin(int value)
    {
        _coinCount.text = value.ToString();
    }
}
