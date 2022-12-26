using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : ShopPanel
{
    [Header("[Views]")]
    [SerializeField] private WeaponView _weaponView;
    [SerializeField] private ArmorView _armorView;
    [Header("[Containers]")]
    [SerializeField] private GameObject _weaponContainer;
    [SerializeField] private GameObject _armorContainer;

    private List<Weapon> _weapons;
    private List<Armor> _armors;
    private PlayerEquipment _playerEquipment;

    public void FillingEquipment(Player player)
    {
        _playerEquipment = player.GetComponent<PlayerEquipment>();

        if (_weapons != null)
        {
            return;
        }
        else
        {
            Player = player;
            _weapons = _playerEquipment.GetListWeapon();
            _armors = _playerEquipment.GetListArmor();

            for (int i = 1; i < _weapons.Count; i++)
            {
                AddWeapon(_weapons[i]);
            }

            for (int i = 1; i < _armors.Count; i++)
            {
                AddArmor(_armors[i]);
            }
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        var view = Instantiate(_weaponView, _weaponContainer.transform);
        view.SellButtonClick += OnSellWeapon;
        view.Render(weapon);
    }

    public void AddArmor(Armor armor)
    {
        var view = Instantiate(_armorView, _armorContainer.transform);
        view.SellButtonClick += OnSellArmor;
        view.Render(armor);
    }

    public void OnSellWeapon(Weapon weapon, WeaponView view)
    {
        TrySellWeapon(weapon, view);
    }

    public void OnSellArmor(Armor armor, ArmorView view)
    {
        TrySellArmor(armor, view);
    }

    public void TrySellWeapon(Weapon weapon, WeaponView view)
    {
        if (weapon.Price <= Player.Coins)
        {
            _playerEquipment.BuyWeapon(weapon);
            weapon.Buy();
            view.SellButtonClick -= OnSellWeapon;
        }
    }

    public void TrySellArmor(Armor armor, ArmorView view)
    {
        if (armor.Price <= Player.Coins)
        {
            _playerEquipment.BuyArmor(armor);
            armor.Buy();
            view.SellButtonClick -= OnSellArmor;
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
