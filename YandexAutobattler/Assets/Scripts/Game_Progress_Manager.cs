using UnityEngine;

public class Game_Progress_Manager : MonoBehaviour
{
    [SerializeField] private Player_Stats _playerStats;
    [SerializeField] private Enemy_Spawner _enemySpawner;
    [SerializeField] private int _currentGold;
    [SerializeField] private int _currentXP;
    [SerializeField] private int _XPForNextLVL;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Stats>();
    }

    private void LootEnemy(int gold, int xp)
    {
        GetGold(gold);
        GetXP(xp);
        
    }

    private void GetGold(int gold)
    {
        _currentGold += Random.Range(0, gold);
    }

    private void GetXP(int xp)
    {
        _currentXP += xp;
        CheckForNextLVL();
    }

    private void CheckForNextLVL()
    {
        if (_currentXP >= _XPForNextLVL)
        {
            _playerStats.GetLVL();
            _currentXP = 0;
            _XPForNextLVL = ((int)Mathf.Pow(_playerStats.LVL, 2));
            _playerStats.HP = _playerStats.HP + _playerStats.LVL;
        }
    }

    private void OnEnable()
    {
        Enemy_Stats.onDeath += LootEnemy;
    }

    private void OnDisable()
    {
        Enemy_Stats.onDeath -= LootEnemy;
    }
}
