using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("プレイヤーの移動速度"), SerializeField]
    float speed = 12f;
    [Tooltip("向きの調整"), SerializeField]
    float dirRate = 0.3f;
    [Tooltip("向く角度"), SerializeField]
    float turnDegree = 30f;

    Camera mainCamera = null;
    Rigidbody rb = null;
    ParticleSystem myParticleSystem = null;
    MeshRenderer []myRenderer = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        myParticleSystem = GetComponentInChildren<ParticleSystem>();
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

        Vector3 f = Vector3.forward;
        if (!Mathf.Approximately(rb.velocity.magnitude, 0)) {
            Vector3 dirvel = rb.velocity.normalized;

            f = Quaternion.Euler(dirvel.y*turnDegree, -dirvel.x * turnDegree, 0)* Vector3.forward;
        }

        transform.forward = Vector3.Lerp(transform.forward,   f, dirRate);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!GameBehaviour.isPlayable) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            myParticleSystem.Play();

            for (int i=0;i<myRenderer.Length;i++)
            {
                myRenderer[i].enabled = false;
            }

            rb.isKinematic = true;

            GameBehaviour.Gameover();
        }
    }
}
