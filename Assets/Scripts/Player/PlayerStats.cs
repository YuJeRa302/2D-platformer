using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("[Wallet]")]
    [SerializeField] private Wallet _wallet;
    [Header("[MaxPlayerLevel]")]
    [SerializeField] private int _maxPlayerLevel;
    [Header("[UI]")]
    [SerializeField] private UIUpdate _uIUpdate;

    private int _currentLevel = 0;
    private int _currentHealthPotion = 2;
    private int _currentExperience = 0;
    private int _maxExperience = 100;
    private int _minPoints = 0;
    private int _abilityPoints = 0;
    private int _healing = 20;
    private Dictionary<int, int> _levels = new Dictionary<int, int>();

    public int PlayerLevel => _currentLevel;
    public int Experience => _currentExperience;
    public int AbilityPoints => _abilityPoints;
    public int CountHealthPotion => _currentHealthPotion;

    private void Start()
    {
        GenerateLevelPlayer(_maxPlayerLevel);
        _uIUpdate.ChangeLevel(_currentLevel);
        _uIUpdate.ChangeCountPotion(_currentHealthPotion);
    }

    public void OnEnemyDie(Enemy enemy)
    {
        _currentExperience += enemy.ExperienceReward;
        _wallet.TakeCoin(enemy.GoldReward);
        _uIUpdate.OnChangeGold(enemy.GoldReward);
        _uIUpdate.OnChangeExperience(enemy.ExperienceReward);
        UpdateStats(_currentLevel);
    }

    public int Heal()
    {
        _currentHealthPotion--;
        _uIUpdate.ChangeCountPotion(_currentHealthPotion);
        return _healing;
    }

    public void TakePotion()
    {
        _currentHealthPotion++;
        _uIUpdate.ChangeCountPotion(_currentHealthPotion);
    }

    public void GivePoints(int value)
    {
        _abilityPoints = Mathf.Clamp(_abilityPoints - value, _minPoints, _abilityPoints);
    }

    private void UpdateStats(int level)
    {
        if (_levels.TryGetValue(level, out int value))
        {
            if (_currentExperience >= value)
            {
                var difference = _currentExperience - value;
                _currentLevel++;
                _abilityPoints++;
                _currentExperience = difference;
                _uIUpdate.ChangeLevel(_currentLevel);
                _levels.TryGetValue(_currentLevel, out int currentValue);
                _uIUpdate.SetNewValueSliderExperience(currentValue, _currentExperience);
            }
        }
        else
        {
            return;
        }
    }

    private void GenerateLevelPlayer(int level)
    {
        for (int i = 0; i < level; i++)
        {
            _levels.Add(i, _maxExperience + _maxExperience * i);
        }
    }
}