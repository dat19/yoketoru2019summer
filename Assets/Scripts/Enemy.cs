using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RandomPosition), typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    #region Private Variables

    RandomPosition rand = null;
    Rigidbody rb = null;
    /// <summary>
    /// スピード
    /// </summary>
    float speed = 5f;

    #endregion Private Variables

    private void Awake()
    {
        rand = GetComponent<RandomPosition>();
        rb = GetComponent<Rigidbody>();
        rand.SetPosition();
    }

    /// <summary>
    /// 指定の値を速度として記録して、ランダムで移動方向を設定します。
    /// </summary>
    /// <param name="spd">設定する速度</param>
    public void SetSpeed(float spd)
    {
        speed = spd;
        float th = Random.Range(0f, Mathf.PI * 2f);
        Vector3 vel = new Vector3(Mathf.Cos(th), Mathf.Sin(th), 0);
        rb.velocity = vel * speed;
    }

    private void FixedUpdate()
    {
        // 
    }
}
