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
        EnemyUi.SetSliderValue(Health);
        Target = FindObjectOfType<Player>();
        _cooldownSpell = CooldownSpell(_cooldownTime);
        StartCoroutine(_cooldownSpell);
    }

    protected override IEnumerator AttackPlayer(Player player)
    {
        var waitForSeconds = new WaitForSeconds(_delayAttack);

        while (IsAttack == true)
        {
            player.TakeDamage(Damage);

            if (isUseSpell == true)
            {
                Animator.Play(TransitionParametr.Cast.ToString());
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
        yield return new WaitForSeconds(timeLeft);
        isUseSpell = true;
    }

    private void OnEnemyDie(Enemy enemy)
    {
        StopCoroutine(_cooldownSpell);
    }
}