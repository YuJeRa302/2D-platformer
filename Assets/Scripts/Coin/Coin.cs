using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _valueCoin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Wallet>(out Wallet wallet))
        {
            wallet.TakeCoin(_valueCoin);
            Destroy(gameObject);
        }
    }
}
