using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Tooltip("基礎点"), SerializeField]
    int point = 100;
    [Tooltip("パーティクル"), SerializeField]
    GameObject getParticle = null;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameParams.AddScore(point*EnemySpawner.enemyCount);
            EnemySpawner.IncrementEnemy();

            if (getParticle != null)
            {
                Instantiate(getParticle, transform.position, Quaternion.identity);
            }
        }
    }
}
