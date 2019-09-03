using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("プレイヤーの移動速度"), SerializeField]
    float speed = 5f;

    Camera mainCamera = null;
    Rigidbody rb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!GameBehaviour.isPlayable) return;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        if (mainCamera == null) return;

        Vector3 mp = Input.mousePosition;
        mp.z = -mainCamera.transform.position.z;
        Vector3 target = mainCamera.ScreenToWorldPoint(mp);
        Vector3 vel = (target - transform.position) / Time.fixedDeltaTime;
        if (vel.magnitude < speed)
        {
            rb.velocity = vel;
        }
        else
        {
            rb.velocity = vel.normalized * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameBehaviour.Gameover();
        }
    }
}
