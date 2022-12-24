using UnityEngine;
using UnityEngine.UI;

public class AchievementsView : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _countEnemy;
    [SerializeField] private Image _iconEnemy;
    [SerializeField] private Slider _slider;

    private Achievements _achievements;

    public void Render(Achievements achievements)
    {
        _achievements = achievements;
        _name.text = achievements.Name;
        _countEnemy.text = achievements.CurrentCount.ToString() + "/" + achievements.MaxCount.ToString();
        _iconEnemy.sprite = achievements.EnemyIcon;
        _slider.maxValue = achievements.MaxCount;
        _slider.value = achievements.CurrentCount;
    }

    public void UpdateCount(Achievements achievements)
    {
        _countEnemy.text = achievements.CurrentCount.ToString() + "/" + achievements.MaxCount.ToString();
        _slider.value = achievements.CurrentCount;
    }
}