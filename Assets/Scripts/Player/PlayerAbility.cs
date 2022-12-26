using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [Header("[Player]")]
    [SerializeField] private Player _player;
    [Header("[Player Stats]")]
    [SerializeField] private PlayerStats _playerStats;
    [Header("[Ability]")]
    [SerializeField] private List<Ability> _abilities;

    enum NameAbility
    {
        Health,
        Armor
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

    public List<Ability> GetListAbility()
    {
        return _abilities;
    }

    private void ApplyAbility(Ability ability)
    {
        TryGetValue(ability.Name, out NameAbility nameAbility);

        switch (nameAbility)
        {
            case NameAbility.Armor:
                _player.UpdateArmor(ability.Value);
                break;
            case NameAbility.Health:
                _player.UpdateMaxHealth(ability.Value);
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