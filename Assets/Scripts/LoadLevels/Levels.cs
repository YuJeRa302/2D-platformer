using UnityEngine;

public abstract class Levels : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _countEnemy;
    [SerializeField] private bool _isComplete;

    public int Id => _id;
    public string Name => _name;
    public Enemy EnemyPrefab => _prefab;
    public int CountEnemy => _countEnemy;
    public bool IsComplete => _isComplete;

    public void SetComplete()
    {
        _isComplete = true;
    }
}