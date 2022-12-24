using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : Panels
{
    [Header("[Levels]")]
    [SerializeField] private List<Levels> _levels = new List<Levels>();
    [Header("[Level Load]")]
    [SerializeField] private LevelLoad _levelLoad;
    [Header("[Set Button]")]
    [SerializeField] private GameObject _bossLocation;
    [SerializeField] private GameObject _standartLocation;
    [Header("[Standart Buttons]")]
    [SerializeField] private List<Buttons> _buttonsStandart;
    [Header("[Boss Buttons]")]
    [SerializeField] private List<Buttons> _buttonsBoss;

    public void LoadLevel(Levels loadLevel)
    {
        foreach (var level in _levels)
        {
            if (level.Id == loadLevel.Id)
            {
                SetLevelParametr(loadLevel);
                gameObject.SetActive(false);
            }
        }
    }

    public void OpenBossLocation()
    {
        LoadButtons(_buttonsBoss);
        _bossLocation.SetActive(true);
        _standartLocation.SetActive(false);
    }

    public void OpenStandartLocation()
    {
        LoadButtons(_buttonsStandart);
        _standartLocation.SetActive(true);
        _bossLocation.SetActive(false);
    }

    protected override void UpdatePanelInfo()
    {
        _bossLocation.SetActive(false);
        _standartLocation.SetActive(false);
    }

    private void SetLevelParametr(Levels loadLevel)
    {
        _levelLoad.LoadSecondLoaction(loadLevel, _player);
    }

    private void LoadButtons(List<Buttons> buttons)
    {
        for (int i = 0; i < buttons.Count - 1; i++)
        {
            buttons[i].SetImage();

            if (buttons[i].IsLevelComplete == true)
            {
                buttons[i + 1].UnlockButton();
            }
        }
    }
}