using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    [SerializeField] private Coin _template;

    private void Start()
    {
        var childrenTransform = gameObject.GetComponentInChildren<Transform>();

        for (int i = 0; i < childrenTransform.childCount; i++)
        {
            CreateCoin(childrenTransform.GetChild(i));
        }
    }

    private void CreateCoin(Transform pointSpawn)
    {
        Instantiate(_template, new Vector2(pointSpawn.localPosition.x, pointSpawn.localPosition.y), Quaternion.identity);
    }
}