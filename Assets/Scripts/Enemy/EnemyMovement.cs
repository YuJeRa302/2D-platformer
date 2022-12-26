using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("[Animator]")]
    [SerializeField] protected Animator Animator;
    [Header("[UI]")]
    [SerializeField] protected EnemyUI EnemyUi;

    [Header("[Enemy]")]
    [SerializeField] private Enemy _enemy;

    protected Player Target;
    protected bool IsAttack = false;
    protected bool IsDead = false;

    private IEnumerator _makeDamage;
    private float _delay = 1f;

    enum TransitionParametr
    {
        Die,
        Run,
        Attack,
        Hit
    }

    protected void Update()
    {
        if (IsDead != true)
        {
            if (IsAttack != true)
            {
                Move();
            }
        }
    }

    protected void TakeHit()
    {
        Animator.SetTrigger(TransitionParametr.Hit.ToString());
        SetStateDie();
    }

    protected void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, _enemy.Speed * Time.deltaTime);
        var rotation = transform.position.x - Target.transform.position.x;
        Animator.Play(TransitionParametr.Run.ToString());

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
        EnemyUi.transform.localScale = new Vector3(direction ? 1 : -1, EnemyUi.transform.localScale.y, EnemyUi.transform.localScale.z);
    }

    protected void SetStateDie()
    {
        if (_enemy.Health == 0)
        {
            Animator.Play(TransitionParametr.Die.ToString());

            if (_makeDamage != null)
            {
                StopCoroutine(_makeDamage);
            }

            _enemy.Die();
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDead != true)
        {
            if (collision.TryGetComponent<Player>(out Player player))
            {
                Animator.Play(TransitionParametr.Attack.ToString());

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
            IsAttack = false;

            if (_makeDamage != null)
            {
                StopCoroutine(_makeDamage);
            }
        }
    }

    protected void OnAttack(Player player)
    {
        IsAttack = true;

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

        while (IsAttack == true)
        {
            player.TakeDamage(_enemy.Damage);
            yield return waitForSeconds;
        }
    }
}