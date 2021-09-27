using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyManager
{
    // [public]

    // [private]
    private List<Enemy> enemies = new List<Enemy>();
}

public partial class EnemyManager
{
    // Singleton
    private static EnemyManager _instance;
    public static EnemyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EnemyManager();
            }
            return _instance;
        }
    }
}

public partial class EnemyManager
{
    public void StartTurn()
    {
        CoroutineRunner.RunCoroutine(ActionCoroutine());
    }

    IEnumerator ActionCoroutine()
    {
        foreach (Enemy e in enemies)
        {
            yield return e.Act();
            yield return new WaitForSeconds(0.2f);
        }
        Administrator.Instance.EnemyTrigger();
    }

    public void SummonEnemies()
    {
        Enemy e0 = new Enemy(new Vector2Int(0, 7), EnemyType.Minion);
        Enemy e1 = new Enemy(new Vector2Int(7, 7), EnemyType.Minion);
        enemies.Add(e0);
        enemies.Add(e1);
    }

    private EnemyManager() { }
}