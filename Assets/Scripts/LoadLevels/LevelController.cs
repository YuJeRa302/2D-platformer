using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [Header("[Level Load]")]
    [SerializeField] private LevelLoad _levelLoad;
    [Header("[Level Name]")]
    [SerializeField] private Text _levelName;
    [Header("[Level Panel]")]
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _levelComplete;
    [Header("[Level Spawn]")]
    [SerializeField] private LevelSpawn _levelSpawn;
    [Header("[EndStory]")]
    [SerializeField] private AnimationStory _animationStory;
    [Header("[Config]")]
    [SerializeField] private LoadConfig _loadConfig;

    private int _countEnemyDie;
    private Levels _currentLevel;
    private bool _isPlayerAlive = true;
    private Player _player;
    private PlayerStats _playerStats;
    private PlayerAchievements _playerAchievements;
    private int _lastBossId = 5;

    public void TakeParametrsLevel(Levels loadLevel, Player player, Transform spawnPosition)
    {
        _currentLevel = loadLevel;
        _player = player;
        _playerStats = player.GetComponent<PlayerStats>();
        _playerAchievements = player.GetComponent<PlayerAchievements>();
        SpawnPlayer(spawnPosition);
        SetLevelParametrs();
    }

    public void StartLevel()
    {
        LoadScene();
    }

    public void OnEnemyDie(Enemy enemy)
    {
        _countEnemyDie++;
        _playerStats.OnEnemyDie(enemy);
        enemy.Dying -= OnEnemyDie;

        if (enemy.Id == _lastBossId)
        {
            KillLastBoss();
        }
        else if (_countEnemyDie == _currentLevel.CountEnemy && _isPlayerAlive == true)
        {
            WinPlayer();
        }
    }

    public void UnlockPlayerMovement()
    {
        _player.GetComponent<PlayerMovement>().enabled = true;
        _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void SpawnPlayer(Transform position)
    {
        LockPlayerMovement();
        _player.GetComponent<Rigidbody2D>().position = new Vector2(position.position.x, position.position.y);
    }

    private void LockPlayerMovement()
    {
        _player.GetComponent<PlayerMovement>().enabled = false;
        _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void SetLevelParametrs()
    {
        _levelName.text = _currentLevel.Name;
    }

    private void OnPlayerDie()
    {
        _isPlayerAlive = false;
        WinEnemy();
    }

    private void WinEnemy()
    {
        UpdateAchievements(_currentLevel.EnemyPrefab, _countEnemyDie);
        _player.PlayerDie -= OnPlayerDie;
        _levelLoad.LoadFirstLoaction(_gameOver);
    }

    private void WinPlayer()
    {
        UpdateAchievements(_currentLevel.EnemyPrefab, _countEnemyDie);
        _player.PlayerDie -= OnPlayerDie;
        _currentLevel.SetComplete();
        _levelLoad.LoadFirstLoaction(_levelComplete);
    }

    private void UpdateAchievements(Enemy enemy, int countEnemy)
    {
        _playerAchievements.UpdateAchievements(enemy, countEnemy);
    }

    private void KillLastBoss()
    {
        LockPlayerMovement();
        _animationStory.EndStory();
        _loadConfig.SetStoryShowValue(true);
        UpdateAchievements(_currentLevel.EnemyPrefab, _countEnemyDie);
    }

    private void LoadScene()
    {
        _isPlayerAlive = true;
        _countEnemyDie = 0;
        _player.PlayerDie += OnPlayerDie;
        _levelSpawn.StartSpawn(_currentLevel);
        _startPanel.SetActive(false);
        UnlockPlayerMovement();
    }
}