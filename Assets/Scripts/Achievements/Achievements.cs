using UnityEngine;

public abstract class Achievements : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _currentCount;
    [SerializeField] private int _maxCount;

    private int _minEnemyCount = 0;

    public int Id => _id;
    public Sprite EnemyIcon => _sprite;
    public string Name => _name;
    public int CurrentCount => _currentCount;
    public int MaxCount => _maxCount;

    public void UpdateCount(int value)
    {
        _currentCount = Mathf.Clamp(_currentCount + value, _minEnemyCount, _maxCount);
    }
}