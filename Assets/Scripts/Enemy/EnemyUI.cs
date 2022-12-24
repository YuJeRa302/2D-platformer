using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [Header("[Sliders]")]
    [SerializeField] private Slider _sliderHP;
    [Header("[Player]")]
    [SerializeField] private Enemy _enemy;

    public void SetSliderValue(int value)
    {
        _sliderHP.maxValue = value;
        _sliderHP.value = _sliderHP.maxValue;
    }

    private void OnEnable()
    {
        _enemy.ChangedHealth += OnChangeHealth;
    }

    private void OnDisable()
    {
        _enemy.ChangedHealth -= OnChangeHealth;
    }

    public void OnChangeHealth(int target)
    {
        _sliderHP.value = target;
    }
}