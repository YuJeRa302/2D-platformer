using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : EnemyMovement
{
    [Header("[Enemy Stats]")]
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private int _goldReward;
    [SerializeField] private int _experienceReward;
    [SerializeField] private float _speed;

    private int _minHealth = 0;
    private UnityEvent<int> _healthBarUpdate = new UnityEvent<int>();
    private UnityEvent<Enemy> _die = new UnityEvent<Enemy>();
    private int _delayDestroy = 1;

    public int Id => _id;
    public string Name => _name;
    public int Damage => _damage;
    public float Speed => _speed;
    public int Health => _health;
    public int GoldReward => _goldReward;
    public int ExperienceReward => _experienceReward;

    public event UnityAction<int> ChangedHealth
    {
        add => _healthBarUpdate.AddListener(value);
        remove => _healthBarUpdate.RemoveListener(value);
    }

    public event UnityAction<Enemy> Dying
    {
        add => _die.AddListener(value);
        remove => _die.RemoveListener(value);
    }
    public void Die()
    {
        IsDead = true;
        Destroy(gameObject, _delayDestroy);
        _die.Invoke(this);
    }

    public void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, _minHealth, _health);
        _healthBarUpdate.Invoke(Health);
        TakeHit();
    }

    protected virtual void Start()
    {
        EnemyUi.SetSliderValue(Health);
        Target = FindObjectOfType<Player>();
    }
}