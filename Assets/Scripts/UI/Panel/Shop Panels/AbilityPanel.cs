using System.Collections.Generic;
using UnityEngine;

public class AbilityPanel : ShopPanel
{
    [Header("[Views]")]
    [SerializeField] private AbilityView _abilityView;
    [Header("[Containers]")]
    [SerializeField] private GameObject _abilityContainer;

    private List<Ability> _abilities;
    private PlayerAbility _playerAbility;

    public void FillingAbility(Player player)
    {
        _playerAbility = player.GetComponent<PlayerAbility>();

        if (_abilities != null)
        {
            return;
        }
        else
        {
            Player = player;
            _abilities = _playerAbility.GetListAbility();

            for (int i = 0; i < _abilities.Count; i++)
            {
                AddAbility(_abilities[i]);
            }
        }
    }

    public void AddAbility(Ability ability)
    {
        var view = Instantiate(_abilityView, _abilityContainer.transform);
        view.SellButtonClick += OnSellButton;
        view.Render(ability);
    }

    public void OnSellButton(Ability ability, AbilityView view)
    {
        TrySellAbility(ability, view);
    }

    public void TrySellAbility(Ability ability, AbilityView view)
    {
        if (ability.Price <= Player.UpgradePoints)
        {
            _playerAbility.BuyAbility(ability);
            ability.Buy();
            view.SellButtonClick -= OnSellButton;
        }
    }

    public void OpenPanel()
    {
        CloseAllPanels();
        gameObject.SetActive(true);
    }

    public override void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}