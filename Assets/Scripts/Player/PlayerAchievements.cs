using System.Collections.Generic;
using UnityEngine;

public class PlayerAchievements : MonoBehaviour
{
    [Header("[Transform]")]
    [SerializeField] private Transform _achievementsTransform;
    [Header("[Achievements]")]
    [SerializeField] private List<Achievements> _achievements;

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

    public List<Achievements> GetListAchievements()
    {
        return _achievements;
    }

    private void Awake()
    {
        AddItemToList();
    }

    private void AddItemToList()
    {
        for (int i = 0; i < _achievementsTransform.childCount; i++)
        {
            _achievements.Add(_achievementsTransform.GetChild(i).GetComponent<EnemyAchiev>());
        }
    }
}