using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : Panels
{
    [SerializeField] private Text _text;
    [SerializeField] private List<Panels> _panels = new List<Panels>();

    private string _nameObject;

    public void GetPlayer(Player player)
    {
        Player = player;
    }

    public void GetObjectName(string value)
    {
        _nameObject = value;
        SetText();
    }

    public void ClickOk()
    {
        foreach (var panel in _panels)
        {
            if (panel.Name.Equals(_nameObject))
            {
                panel.OpenPanel(Player);
            }
        }

        gameObject.SetActive(false);
    }

    public void ClickCancel()
    {
        gameObject.SetActive(false);
    }

    private void SetText()
    {
        string value = "Желаете открыть ";

        foreach (var panel in _panels)
        {
            if (panel.Name.Equals(_nameObject))
            {
                value += panel.Name + "?";
            }
        }

        _text.text = value;
    }
}