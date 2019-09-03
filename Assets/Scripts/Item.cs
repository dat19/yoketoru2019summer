using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RandomPosition))]
public class Item : MonoBehaviour
{
    [Tooltip("基礎点"), SerializeField]
    int point = 100;
    [Tooltip("パーティクル"), SerializeField]
    GameObject getParticle = null;

    RandomPosition randPos = null;
    Animator anim;
    bool isStarted = false;
    SphereCollider sphereCollider = null;
    Rigidbody rb = null;

    private void Awake()
    {
        randPos = GetComponent<RandomPosition>();
        anim = GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!GameBehaviour.isPlayable) return;

        if (!isStarted)
        {
            isStarted = true;
            anim.SetTrigger("Spawn");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!GameBehaviour.isPlayable) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            sphereCollider.enabled = false;
            rb.velocity = Vector3.zero;

            SoundManager.PlaySE(SoundManager.AUDIO_LIST.ITEM);

            GameParams.AddScore(point*EnemySpawner.enemyCount);
            EnemySpawner.IncrementEnemy();
            EnemySpawner.ChangeDir();
            anim.SetTrigger("Get");

            if (getParticle != null)
            {
                Instantiate(getParticle, transform.position, Quaternion.identity);
            }
        }
    }

    /// <summary>
    /// 消えた時の処理
    /// </summary>
    public void Spawn()
    {
        randPos.SetPosition();
        rb.velocity = Vector3.zero;
        anim.SetTrigger("Spawn");
    }
}
