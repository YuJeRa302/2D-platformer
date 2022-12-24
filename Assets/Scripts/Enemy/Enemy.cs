using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    [Header("[Enemy Stats]")]
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private int _goldReward;
    [SerializeField] private int _experienceReward;
    [SerializeField] private float _speed;

    [Header("[Animator]")]
    [SerializeField] protected Animator _animator;
    [Header("[UI]")]
    [SerializeField] protected EnemyUI _enemyUi;
    protected Transform _target;
    protected bool isAttack = false;
    protected bool isDead = false;

    private int _minHealth = 0;
    private UnityEvent<int> _healthBarUpdate = new UnityEvent<int>();
    private UnityEvent<Enemy> _die = new UnityEvent<Enemy>();
    private IEnumerator _makeDamage;
    private float _delay = 1f;
    private int _delayDestroy = 1;

    public int Id => _id;
    public string Name => _name;
    public int Damage => _damage;
    public float Speed => _speed;
    public int Health => _health;
    public int GoldReward => _goldReward;
    public int ExperienceReward => _experienceReward;

    enum TransitionParametr
    {
        Die,
        Run,
        Attack,
        Hit
    }

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

    public void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, _minHealth, _health);
        _healthBarUpdate.Invoke(Health);
        TakeHit();
    }

    protected void Die()
    {
        isDead = true;
        _animator.Play(TransitionParametr.Die.ToString());
        Destroy(gameObject, _delayDestroy);
        _die.Invoke(this);
    }

    protected virtual void Start()
    {
        _enemyUi.SetSliderValue(Health);
        _target = FindObjectOfType<Player>().GetComponent<Transform>();
    }

    protected void Update()
    {
        if (isDead != true)
        {
            if (isAttack != true)
            {
                Move();
            }
        }
    }

    protected void TakeHit()
    {
        _animator.SetTrigger(TransitionParametr.Hit.ToString());
        SetStateDie();
    }

    protected void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, Speed * Time.deltaTime);
        var rotation = transform.position.x - _target.position.x;
        _animator.Play(TransitionParametr.Run.ToString());

        if (rotation > 0)
        {
            FilpDirection(false);
        }
        else
        {
            FilpDirection(true);
        }
    }

    protected void FilpDirection(bool direction)
    {
        transform.localScale = new Vector3(direction ? 1 : -1, transform.localScale.y, transform.localScale.z);
        _enemyUi.transform.localScale = new Vector3(direction ? 1 : -1, _enemyUi.transform.localScale.y, _enemyUi.transform.localScale.z);
    }

    protected void SetStateDie()
    {
        if (Health == 0)
        {
            if (_makeDamage != null)
            {
                StopCoroutine(_makeDamage);
            }

            Die();
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead != true)
        {
            if (collision.TryGetComponent<Player>(out Player player))
            {
                OnAttack(player);
            }
        }
        else
        {
            return;
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            isAttack = false;

            if (_makeDamage != null)
            {
                StopCoroutine(_makeDamage);
            }
        }
    }

    protected void OnAttack(Player player)
    {
        isAttack = true;
        _animator.Play(TransitionParametr.Attack.ToString());

        if (_makeDamage != null)
        {
            StopCoroutine(_makeDamage);
        }

        _makeDamage = AttackPlayer(player);
        StartCoroutine(_makeDamage);
    }

    protected virtual IEnumerator AttackPlayer(Player player)
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        while (isAttack == true)
        {
            player.TakeDamage(Damage);
            yield return waitForSeconds;
        }
    }
}