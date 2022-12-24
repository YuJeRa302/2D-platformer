using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("[Transform]")]
    [SerializeField] private Transform _weponsTransform;
    [SerializeField] private Transform _armorsTransform;
    [SerializeField] private Transform _achievementsTransform;
    [Header("[Weapon]")]
    [SerializeField] private List<Weapon> _weapon;
    [Header("[Armor]")]
    [SerializeField] private List<Armor> _armor;
    [Header("[Ability]")]
    [SerializeField] private List<Ability> _abilities;
    [Header("[Achievements]")]
    [SerializeField] private List<Achievements> _achievements;
    [Header("[Wallet]")]
    [SerializeField] private Wallet _wallet;
    [Header("[Player Stats]")]
    [SerializeField] private PlayerStats _playerStats;
    [Header("[Player Movement]")]
    [SerializeField] private PlayerMovement _playerMovement;

    private int _currentHealth;
    private int _minHealth = 0;
    private int _maxHealth = 100;
    private int _playerArmor = 0;
    private Weapon _currentWeapon;
    private Armor _currentArmor;
    private UnityEvent<int> _healthBarUpdate = new UnityEvent<int>();
    private UnityEvent _playerDie = new UnityEvent();

    enum NameAbility
    {
        Health,
        Armor
    }

    public int UpgradePoints => _playerStats.AbilityPoints;
    public int Coins => _wallet.GiveCoin();
    public int CountWeapon => _weapon.Count;
    public int CountArmor => _armor.Count;

    public event UnityAction<int> ChangedHealth
    {
        add => _healthBarUpdate.AddListener(value);
        remove => _healthBarUpdate.RemoveListener(value);
    }

    public event UnityAction PlayerDie
    {
        add => _playerDie.AddListener(value);
        remove => _playerDie.RemoveListener(value);
    }

    private void Awake()
    {
        AddItemToList();
        _currentWeapon = _weapon[0];
        _currentArmor = _armor[0];
        _playerArmor = _currentArmor.ItemArmor;
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - (damage - _playerArmor), _minHealth, _maxHealth);
            _healthBarUpdate.Invoke(_currentHealth);
        }
        else
        {
            _playerDie.Invoke();
        }
    }

    public void Heal()
    {
        if (_playerStats.CountHealthPotion > 0 && _currentHealth != _maxHealth)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + _playerStats.Heal(), _minHealth, _maxHealth);
            _healthBarUpdate.Invoke(_currentHealth);
        }
        else
        {
            return;
        }
    }

    public void BuyWeapon(Weapon weapon)
    {
        _wallet.Buy(weapon.Price);
        _weapon.Add(weapon);
        _currentWeapon.gameObject.SetActive(false);

        if (_currentWeapon.Damage < weapon.Damage)
        {
            _currentWeapon = weapon;
        }
        else
        {
            return;
        }
    }

    public void BuyArmor(Armor armor)
    {
        _wallet.Buy(armor.Price);
        _armor.Add(armor);
        _currentArmor.gameObject.SetActive(false);

        if (_currentArmor.ItemArmor < armor.ItemArmor)
        {
            _currentArmor = armor;
        }
        else
        {
            return;
        }
    }

    public void BuyItems(int value)
    {
        _wallet.Buy(value);
        _playerStats.TakePotion();
    }

    public void BuyAbility(Ability ability)
    {
        if (_playerStats.AbilityPoints >= ability.Price)
        {
            _abilities.Add(ability);
            _playerStats.GivePoints(ability.Price);
            ApplyAbility(ability);
        }
        else
        {
            return;
        }
    }

    public void OnEnemyDie(Enemy enemy)
    {
        _playerStats.OnEnemyDie(enemy);
    }

    public void UpdateAchievements(Enemy enemy, int countEnemy)
    {
        foreach (var achievement in _achievements)
        {
            if (achievement.Id == enemy.Id)
            {
                achievement.UpdateCount(countEnemy);
            }
        }
    }

    public void Recover()
    {
        _currentHealth = _maxHealth;
        _playerMovement.Recover();
        _healthBarUpdate.Invoke(_currentHealth);
    }

    public void SetIdleState()
    {
        _playerMovement.Recover();
    }

    public List<Weapon> GetListWeapon()
    {
        return _weapon;
    }

    public List<Armor> GetListArmor()
    {
        return _armor;
    }

    public List<Ability> GetListAbility()
    {
        return _abilities;
    }

    public List<Achievements> GetListAchievements()
    {
        return _achievements;
    }

    private void AddItemToList()
    {
        for (int i = 0; i < _weponsTransform.childCount; i++)
        {
            _weapon.Add(_weponsTransform.GetChild(i).GetComponent<Sword>());
        }

        for (int i = 0; i < _armorsTransform.childCount; i++)
        {
            _armor.Add(_armorsTransform.GetChild(i).GetComponent<Helmet>());
        }

        for (int i = 0; i < _achievementsTransform.childCount; i++)
        {
            _achievements.Add(_achievementsTransform.GetChild(i).GetComponent<EnemyAchiev>());
        }
    }

    private void ApplyAbility(Ability ability)
    {
        TryGetValue(ability.Name, out NameAbility nameAbility);

        switch (nameAbility)
        {
            case NameAbility.Armor:
                _playerArmor += ability.Value;
                break;
            case NameAbility.Health:
                _maxHealth += ability.Value;
                break;
        }
    }

    private bool TryGetValue(string value, out NameAbility nameAbility)
    {
        nameAbility = default(NameAbility);
        bool success = Enum.IsDefined(typeof(NameAbility), value);

        if (success)
        {
            nameAbility = (NameAbility)Enum.Parse(typeof(NameAbility), value);
        }

        return success;
    }
}