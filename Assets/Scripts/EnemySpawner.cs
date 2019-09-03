using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("敵のプレハブ"), SerializeField]
    GameObject enemyPrefab = null;
    [Tooltip("開始時の敵の数"), SerializeField]
    int startEnemyCount = 1;
    [Tooltip("敵の最低速度"), SerializeField]
    float minSpeed = 2f;
    [Tooltip("敵の最高速度"), SerializeField]
    float maxSpeed = 6f;

    List<GameObject> enemies = new List<GameObject>();

    static int enemyCount;

    private void Start()
    {
        enemyCount = startEnemyCount;
    }

    private void FixedUpdate()
    {
        for (; enemies.Count < enemyCount; )
        {
            GameObject go = Instantiate<GameObject>(enemyPrefab);
            float spd = Random.Range(minSpeed, maxSpeed);
            go.GetComponent<Enemy>().SetSpeed(spd);
            enemies.Add(go);
        }
    }

}
