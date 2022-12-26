using System.Collections.Generic;
using UnityEngine;

public class AchievementsPanel : Panels
{
    [Header("[Views]")]
    [SerializeField] private AchievementsView _achievementsView;
    [Header("[Containers]")]
    [SerializeField] private GameObject _achievementsContainer;
    [Header("[Achievements]")]
    [SerializeField] private AchievementsView[] _achievementInPanel;

    private List<Achievements> _achievements;
    private PlayerAchievements _playerAchievements;

    private void Start()
    {
        Filling();
    }

    public void Filling()
    {
        _playerAchievements = Player.GetComponent<PlayerAchievements>();
        _achievements = _playerAchievements.GetListAchievements();

        for (int i = 0; i < _achievements.Count; i++)
        {
            Add(_achievements[i]);
        }

        var childrenAchievemetns = _achievementsContainer.GetComponentInChildren<Transform>();
        _achievementInPanel = new AchievementsView[childrenAchievemetns.childCount];

        for (int i = 0; i < childrenAchievemetns.childCount; i++)
        {
            _achievementInPanel[i] = childrenAchievemetns.GetChild(i).GetComponent<AchievementsView>();
        }
    }

    public void Add(Achievements achievements)
    {
        var view = Instantiate(_achievementsView, _achievementsContainer.transform);
        view.Render(achievements);
    }

    protected override void UpdatePanelInfo()
    {
        if (_achievementInPanel != null)
        {
            UpdateInfo();
        }
        else
        {
            return;
        }
    }

    private void UpdateInfo()
    {
        _playerAchievements = Player.GetComponent<PlayerAchievements>();
        _achievements = _playerAchievements.GetListAchievements();

        for (int index = 0; index < _achievementInPanel.Length; index++)
        {
            _achievementInPanel[index].UpdateCount(_achievements[index]);
        }
    }
}