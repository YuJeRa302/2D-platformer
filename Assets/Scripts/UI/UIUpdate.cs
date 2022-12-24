using UnityEngine;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour
{
    [Header("[Sliders]")]
    [SerializeField] private Slider _sliderHP;
    [SerializeField] private Slider _sliderXP;
    [Header("[Player]")]
    [SerializeField] private Player _player;
    [Header("[Level]")]
    [SerializeField] private Text _level;
    [Header("[Text]")]
    [SerializeField] private Text _countHealthPotion;
    [SerializeField] private Text _countCoins;
    [SerializeField] private Text _countExperienceUpdate;
    [SerializeField] private Text _countGoldUpdate;
    [Header("[Animator]")]
    [SerializeField] private Animator _experienceUpdateAnimator;
    [SerializeField] private Animator _goldUpdateAnimator;

    private const string _nameExperienceParametr = "XP";
    private const string _nameGoldParametr = "G";

    enum TransitionParametr
    {
        Take,
    }

    private void Awake()
    {
        _sliderHP.value = _sliderHP.maxValue;
    }

    private void OnEnable()
    {
        _player.ChangedHealth += OnChangeHealth;
    }

    private void OnDisable()
    {
        _player.ChangedHealth -= OnChangeHealth;
    }

    public void ChangeCoinsCount(int value)
    {
        _countCoins.text = value.ToString();
    }

    public void ChangeLevel(int value)
    {
        _level.text = value.ToString();
    }

    public void ChangeCountPotion(int value)
    {
        _countHealthPotion.text = value.ToString();
    }

    public void OnChangeHealth(int target)
    {
        _sliderHP.value = target;
    }

    public void OnChangeExperience(int target)
    {
        SetTextStats(_countExperienceUpdate, target.ToString(), _nameExperienceParametr, _experienceUpdateAnimator);
        _sliderXP.value += target;
    }

    public void SetNewValueSliderExperience(int value, int difference)
    {
        _sliderXP.maxValue = value;
        _sliderXP.value = difference;
    }

    public void OnChangeGold(int target)
    {
        SetTextStats(_countGoldUpdate, target.ToString(), _nameGoldParametr, _goldUpdateAnimator);
    }

    private void SetTextStats(Text template, string text, string nameStats, Animator animator)
    {
        template.text = text + " " + nameStats;
        animator.SetTrigger(TransitionParametr.Take.ToString());
    }
}