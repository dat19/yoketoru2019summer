using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("プレイヤーの移動速度"), SerializeField]
    float speed = 5f;

    Camera mainCamera = null;
    Rigidbody rb = null;
    ParticleSystem particleSystem = null;
    MeshRenderer []myRenderer = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        myRenderer = GetComponentsInChildren<MeshRenderer>();
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
        if (!GameBehaviour.isPlayable) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            particleSystem.Play();

            for (int i=0;i<myRenderer.Length;i++)
            {
                myRenderer[i].enabled = false;
            }

            rb.isKinematic = true;

            GameBehaviour.Gameover();
        }
    }
}
