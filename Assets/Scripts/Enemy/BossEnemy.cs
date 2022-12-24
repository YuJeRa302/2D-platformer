using System.Collections;
using UnityEngine;

public class BossEnemy : Enemy
{
    [Header("[SpellDamage]")]
    [SerializeField] private int _spellDamage;
    [Header("[CooldownAbility]")]
    [SerializeField] private float _cooldownTime;

    private bool isUseSpell = false;
    private IEnumerator _cooldownSpell;
    private int _delayAttack = 1;
    private float _minTime = 0f;

    enum TransitionParametr
    {
        Die,
        Run,
        Attack,
        Hit,
        Cast,
        Spell
    }

    protected override void Start()
    {
        _enemyUi.SetSliderValue(Health);
        _target = FindObjectOfType<Player>().GetComponent<Transform>();
        _cooldownSpell = CooldownSpell(_cooldownTime);
        StartCoroutine(_cooldownSpell);
    }

    protected override IEnumerator AttackPlayer(Player player)
    {
        var waitForSeconds = new WaitForSeconds(_delayAttack);

        while (isAttack == true)
        {
            player.TakeDamage(Damage);

            if (isUseSpell == true)
            {
                _animator.Play(TransitionParametr.Cast.ToString());
                isUseSpell = false;
                player.TakeDamage(_spellDamage);

                if (_cooldownSpell != null)
                {
                    StopCoroutine(_cooldownSpell);
                    _cooldownSpell = CooldownSpell(_cooldownTime);
                    StartCoroutine(_cooldownSpell);
                }
            }

            yield return waitForSeconds;
        }
    }

    private void OnEnable()
    {
        Dying += OnEnemyDie;
    }

    private void OnDisable()
    {
        Dying -= OnEnemyDie;
    }

    private IEnumerator CooldownSpell(float timeLeft)
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            var normalizedValue = Mathf.Clamp(timeLeft, _minTime, _cooldownTime);
            yield return null;
        }

        isUseSpell = true;
    }

    private void OnEnemyDie(Enemy enemy)
    {
        StopCoroutine(_cooldownSpell);
    }
}